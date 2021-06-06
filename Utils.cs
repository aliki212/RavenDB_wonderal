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
    }
}
