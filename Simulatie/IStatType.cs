using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie
{
    public interface IStatType
    {
        public int TypeNum { get; }
        public int Id { get; }
        public int Role { get; }
        public string Value { get; }
        public void AskForValueInput(SimulationDatabaseContext db);
    }
}
