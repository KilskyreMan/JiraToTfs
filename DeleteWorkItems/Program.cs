using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace DeleteWorkItems
{
    internal class Program
    {
        private static void displayHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("     To delete *ALL* WorkItems found in project 'ACME' in TFS collection 'SandBox': ");
            Console.WriteLine("        DeleteWorkItems /project=http://tfsserver:8080/Tfs/SandBox/ACME");
            Console.WriteLine("");
            Console.WriteLine("     To delete *SELECTED* WorkItems from project 'ACME', in TFS collection 'SandBox': ");
            Console.WriteLine(
                "        DeleteWorkItems /project=http://tfsserver:8080/Tfs/SandBox/ACME /Ids=c:\\WorkItemIds.csv");
            Console.WriteLine("        (Sample CSV: 10023, 10024, .., 10045)");
            Console.WriteLine("");
            Console.WriteLine("NOTE: Use wisely as deletion is non-reversible.");
        }

        private static int Main(string[] args)
        {
            if (args.Length < 1 || String.Compare(args[0], "?") == 0 || String.Compare(args[0], "help", true) == 0)
            {
                displayHelp();
                return 0;
            }

            string teamProject = "",
                collection = "",
                fullProjectName = "",
                csvFile = "";
            foreach (var arg in args)
            {
                var param = arg.ToUpper();
                if (param.IndexOf("/PROJECT=") == 0)
                {
                    param = param.Substring(param.IndexOf('=') + 1);
                    teamProject = param.Substring(param.LastIndexOf('/') + 1);
                    collection = param.Substring(0, param.LastIndexOf('/'));
                    fullProjectName = arg;
                }

                if (param.IndexOf("/IDS=") == 0)
                {
                    csvFile = param.Substring(param.IndexOf('=') + 1);
                }
            }

            if (string.IsNullOrWhiteSpace(teamProject) || string.IsNullOrWhiteSpace(collection))
            {
                Console.WriteLine("");
                Console.WriteLine("* Invalid parameters specified. Review 'help' and try again. *");
                Console.WriteLine("");
                displayHelp();
                return 0;
            }

            try
            {
                var tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(collection));
                if (tfs != null)
                {
                    Console.WriteLine("");
                    if (string.IsNullOrWhiteSpace(csvFile))
                    {
                        Console.WriteLine("Are you sure you wish to remove all work-items found under");
                    }
                    else
                    {
                        Console.WriteLine("Are you sure you wish to remove selected work-items declared in");
                        Console.WriteLine("    " + csvFile);
                        Console.WriteLine("from under");
                    }
                    Console.WriteLine("    " + fullProjectName);
                    Console.Write("'Y' to continue, any other key to abort: ");

                    var input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Y)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("This could take some time, please wait.");

                        int[] toDelete = null;
                        var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));

                        if (string.IsNullOrWhiteSpace(csvFile))
                        {
                            Console.WriteLine("    * Querying TFS project '{0}' for work-items to delete.", teamProject);
                            var queryResults = workItemStore.Query("Select [ID] From WorkItems");
                            toDelete = (from WorkItem workItem in queryResults select workItem.Id).ToArray();
                        }
                        else if (File.Exists(csvFile))
                        {
                            Console.WriteLine("    * Deleting work-items described in '{0}'.", csvFile);
                            var ids = File.ReadAllText(csvFile);
                            var toParse = ids.Split(',');
                            int parsed;
                            var parsedIds = new List<int>();
                            foreach (var n in toParse)
                            {
                                if (int.TryParse(n, out parsed))
                                {
                                    parsedIds.Add(parsed);
                                }
                            }
                            toDelete = parsedIds.ToArray();
                        }

                        if (toDelete != null && toDelete.Count() > 0)
                        {
                            var errors = workItemStore.DestroyWorkItems(toDelete);
                            var inError = (errors != null ? errors.Count() : 0);
                            if (inError > 0)
                            {
                                Console.Write("    * Failed to delete {0} WorkItems!", errors.Count());
                                foreach (var error in errors)
                                {
                                    Console.Write("        {0} - {1}", error.Id, error.Exception.Message);
                                }
                            }
                            else
                            {
                                Console.Write("    * Deleted {0} work-items.", toDelete.Count());
                            }
                            workItemStore.RefreshCache();
                            workItemStore.SyncToCache();
                        }
                        else
                        {
                            Console.WriteLine("No work-items found to delete in project '{0}'.", teamProject);
                        }
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Phew, aborting delete, no damage done.");
                    }
                }
                else
                {
                    Console.WriteLine("No work-items deleted, unable to connect to TFS. Check URI specified.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("Failed to delete work-items.");
                Console.Write(ex.Message);
                return -1;
            }

            return 0;
        }
    }
}