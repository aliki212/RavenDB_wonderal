using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Session;
using RavenDB_wonderal.Index;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB_wonderal
{
    class Program
    {
        static readonly X509Certificate2 clientCertificate = new X509Certificate2("C:\\Users\\Ali\\Downloads\\wonderal.Cluster.Settings\\admin.client.certificate.wonderal.pfx");

        private static IDocumentStore CreateDocumentStore()
        { return new DocumentStore
            {
                Urls = new[] { "https://a.wonderal.ravendb.community/" },
                Database = "Pawns",
                Certificate = clientCertificate,
                Conventions = { }
            }.Initialize();
        }

        static void Main(string[] args)
        {
            using (var store = CreateDocumentStore())
            {
                IndexCreation.CreateIndexes(typeof(Pawns_Search).Assembly, store);
                IndexCreation.CreateIndexes(typeof(Pawn_Seach_All).Assembly, store); ;

                Utils.StartDemo(store);
            }
        }
    }
}
