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
            Pawn newPawn = new Pawn();
            
                newPawn.Id = id.ToString();
                newPawn.Name = Faker.Name.FullName(NameFormats.WithPrefix);
                newPawn.Email = Faker.Internet.Email(newPawn.Name);
                newPawn.Biography = Lorem.Paragraph(1);
                return newPawn;
        }
        

        //public static Pawn GeneratePawn(int id)
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    FileStream lfile = new FileStream(@"C:\TTemp\Backstories_REBAdult_Basic.xml", FileMode.Open);
        //    Pawn newPawn = new Pawn();

        //    //var number = RandomNumber.Next(0, 100);

        //    //if (number % 2 == 0) //chance of male or female
        //    //{
        //    //    newPawn.Gender = "male";
        //    //    lfile = new FileStream("C:\\TTemp\\MeleBio.xml", FileMode.Open);
        //    //}
        //    //else
        //    //{
        //    //    newPawn.Gender = "female";
        //    //    lfile = new FileStream("C:\\TTemp\\FemaleBio.xml", FileMode.Open);
        //    //}

        //    xmlDocument.Load(lfile);
        //    string bio = "";
        //    XmlNodeList list = xmlDocument.GetElementsByTagName("baseDesc");
        //    var number = RandomNumber.Next(0, list.Count);

        //    XmlElement chosenBio = (XmlElement)xmlDocument.GetElementsByTagName("baseDesc")[number];

        //    bio = chosenBio.InnerText;
        //    newPawn.Id = id.ToString();
        //    newPawn.Name = Faker.Name.FullName(NameFormats.WithPrefix);
        //    newPawn.Email = Faker.Internet.Email(newPawn.Name);
        //    newPawn.Biography = bio;
        //    return newPawn;
        //}


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
