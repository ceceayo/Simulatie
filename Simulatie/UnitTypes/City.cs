using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie.UnitTypes
{
    public class City : IUnitType
    {
        public int TypeNum { get; } = 1;
        public int Id { get; set; }
        public Dictionary<int, string> arguments { get; set; } = new Dictionary<int, string>();

        public City(int Id, Dictionary<int, string> args)
        {
            this.Id = Id;
            this.arguments = args;
        }

        public UnitTickResponse? OnTick()
        {
            return new UnitTickResponse
            {
                newunit = new City(args: this.arguments, Id: this.Id),
                resources_used = 0
            };
        }
    }
}