using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Simulatie.UnitTypes;

namespace Simulatie
{
    public class UnitProvider
    {
        public Dictionary<int, Type> Types = new Dictionary<int, Type>
        {
            { 1, typeof(City) },
            { 2, typeof(House) }
        };

        public UnitProvider()
        {
            Log.Debug("Created a unitprovider");
        }

        public Type? GetType(int type)
        {
            if (Types.ContainsKey(type))
            {
                return Types[type];
            }
            return null;
        }

        public IUnitType? GetInstance(int id, SimulationDatabaseContext db)
        {
            var o = db.SimulatedUnits.Find(id);
            if (o == null) { return null; }
            if (Types.ContainsKey(o.Type))
            {
                var t = Types[o.Type];
                var i = Activator.CreateInstance(t, [id, new Dictionary<int, string>()]) as IUnitType;
                return i;
            }
            return null;
        }
    }
}
