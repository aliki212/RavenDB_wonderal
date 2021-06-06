using Raven.Client.Documents;
using Raven.Client.Documents.Session;
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
        //validOperationException: The supplied  certificate contains no private key.Constructing the certificate with the 'X509KeyStorageFlags.MachineKeySet' flag may solve this problem.

        //private static readonly IDocumentStore store = new DocumentStore
        //{
        //    Urls = new[] { "https://a.wonderal.ravendb.community/"},
        //    Database = "Pawns",
        //    Certificate = clientCertificate,
        //    Conventions = { }
        //}.Initialize();

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

            //using (IDocumentSession session = store.OpenSession())
            //{
            //    // Create a new entity
            //    Company entity = new Company { Name = "Company2" };
            //    // Mark the entity for storage in the Session.
            //    session.Store(entity);
            //    // Any changes made to the entity will now be tracked by the Session.
            //    // However, the changes are persisted to the server only when 'SaveChanges()' is called.
            //    session.SaveChanges();
            //    // At this point the entity is persisted to the database as a new document.
            //    // Since no database was specified when opening the Session, the Default Database is used.
            //}

            using (var store = CreateDocumentStore())
            {
                Utils.StartDemo(store);
            }
        }
    }
}
