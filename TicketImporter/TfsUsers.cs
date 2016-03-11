#region License
/*
    This source makes up part of JiraToTfs, a utility for migrating Jira
    tickets to Microsoft TFS.

    Copyright(C) 2016  Ian Montgomery

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TicketImporter
{
    public delegate void ImpersonationFailure(string failure);

    public class TfsUsers
    {
        public TfsUsers(TfsProject tfsProject)
        {
            this.tfsProject = tfsProject;
            defaultAsignee = "";
            failedImpersonations = new HashSet<User>();
            unableToAddTickets = new HashSet<User>();
            var settings = SettingsStore.Load(key);
            settings.TryGetValue(defaultAsigneeKey, out defaultAsignee);
            defaultCreator = "";
            settings.TryGetValue(defaultCreatorKey, out defaultCreator);
            tfsUsers = allTfsUsers(this.tfsProject.Tfs);
            canImpersonate = ableToImpersonate(this.tfsProject.Tfs);
            impersonated_Users = new Dictionary<User, TfsTeamProjectCollection>();
        }

        public IEnumerable<string> DefaultAsignees
        {
            get
            {
                var commonUsers = new Dictionary<string, int>();

                foreach (WorkItemType workItemType in tfsProject.WorkItemTypes)
                {
                    var workItem = new WorkItem(workItemType);
                    foreach (string allowedUser in workItem.Fields[CoreField.AssignedTo].AllowedValues)
                    {
                        if (commonUsers.ContainsKey(allowedUser) == false)
                        {
                            commonUsers[allowedUser] = 1;
                        }
                        else
                        {
                            commonUsers[allowedUser] += 1;
                        }
                    }
                }

                var inAllTypes = tfsProject.WorkItemTypes.Count;
                return from commonUser in commonUsers where commonUser.Value == inAllTypes select commonUser.Key;
            }
        }

        public string CurrentDefaultAsignee
        {
            get { return defaultAsignee; }
            set { defaultAsignee = value; }
        }

        public IEnumerable<string> DefaultCreators
        {
            get 
            {
                return tfsUsers.Select(creator => creator.DisplayName);
            }
        }

        public string CurrentDefaultCreator
        {
            get { return defaultCreator; }
            set { defaultCreator = value; }
        }

        public bool CanImpersonate
        {
            get { return canImpersonate; }
        }

        public List<User> FailedImpersonations
        {
            get { return failedImpersonations.ToList(); }
        }

        public List<User> NoRightsToAdd
        {
            get { return unableToAddTickets.ToList(); }
        }

        public void AssignUser(User toAssign, WorkItem workItem)
        {
            if (toAssign.HasIdentity)
            {
                var assignedUser = (string) workItem[CoreField.AssignedTo];

                var matched = false;
                foreach (string tfsUser in workItem.Fields[CoreField.AssignedTo].AllowedValues)
                {
                    matched = toAssign.IsSameUser(tfsUser);
                    if (matched)
                    {
                        assignedUser = tfsUser;
                        break;
                    }
                }

                if (matched == false)
                {
                    if (String.IsNullOrWhiteSpace(defaultAsignee) == false)
                    {
                        assignedUser = defaultAsignee;
                    }
                    failedImpersonations.Add(toAssign);
                }
                workItem[CoreField.AssignedTo] = assignedUser;
            }
        }

        public event ImpersonationFailure OnFailedToImpersonate;

        private TeamFoundationIdentity getId(string tfsUser)
        {
            TeamFoundationIdentity id = null;
            if (String.IsNullOrEmpty(tfsUser) == false)
            {
                var ims = tfsProject.Tfs.GetService<IIdentityManagementService>();
                id = ims.ReadIdentity(IdentitySearchFactor.AccountName, tfsUser, MembershipQuery.None,
                    ReadIdentityOptions.None);
                if (id == null)
                {
                    id = ims.ReadIdentity(IdentitySearchFactor.DisplayName, tfsUser, MembershipQuery.None,
                        ReadIdentityOptions.None);
                }
            }
            return id;
        }

        public TfsTeamProjectCollection ImpersonateDefaultCreator()
        {
            return Impersonate(new User(defaultCreator, defaultCreator, ""));
        }

        public bool CanAddTicket(User toCheck)
        {
            var ableToAdd = true;
            if (unableToAddTickets.Contains(toCheck) == false)
            {
                var impersonated_User = Impersonate(toCheck);
                if (impersonated_User != null)
                {
                    var workItemStore = (WorkItemStore) impersonated_User.GetService(typeof (WorkItemStore));
                    if (workItemStore.Projects[tfsProject.Project].HasWorkItemWriteRights == false)
                    {
                        unableToAddTickets.Add(toCheck);
                        ableToAdd = false;
                    }
                }
            }
            return ableToAdd;
        }

        public TfsTeamProjectCollection Impersonate(User userToImpersonate)
        {
            TfsTeamProjectCollection impersonated_User = null;
            try
            {
                if (canImpersonate)
                {
                    if (userToImpersonate.IsSameUser(tfsProject.Tfs.AuthorizedIdentity.DisplayName) == false)
                    {
                        impersonated_Users.TryGetValue(userToImpersonate, out impersonated_User);
                        if (impersonated_User == null)
                        {
                            var tfsUser = defaultCreator;
                            foreach (var windowsUser in tfsUsers)
                            {
                                if (userToImpersonate.IsSameUser(windowsUser.GetAttribute("Mail", ""))
                                    || userToImpersonate.IsSameUser(windowsUser.DisplayName))
                                {
                                    tfsUser = windowsUser.DisplayName;
                                    break;
                                }
                            }

                            var toImpersonate = getId(tfsUser);
                            if (toImpersonate != null)
                            {
                                impersonated_User = new TfsTeamProjectCollection(tfsProject.Tfs.Uri,
                                    toImpersonate.Descriptor);
                                impersonated_Users[userToImpersonate] = impersonated_User;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OnFailedToImpersonate != null)
                {
                    OnFailedToImpersonate(
                        string.Format("Failed to impersonate '{0}'. ({1})", userToImpersonate.DisplayName, ex.Message));
                    return tfsProject.Tfs;
                }
            }
            return (impersonated_User?? tfsProject.Tfs);
        }

        public void ReleaseImpersonations()
        {
            foreach (var impersonatedUser in impersonated_Users.Values)
            {
                impersonatedUser.Dispose();
            }
            impersonated_Users.Clear();
        }

        public void Save()
        {
            var toSave = new Dictionary<string, string>();
            toSave[defaultAsigneeKey] = defaultAsignee;
            toSave[defaultCreatorKey] = defaultCreator;
            SettingsStore.Save(key, toSave);
        }

        #region private class variables

        private string defaultAsignee;
        private string defaultCreator;
        private const string key = "TFS_Users";
        private const string defaultAsigneeKey = "DefaultAsignee";
        private const string defaultCreatorKey = "DefaultCreator";
        private readonly HashSet<User> failedImpersonations;
        private readonly HashSet<User> unableToAddTickets;
        private readonly TfsProject tfsProject;
        private readonly TeamFoundationIdentity[] tfsUsers;
        private readonly bool canImpersonate;
        private readonly Dictionary<User, TfsTeamProjectCollection> impersonated_Users;

        #endregion

        #region Private TFS User utility methods
        private TeamFoundationIdentity[] allTfsUsers(TfsConnection tfs)
        {
            try
            {
                var idService = tfs.GetService<IIdentityManagementService>();
                var collectionWideValidUsers = idService.ReadIdentity(IdentitySearchFactor.DisplayName,
                                                                      "Project Collection Valid Users",
                                                                      MembershipQuery.Expanded,
                                                                      ReadIdentityOptions.None);
                var validMembers = idService.ReadIdentities(collectionWideValidUsers.Members,
                                                            MembershipQuery.Expanded,
                                                            ReadIdentityOptions.ExtendedProperties);
                return validMembers.Where(_ => !_.IsContainer && _.Descriptor.IdentityType != "Microsoft.TeamFoundation.UnauthenticatedIdentity").ToArray();
            }
            catch
            {
                /* Do nothing */
            }

            return  (tfs != null? new TeamFoundationIdentity[] { tfs.AuthorizedIdentity } : new TeamFoundationIdentity[0]);
        }

        private bool ableToImpersonate(TfsConnection tfs)
        {
            var ableToImpersonate = false;
            try
            {
                var ims = tfs.GetService<IIdentityManagementService>();
                foreach (var user in tfsUsers)
                {
                    var toImpersonate = ims.ReadIdentity(IdentitySearchFactor.DisplayName, user.DisplayName, MembershipQuery.None, ReadIdentityOptions.None);
                    if (toImpersonate != null)
                    {
                        if (string.CompareOrdinal(tfs.AuthorizedIdentity.DisplayName, toImpersonate.DisplayName) != 0)
                        {
                            using (var tfs_impersonated = new TfsTeamProjectCollection(tfs.Uri, toImpersonate.Descriptor))
                            {
                                // Will raise an exception if impersonation failed.
                                var authorised = tfs_impersonated.AuthorizedIdentity.DisplayName;
                                ableToImpersonate = true;
                            }
                            break;
                        }
                    }
                }
            }
            catch /*(AccessCheckException ex)*/
            {
                // If impersonation fails, it will throw an AccessCheckException..
                ableToImpersonate = false;
            }
            return ableToImpersonate;
        }

        #endregion
    }
}