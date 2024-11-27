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
    public class SimpleNumber : IStatType
    {
        public int TypeNum { get; } = 1;
        public int Id { get; }
        public string Value { get; set; }
        public int Role { get; }
        public SimpleNumber(int id, SimulationDatabaseContext db)
        {
            Id = id;
            var me_in_db = db.Statistics.Find(Id);
            Value = me_in_db.Value;
            Role = me_in_db.Role;

        }
        public void AskForValueInput(SimulationDatabaseContext db, bool guiMode, string Message)
        {
            Log.Information("Please enter a number for the value of this statistic. Type = {type}, Role = {role}", TypeNum, Role);
            int value;
            while (true)
            {
                string input;
                if (guiMode)
                {
                    input = Interaction.InputBox(Message, "Enter a number", "0");
                }
                else
                {
                    input = Console.ReadLine()!;
                }
                if (int.TryParse(input, out value))
                {
                    break;
                }
                else
                {
                    Log.Error("Please enter a valid number. Can NOT parse '{input}' as int. Try again!", input);
                }
            }

            var me_in_db = db.Statistics.Find(Id);
            me_in_db!.Value = value.ToString();
            Value = value.ToString();
            db.SaveChanges();

        }
        public int GetNumber()
        {
            return int.Parse(Value);
        }
    }
}
