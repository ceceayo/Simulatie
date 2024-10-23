using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie.StatTypes
{
    public class SimpleNumber : IStatType
    {
        public int TypeNum { get; } = 1;
        public int Id { get; }
        public string Value { get; }
        public SimpleNumber(int id, SimulationDatabaseContext db)
        {
            Id = id;
            var me_in_db = db.Statistics.Find(Id);
            Value = me_in_db.Value;
            
        }
        public void AskForValueInput(SimulationDatabaseContext db)
        {
            // empty (for now >w<)
        } 
    }
}
