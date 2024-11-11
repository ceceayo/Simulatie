using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie
{
    public record CreateSimulationItem(
        IUnitType Unit,
        int OwnerId
    );
}
