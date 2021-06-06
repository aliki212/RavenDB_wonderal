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

                if (selection == 1)
                {
                    InitializeSampleData(store);
                }
                if (selection == 2)
                {
                    SearchPawns(store);
                }
                
                if (selection == 3)
                {
                    
                }
                if (selection == 4)
                {

                }
                if (selection == 5)
                {
                    DeleteAPawn(store);
                }
                if (selection == 6)
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
            Console.WriteLine("---" + Environment.NewLine + "Enter your text to search    **Hint** Leave empty for full list");
            var inputText = Console.ReadLine().Trim();

            var pawns = Enumerable.Empty<Pawn>();

            using (var session = store.OpenSession())
            {
                pawns = session.Query<Pawn>().ToList();
                
            }
            Console.WriteLine(@"Results of the search: {0} items", pawns.Count());
            foreach(Pawn pawn in pawns)
            {
                Console.WriteLine(@"Id: {0} \tName:{1} \tEmail:{2} \tBiography:{3}", pawn.Id, pawn.Name, pawn.Email, pawn.Biography);
                //Console.WriteLine($"Id: " + pawn.Id + " \tName:{1} " + pawn.Name + "  \tEmail:{2} " + pawn.Email + " \tBiography:{3}" + pawn.Biography);//+ ", pawn.Id, pawn.Name, pawn.Email, pawn.Biography);

            }

        }

        public static void DeleteAPawn(IDocumentStore store)
        {
            Console.WriteLine("---" + Environment.NewLine + "Enter the Pawn Id to delete");
            var inputText = Console.ReadLine().Trim();
            Pawn pTemp = new Pawn();
            
            using (var session = store.OpenSession())
            {
                pTemp = session.Query<Pawn>().Where(p => p.Id == inputText).FirstOrDefault();
                session.Delete(pTemp.Id);
                
                session.SaveChanges();
            }
            Console.WriteLine(@"Successfully deleted Pawn with Name : {0} ", pTemp.Name);
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
