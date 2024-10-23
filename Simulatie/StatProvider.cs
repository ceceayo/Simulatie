using Serilog;
using Simulatie.StatTypes;
using Simulatie.UnitTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulatie
{
    public class StatProvider
    {
        public Dictionary<int, Type> Types = new Dictionary<int, Type>
        {
            {1, typeof(SimpleNumber)}
        };
        public StatProvider()
        {
            Log.Debug("Created a statprovider");
        }
        public Type? GetType(int type)
        {
            if (Types.ContainsKey(type))
            {
                return Types[type];
            }
            return null;
        }
        public IStatType? GetInstance(int id, SimulationDatabaseContext db)
        {
            var o = db.Statistics.Find(id);
            if (o == null) { return null; }
            if (Types.ContainsKey(o.Type))
            {
                var t = Types[o.Type];
                var i = Activator.CreateInstance(t, [id, db]) as IStatType;
                return i;
            }
            return null;
        }
    }
}
