using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie
{
    public interface IUnitType
    {
        public int TypeNum { get; }
        public int Id { get; }
        public Dictionary<int, string> arguments { get; }
    }
}
