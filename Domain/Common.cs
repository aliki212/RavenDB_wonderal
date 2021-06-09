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
                    "1 > Initialize sample data",
                    "2 > Search for Pawns",
                    "3 > Search for email",
                    "4 > Find a Pawn",
                    "5 > Delete a Pawn",
                    "6 > Delete ALL Pawns" 
                }));
          
            string result = choice.Substring(0, 1);
            return result;
        }

        
        public static void ViewPawnTable(IEnumerable<Pawn> pawns, int delay)
        {
            var table = new Table().Centered();
            
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
                table.AddColumn(new TableColumn("[green]Email[/]").Centered());
                ctx.Refresh();
                Thread.Sleep(1000);
                table.AddColumn(new TableColumn("[yellow]Biography[/]").Centered());
                ctx.Refresh();
                Thread.Sleep(1000);
                foreach (Pawn pawn in pawns)
                {
                    table.AddRow(pawn.Id, "[blue]" + pawn.Name + "[/]", "[green]" + pawn.Email + "[/]", "[yellow]" + pawn.Biography + "[/]");
                    ctx.Refresh();
                    Thread.Sleep(delay); 
                }

            });
        }
    }
}
