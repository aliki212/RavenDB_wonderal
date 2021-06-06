using Raven.Client.Documents;
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

                }
                if (selection == 6)
                {

                }
            } while (selection > 0);
        }

        public static void InitializeSampleData(IDocumentStore store)
        {
            var pawns = DomainUtils.BuildSamplePawns(150);
            using (var session = store.OpenSession())
            {
                foreach(Pawn pawn in pawns)
                {
                    session.Store(pawn);
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
                //Console.WriteLine($"Id: {0} \tName:{1} \tEmail:{2} \tBiography:{3}", pawn.Id, pawn.Name, pawn.Email, pawn.Biography);
                Console.WriteLine($"Id: " + pawn.Id + " \tName:{1} " + pawn.Name + "  \tEmail:{2} " + pawn.Email + " \tBiography:{3}" + pawn.Biography);//+ ", pawn.Id, pawn.Name, pawn.Email, pawn.Biography);

            }

        }
    }
}
