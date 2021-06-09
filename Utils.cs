using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Operations.Indexes;
using Raven.Client.Documents.Session;
using RavenDB_wonderal.Domain;
using RavenDB_wonderal.Index;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB_wonderal
{
    public class Utils
    {
        public static void StartDemo(IDocumentStore store)
        {
            int selection = 0;
            do
            {
                int.TryParse(Common.ShowMenu(), out selection);

                if (selection == 1) // initialize all
                {
                    InitializeSampleData(store);
                }
                if (selection == 2) // search text
                {
                    SearchPawns(store);
                }          
                if (selection == 3) // search email
                {
                    SearchEmailPawn(store);
                }
                if (selection == 4) // get 1
                {
                    GetAPawn(store);
                }
                if (selection == 5) // delete 1
                {
                    DeleteAPawn(store);
                }
                if (selection == 6) // delete all
                {
                    DeleteAllPawns(store);
                }
            } while (selection > 0);
        }

        public static void InitializeSampleData(IDocumentStore store)
        {
            int pawnsToGenerate = 0;
            
            AnsiConsole.Markup("[yellow]Enter how many pawns you want to generate:[/] ");
            AnsiConsole.Markup("([underline yellow]Hint:[/] Default is 100) : ");

            var inputText =  Console.ReadLine().Trim();
            if (String.IsNullOrWhiteSpace(inputText))
                pawnsToGenerate = 100;
            else
            {
                pawnsToGenerate = Convert.ToInt32(inputText);
            }
            var pawns = DomainUtils.BuildSamplePawns(pawnsToGenerate);
            using (var session = store.OpenSession())
            {
                foreach(Pawn pawn in pawns)
                {
                    session.Store(pawn, null); //Before the id was stored in the metadata part of the document - if added in this parameter will it change?
                }
                session.SaveChanges();
            }
            Console.WriteLine($"List of {pawnsToGenerate} Pawns has been successfully been generated");
            Console.ReadKey();
            Console.Clear();
        }

        public static void SearchPawns(IDocumentStore store)
        {
            Console.WriteLine("---" + Environment.NewLine + "Enter your text to search all text properties   **Hint** Leave empty for full list");
            var inputText = Console.ReadLine().Trim();

            var pawns = Enumerable.Empty<Pawn>();

            using (var session = store.OpenSession())
            {
                var query = session.Query<Pawn>();              
                if (String.IsNullOrWhiteSpace(inputText))
                {
                    pawns = query.ToList();
                    Common.ViewPawnTable(pawns, 80);
                    Console.ReadKey();

                }
                else
                {
                     pawns = session
                    .Query<Pawn>()
                    .Search(x => x.Name, inputText)
                    .Search(x => x.Email, inputText)
                    .Search(x => x.Biography, inputText)
                    .ToList();
                    Common.ViewPawnTable(pawns, 1000);
                    Console.ReadKey();

                }
            }

            Console.WriteLine(@"Results of the search: {0} items", pawns.Count());

            Console.ReadKey();
            Console.Clear();

        }

        public static void SearchEmailPawn(IDocumentStore store)
        {
            Console.WriteLine("---" + Environment.NewLine + "Enter your text to search the email properties of the Pawns");
            var inputText = Console.ReadLine().Trim();

            var pawns = Enumerable.Empty<Pawn>();
            //store.Maintenance.Send(new StartIndexOperation("Pawns/Email"));
            //store.DatabaseCommands//.PutIndex("Pawns/Email",
            //            new IndexDefinitionBuilder<Pawn>
            //            {
            //                Map = posts => from post in posts
            //                               select new { post.Title }
            //            });

            using (var session = store.OpenSession())
            {
                var query = session.Query<Pawn>();
                if (inputText == null)
                Console.WriteLine("Please try again");
                else
                {
                    // pawns = session
                    //.Query<Pawn>()//.Where(x =>x.Email.Contains(inputText))
                    //.Search(x => x.Email, inputText )//options: SearchOptions.Or, Raven.Client.EcapeQueryOptions: EscapeQueryOptions.AllowAllWildcards)
                    //.ToList();
                    pawns = session.Query<Pawn_Seach_All.Result, Pawn_Seach_All>()
                         .Where(p => p.Query == inputText)
                         .OfType<Pawn>()
                         .ToList();
                }
            }
            if (pawns.Count() > 0)
            {
                Console.WriteLine(@"Results of the search: {0} items", pawns.Count());
                if (pawns.Count()>50)
                Common.ViewPawnTable(pawns, 1000);
                else
                Common.ViewPawnTable(pawns, 100);
                Console.ReadKey();
                Console.Clear();
            }
            else
                Console.WriteLine("Please try again");

        }

        public static void GetAPawn(IDocumentStore store)
        {
            Console.WriteLine("---" + Environment.NewLine + "Enter the Pawn Id to delete");
            var inputText = Console.ReadLine().Trim();
            Pawn pawnTemp = new Pawn();

            using (var session = store.OpenSession())
            {
                pawnTemp = session.Query<Pawn>().Where(p => p.Id == inputText).FirstOrDefault();
                session.SaveChanges();
            }
            Console.WriteLine($"Id: {pawnTemp.Id} \tName:{pawnTemp.Name} \tEmail:{pawnTemp.Email} \nBiography:{pawnTemp.Biography}");
            Console.ReadKey();
            Console.Clear();
        }


        public static void DeleteAPawn(IDocumentStore store)
        {
            Console.WriteLine("-- -" + Environment.NewLine + "Enter the Pawn Id to delete");
            var inputText = Console.ReadLine().Trim();
            Pawn pawn = new Pawn();
            
            using (var session = store.OpenSession())
            {
                pawn = session.Query<Pawn>().Where(p => p.Id == inputText).FirstOrDefault();
                session.Delete(pawn);
                
                session.SaveChanges();
            }
            Console.WriteLine(@"Successfully deleted Pawn with Name : {0} ", pawn.Name);
            Console.ReadKey();
            Console.Clear();
        }

        public static void DeleteAllPawns(IDocumentStore store)
        {
            var pawns = Enumerable.Empty<Pawn>();
            int count = 0;
            using (var session = store.OpenSession())
            {
                pawns = session.Query<Pawn>().ToList();
                count = pawns.Count();

                foreach (var pawn in pawns)
                {
                    session.Delete(pawn);
                }
                session.SaveChanges();
            }
            Console.WriteLine(@"Deleted {0} items", count);
            Console.ReadKey();
            Console.Clear();
        }

    }
}
