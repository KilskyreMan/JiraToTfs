using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TicketImporter
{
    public class User
    {
        public User()
            : this("", "", "")
        {
        }

        public User(string displayName, string name, string eMail)
        {
            DisplayName = displayName;
            Name = name;
            this.eMail = eMail;
        }

        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string eMail { get; set; }

        public bool HasIdentity
        {
            get { return (String.IsNullOrWhiteSpace(Name) == false ? true : false); }
        }

        public bool IsSameUser(string user)
        {
            bool isSame = false,
                isEmail = Regex.IsMatch(user,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            if (isEmail != true)
            {
                char[] deliminators = {' ', ',', '.'};

                var nameConstituents = (from item in Name.Split(deliminators)
                                        where string.IsNullOrWhiteSpace(item) == false select item.ToUpper()).ToList();

                var userConstituents = (from item in user.Split(deliminators)
                                        where string.IsNullOrWhiteSpace(item) == false select item.ToUpper()).ToList();

                if (nameConstituents.Count == userConstituents.Count)
                {
                    var matches =
                        new List<string>(nameConstituents.Intersect(userConstituents));
                    if (matches.Count == nameConstituents.Count)
                    {
                        isSame = true;
                    }
                }
            }
            else
            {
                isSame = string.Compare(eMail, user, StringComparison.OrdinalIgnoreCase) == 0;
            }

            return isSame;
        }

        public override bool Equals(object obj)
        {
            var isEqual = false;
            var toCompare = obj as User;
            if (toCompare != null)
            {
                isEqual = (String.Compare(Name, toCompare.Name, true) == 0 ? true : false);
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}