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