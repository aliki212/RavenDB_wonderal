using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB_wonderal.Domain
{
    public class Common
    {
        public static string ShowMenu()
        {
            Console.WriteLine(
                "---" + Environment.NewLine +
                "|| Select Menu ||" + Environment.NewLine +
                "1 - Initialize sample data" + Environment.NewLine +
                "2 - Search for Pawns" + Environment.NewLine +
                "3 - Get a Pawn" + Environment.NewLine +
                "4 - Update a Pawn" + Environment.NewLine +
                "5 - Delete a Pawn" + Environment.NewLine +
                "6 - Delete ALL Pawns" + Environment.NewLine  
                );
            return Console.ReadLine();
        }
    }
}
