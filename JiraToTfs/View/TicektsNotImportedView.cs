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
using System.Windows.Forms;
using TicketImporter.Interface;

namespace JiraToTfs.View
{
    public delegate void ShowCustomFieldMappings();

    public partial class TicektsNotImportedView : Form
    {
        public ShowCustomFieldMappings OnShowCustomFieldMappings;

        public TicektsNotImportedView(List<IFailedTicket> failedTickets)
        {
            InitializeComponent();

            foreach (var ticket in failedTickets)
            {
                var ticketNode = new TreeNode(ticket.Summary);
                ticketNode.Nodes.Add("Type: " + ticket.Type);
                ticketNode.Nodes.Add("Title: " + ticket.Title);
                foreach (var field in ticket.Issues)
                {
                    var fieldNode = new TreeNode(field.Problem);
                    fieldNode.Nodes.Add(new TreeNode("Value: " + field.Value));

                    if (field.Info.Count > 0)
                    {
                        var infoNode = new TreeNode("Info");
                        foreach (var fieldInfo in field.Info)
                        {
                            infoNode.Nodes.Add(new TreeNode(fieldInfo));
                        }
                        fieldNode.Nodes.Add(infoNode);
                    }
                    ticketNode.Nodes.Add(fieldNode);
                }
                failedTicketTree.Nodes.Add(ticketNode);
            }
        }

        private void skipAndContinueBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fieldMappingsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (OnShowCustomFieldMappings != null)
            {
                OnShowCustomFieldMappings();
            }
        }
    }
}