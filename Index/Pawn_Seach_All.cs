using Raven.Client.Documents.Indexes;
using RavenDB_wonderal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB_wonderal.Index
{
    public class Pawn_Seach_All : AbstractIndexCreationTask<Pawn>
    {
        public class Result
        {
            public string Query;
        }
        public Pawn_Seach_All()
        {
            Map = pawns =>
            from pawn in pawns
            select new
            {
                Query = new object[]
                {
                    pawn.Name, pawn.Email, pawn.Biography
                    //,                    pawn.Email.Split(("@", StringSplitOptions.RemoveEmptyEntries);
                }
            };
            Index(x => x, FieldIndexing.Search);
        }
        //Indexes = {{ x => x.Tags, FieldIndexing.Analyzed }}
        //        }.ToIndexDefinition(store.Conventions));
    }
}
