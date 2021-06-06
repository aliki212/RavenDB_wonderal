using Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB_wonderal.Domain
{
    public class DomainUtils
    {
        public static  IList<Pawn>BuildSamplePawns(int numberOfPawns)
        {
            var pawns = new List<Pawn>();
            for (int i=0; i<= numberOfPawns; i++)
            {
                var p = GeneratePawn(i +201);
                UpdatePawnProperties(p);
                pawns.Add(p);
            }
            return pawns;
        }

        public static Pawn GeneratePawn(int id)
        {
            return new Pawn()
            {
                Id = id.ToString(),
                Name = Lorem.Sentence(3),
                Email = Lorem.GetFirstWord(),
                Biography = Lorem.Paragraph(3)
            };
        }

        public static void UpdatePawnProperties(Pawn pawn)
        {
            var traits = new List<string>();
            var number = RandomNumber.Next(0, 5);
            if(number>=1)
             traits.Add("Gambler"); 
            if (number >= 2)
            traits.Add("Toxic");
            if (number >= 3)
            traits.Add("Pessimist");
            if (number >= 4)
            traits.Add("Nudist");
            if (number >= 5)
            traits.Add("Conservative");
            pawn.Traits = traits;
        }
    }
}
