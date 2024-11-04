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
        public IStatType FindInstance(SimulationDatabaseContext db, int role, int type, Simulation sim)
        {
            var o = db.Statistics.Where(x => x.Role == role && x.Type == type && x.Owner == sim).ToList();
            if (o.Count == 0)
            {
                var i = this.GetType(type);
                if (i != null)
                {
                    var x = Activator.CreateInstance(i, new object[] { -1, db }) as IStatType;
                    Log.Debug("Created DB item for stat with {role} of {type}: {x}", role, type, x);
                    Log.Information("Please, enter a value for stat with role {role} of type {type}.", role, type);
                    x.AskForValueInput(db);
                    db.Statistics.Add(new Stats { Role = role, Type = type, Value = x.Value, Owner = sim });
                    return x;
                }
            }
            else if (o.Count == 1)
            {
                return this.GetInstance(o[0].Id, db);
            }
            else
            {
                Log.Fatal("Found multiple stats with role {role} of type {type}.", role, type);
                throw new Exception("Found multiple stats with role and type");
            }
            Log.Fatal("Could not find or create stat")
            throw new Exception("Could not find or create stat");
        }
    }
}
