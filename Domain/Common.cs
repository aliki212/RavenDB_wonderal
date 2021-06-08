using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RavenDB_wonderal.Domain
{
    public class Common
    {
        public static string ShowMenu()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("|| Select Menu ||")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] { 
                    "1 - Initialize sample data" + Emoji.Known.HundredPoints,
                    "2 - Search for Pawns" + Emoji.Known.Trolleybus,
                    "3 - Search for email" + Emoji.Known.EMail,
                    "4 - Find a Pawn" + Emoji.Known.Merperson,
                    "5 - Delete a Pawn" + Emoji.Known.Fire,
                    "6 - Delete ALL Pawns" + Emoji.Known.Fireworks
                }));

            // Echo the fruit back to the terminal
            AnsiConsole.WriteLine($"I agree. {choice} is tasty!");
            //Console.WriteLine(
            //    "---" + Environment.NewLine +
            //    "|| Select Menu ||" + Environment.NewLine +
            //    "1 - Initialize sample data" + Environment.NewLine +
            //    "2 - Search for Pawns" + Environment.NewLine +
            //    "3 - Search for email" + Environment.NewLine +
            //    "4 - Update a Pawn" + Environment.NewLine +
            //    "5 - Delete a Pawn" + Environment.NewLine +
            //    "6 - Delete ALL Pawns" + Environment.NewLine  
            //    );

            
            string result = choice.Substring(0, 1);
            return result;//Console.ReadLine();
        }

        public static string ShowMenuEdit()
        {
            Console.WriteLine("---" + Environment.NewLine + "Choose which property to edit" 
                 + Environment.NewLine + "1 -- Type 1 for Name" 
                 + Environment.NewLine + "2 -- Type 2 for Email"
                 + Environment.NewLine + "3 -- Type 3 for Biography");
            
            return Console.ReadLine().Trim();
        }
        
        public static void ViewPawnTable(IEnumerable<Pawn> pawns)
        {
            var table = new Table().Centered();
            
            // Add some columns
           // table.AddColumn("Id");
           // table.AddColumn(new TableColumn("[blue]Name[/]").Centered());
           // table.AddColumn(new TableColumn("Email").Centered());
           // table.AddColumn(new TableColumn("Biography").Centered());
           // foreach (Pawn pawn in pawns)
           // {

           // // Add some rows
           // table.AddRow(pawn.Id, "[green]"+pawn.Name+"[/]", pawn.Email, pawn.Biography);
           //// table.AddRow(new Markup("[blue]Corgi[/]"), new Panel("Waldo"));
           // }

            // Render the table to the console
            //AnsiConsole.Render(table);

            AnsiConsole.Live(table)
            .AutoClear(false)   // Do not remove when done
            .Overflow(VerticalOverflow.Ellipsis) // Show ellipsis when overflowing
            .Cropping(VerticalOverflowCropping.Top) // Crop overflow at top
            .Start(ctx =>
            {
                table.AddColumn("Id");
                ctx.Refresh();
                Thread.Sleep(1000);
                table.AddColumn(new TableColumn("[blue]Name[/]").Centered());
                ctx.Refresh();
                Thread.Sleep(1000);
                table.AddColumn(new TableColumn("Email").Centered());
                ctx.Refresh();
                Thread.Sleep(1000);
                table.AddColumn(new TableColumn("Biography").Centered());
                ctx.Refresh();
                Thread.Sleep(1000);
                foreach (Pawn pawn in pawns)
                {
                    table.AddRow(pawn.Id, "[green]" + pawn.Name + "[/]", pawn.Email, pawn.Biography);
                    ctx.Refresh();
                    Thread.Sleep(1000); 
                }

            });
        }
    }
}
