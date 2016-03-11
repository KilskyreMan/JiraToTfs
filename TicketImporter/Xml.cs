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

namespace TicketImporter
{
    internal static class Xml
    {
        private static readonly List<Tuple<string, char>> escapeStrings = new List<Tuple<string, char>>
        {
            new Tuple<string, char>("_a_", '&'),
            new Tuple<string, char>("_s_", ' '),
            new Tuple<string, char>("_lb_", '('),
            new Tuple<string, char>("_rb_", ')'),
            new Tuple<string, char>("_gt_", '>'),
            new Tuple<string, char>("_lt_", '<')
        };

        public static string Escape(string toFormat)
        {
            var formatted = toFormat;
            foreach (var p in escapeStrings)
            {
                long result;
                if (long.TryParse(formatted, out result) == false)
                {
                    if (formatted.IndexOf("_n_", StringComparison.Ordinal) == 0)
                    {
                        formatted = formatted.Substring(formatted.IndexOf("_n_", StringComparison.Ordinal) + 3);
                    }
                    else
                    {
                        string escapedChar = p.Item1,
                            charToEscape = p.Item2.ToString();
                        if (formatted.Contains(charToEscape))
                        {
                            formatted = formatted.Replace(charToEscape, escapedChar);
                        }
                        else if (formatted.Contains(escapedChar))
                        {
                            formatted = formatted.Replace(escapedChar, charToEscape);
                        }
                    }
                }
                else
                {
                    formatted = string.Format("{0}{1}", "_n_", formatted);
                }
            }
            return formatted;
        }
    }
}