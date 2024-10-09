using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie
{
    public class UnitTickResponse
    {
        public required IUnitType newunit;
        public required int resources_used;

        public UnitTickResponse()
        {
            //this.newunit = newunit;
            //this.resources_used = resources_used;
        }
    }
}
