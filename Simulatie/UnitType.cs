using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie
{
    public interface IUnitType
    {
        int Id { get; set; }
    }

    public class UnitTypeProvider
    {
        public IUnitType GetUnit(SimulatedUnit unit, SimulationDatabaseContext db)
        {
            switch (unit.Type)
            {
                default:
                    throw new Exception("Unknown unit type");
            }
        }
    }
}
