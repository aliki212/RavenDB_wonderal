using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using RavenDB_wonderal.Domain;
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
                if (selection == 4) // update
                {
                    GetAPawn(store);
                    //UpdateAPawn(store);
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
            var pawns = DomainUtils.BuildSamplePawns(10);
            using (var session = store.OpenSession())
            {
                foreach(Pawn pawn in pawns)
                {
                    session.Store(pawn, null); //Before the id was stored in the metadata part of the document - if added in this parameter will it change?
                }
                session.SaveChanges();
            }
            Console.WriteLine("List of Pawns has been successfully been generated");

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
                pawns = query.ToList();
                else
                {
                     pawns = session
                    .Query<Pawn>()
                    .Search(x => x.Name, inputText)
                    .Search(x => x.Email, inputText)
                    .Search(x => x.Biography, inputText)
                    .ToList();
                }
            }
            Console.WriteLine(@"Results of the search: {0} items", pawns.Count());
            foreach(Pawn pawn in pawns)
            {
                Console.WriteLine($"Id: {pawn.Id} \tName:{pawn.Name} \tEmail:{pawn.Email} \nBiography:{pawn.Biography}");
            }
        }

        public static void SearchEmailPawn(IDocumentStore store)
        {
            Console.WriteLine("---" + Environment.NewLine + "Enter your text to search the email properties of the Pawns");
            var inputText = Console.ReadLine().Trim();

            var pawns = Enumerable.Empty<Pawn>();

            using (var session = store.OpenSession())
            {
                var query = session.Query<Pawn>();
                if (inputText == null)
                Console.WriteLine("Please try again");
                else
                {
                    pawns = session
                   .Query<Pawn>()
                   .Search(x => x.Email, inputText)
                   .ToList();
                }
            }
            if (pawns.Count() > 0)
            {
                Console.WriteLine(@"Results of the search: {0} items", pawns.Count());
                foreach (Pawn pawn in pawns)
                {
                    Console.WriteLine($"Id: {pawn.Id} \tName:{pawn.Name} \tEmail:{pawn.Email} \nBiography:{pawn.Biography}");
                }
            }
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
        }


        public static void UpdateAPawn(IDocumentStore store)
        {
            Console.WriteLine("---" + Environment.NewLine + "Enter the Pawn Id to update");
            var inputTextId = Console.ReadLine().Trim();
            Pawn pawnTemp = new Pawn();
            using (var session = store.OpenSession())
            {
                pawnTemp = session.Query<Pawn>().Where(p => p.Id == inputTextId).FirstOrDefault();
            }
            Console.WriteLine($"Details for Pawn deleted Pawn Id: {pawnTemp.Id} \tName:{pawnTemp.Name} \tEmail:{pawnTemp.Email} \nBiography:{pawnTemp.Biography}");

            string inputEditChoice = Common.ShowMenuEdit();
            int selection = 0;
            do
            {
                int.TryParse(Common.ShowMenu(), out selection);

                if (selection == 1) // initialize all
                {
                    // ChangeName();
                }
                if (selection == 2) // search
                {
                    // ChangeEmail();
                }
                if (selection == 3) // get 1
                {
                    // ChangeBiography();
                }
            } while (selection > 0);
        }

        public static void DeleteAPawn(IDocumentStore store)
        {
            Console.WriteLine("-- -" + Environment.NewLine + "Enter the Pawn Id to delete");
            var inputText = Console.ReadLine().Trim();
            Pawn pawnTemp = new Pawn();
            
            using (var session = store.OpenSession())
            {
                pawnTemp = session.Query<Pawn>().Where(p => p.Id == inputText).FirstOrDefault();
                session.Delete(pawnTemp.Id);
                
                session.SaveChanges();
            }
            Console.WriteLine(@"Successfully deleted Pawn with Name : {0} ", pawnTemp.Name);
        }

        public static void DeleteAllPawns(IDocumentStore store)
        {
            var pawns = Enumerable.Empty<Pawn>();
            int count = 0;
            using (var session = store.OpenSession())
            {
                pawns = session.Query<Pawn>().ToList();
                count = pawns.Count();

                foreach (var p in pawns)
                {
                    session.Delete(p.Id);
                }
                session.SaveChanges();
            }
            Console.WriteLine(@"Deleted {0} items", count);
        }
    }
}
