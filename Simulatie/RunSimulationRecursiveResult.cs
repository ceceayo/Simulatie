using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie
{
    internal class RunSimulationRecursiveResult
    {
        public required int ResourcesUsed = 0;
        public required List<IUnitType> NewUnits = new List<IUnitType>();
    }
}
