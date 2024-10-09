using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie.UnitTypes
{
    public class House : IUnitType
    {
        public int TypeNum { get; } = 2;
        public int Id { get; set; }
        public Dictionary<int, string> arguments { get; set; } = new Dictionary<int, string>();

        public House(int id, Dictionary<int, string> args)
        {
            this.Id = id;
        }

        public UnitTickResponse? OnTick()
        {
            return new UnitTickResponse
            {
                newunit = new House(args: this.arguments, Id: this.Id),
                resources_used = 4
            };
        }
    }
}
