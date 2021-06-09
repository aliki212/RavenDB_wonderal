using Raven.Client.Documents.Indexes;
using RavenDB_wonderal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB_wonderal.Index
{
    public class Pawns_Search : AbstractIndexCreationTask<Pawn>
    {
        public class Result
        {
            public string Query;
        }
        public Pawns_Search()
        {
            Map = pawns =>
            from pawn in pawns
            select new
            {
                pawn.Email
            };
            Index(x => x.Email, FieldIndexing.Search);
        }
    }
}
