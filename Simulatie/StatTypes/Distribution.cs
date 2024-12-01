using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Serilog;
using Serilog.Debugging;

namespace Simulatie.StatTypes
{
    public class Distribution : IStatType
    {
        public int TypeNum { get; } = 2;
        public int Id { get; }
        public string Value { get; set; }
        public int Role { get; }
        public Distribution(int id, SimulationDatabaseContext db)
        {
            Id = id;
            var me_in_db = db.Statistics.Find(Id);
            Value = me_in_db.Value;
            Role = me_in_db.Role;

        }
        public void AskForValueInput(SimulationDatabaseContext db, IGuiMode? guiMode, string message)
        {
            Log.Information("Please enter a few numbers for the value of this statistic. Type = {type}, Role = {role}", TypeNum, Role);
            List<int> value = new List<int>();
            while (true)
            {
                string input;
                int parsed;
                int rarity;
                if (guiMode != null)
                {
                    input = guiMode.InputBox(message, "Enter a value or enter `done` to exit.", "0");
                }
                else
                {
                    input = Console.ReadLine()!;
                }

                if (input == "done")
                {
                    break;
                }
                else if (int.TryParse(input, out parsed))
                {
                    Log.Information("Ok! Value {val} will have a rarity of ...", parsed);
                    while (true)
                    {
                        if (guiMode != null)
                        {
                            input = guiMode.InputBox(message, "Enter a rarity.", "1");
                        }
                        else
                        {
                            input = Console.ReadLine()!;
                        }
                        if (int.TryParse(input, out rarity))
                        {
                            Log.Information("Okay! Value {val} with rarity {rar}.", parsed, rarity);
                            break;
                        }
                        else
                        {
                            Log.Error("Woopsies. Can't do that. Please try again.");
                        }
                    }

                    for (int i = 0; i <= rarity; i++)
                    {
                        value.Add(parsed);
                    }
                    continue;
                }
                else
                {
                    Log.Error("Please enter a valid number. Can NOT parse '{input}' as int. Try again!", input);
                }
            }
            string value_to_store = string.Join(";", value.ToArray());
            Log.Information("Value will be {val_to_store}.", value_to_store);
            var me_in_db = db.Statistics.Find(Id);
            me_in_db!.Value = value_to_store;
            Value = value_to_store;
            db.SaveChanges();

        }
        public int GetNumber()
        {
            string val_in_db = Value;
            List<int> numbers = new List<int>();
            foreach (string s in val_in_db.Split(";"))
            {
                numbers.Add(int.Parse(s.Trim()));
            }
            Random rand = new Random();
            return numbers[rand.Next(0, numbers.Count)];
        }
    }
}
