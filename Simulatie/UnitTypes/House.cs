﻿using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class House : IUnitType
    {
        public int TypeNum { get; } = 2;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType? Owner { get; set; }

        public House(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type House with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new UnitTickResponse
            {
                NewUnit = new House(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = 0
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? lamps_to_make = sp.FindInstance(db, 2*10000+1, 1, sim, "Total lamps in house") as SimpleNumber;
            SimpleNumber? refrigerators_to_make = sp.FindInstance(db, 2*10000+2, 1, sim, "Total refrigerators in house") as SimpleNumber;
            SimpleNumber? freezers_to_make = sp.FindInstance(db, 2*10000+3, 1, sim, "Total freezers in house") as SimpleNumber;
            SimpleNumber? phonechargers_to_make = sp.FindInstance(db, 2 * 10000 + 4, 1, sim, "Total phonechargers in house") as SimpleNumber;
            SimpleNumber? microwaves_to_make = sp.FindInstance(db, 2 * 10000+5, 1, sim, "Total microwaves in house") as SimpleNumber;
            SimpleNumber? personalcomputers_to_make = sp.FindInstance(db, 2 * 10000 + 6, 1, sim, "Total personalcomputers in house") as SimpleNumber;
            SimpleNumber? airconditioningunits_to_make = sp.FindInstance(db, 2 * 10000 + 7, 1, sim, "Total airconditioningunits in house") as SimpleNumber;
            SimpleNumber? washingmachines_to_make = sp.FindInstance(db, 2 * 10000 + 8, 1, sim, "Total washingmachines in house") as SimpleNumber;
            SimpleNumber? dryers_to_make = sp.FindInstance(db, 2 * 10000 + 9, 1, sim, "Total dryers in house") as SimpleNumber;
            SimpleNumber? televisions_to_make = sp.FindInstance(db, 2 * 10000 + 10, 1, sim, "Total televisions in house") as SimpleNumber;
            if (lamps_to_make == null)
            {
                Log.Fatal("Total lamps to make per house was not found.");
                throw new InvalidOperationException("Total lamps to make per house was not found.");
            }
            List<IUnitType> child_creations = new List<IUnitType>();
            for (int i = 0; i < lamps_to_make.GetNumber(); i++)
            {
                Lamp lamp = new Lamp(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(lamp);

            }
            for (int i = 0; i < refrigerators_to_make.GetNumber(); i++)
            {
                Refrigerator refrigerator = new Refrigerator(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(refrigerator);

            }
            for (int i = 0; i < freezers_to_make.GetNumber(); i++)
            {
                Freezer freezer = new Freezer(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(freezer);

            }
            for (int i = 0; i < phonechargers_to_make.GetNumber(); i++)
            {
                PhoneCharger phonechargers = new PhoneCharger(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(phonechargers);

            }
            for (int i = 0; i < microwaves_to_make.GetNumber(); i++)
            {
                Microwave microwave = new Microwave(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(microwave);

            }
            for (int i = 0; i < personalcomputers_to_make.GetNumber(); i++)
            {
                PersonalComputer personalcomputer = new PersonalComputer(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(personalcomputer);

            }
            for (int i = 0; i < airconditioningunits_to_make.GetNumber(); i++)
            {
                Airconditioningunit airconditioningunit = new Airconditioningunit(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(airconditioningunit);

            }
            for (int i = 0; i < washingmachines_to_make.GetNumber(); i++)
            {
                Washingmachine washingmachine = new Washingmachine(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(washingmachine);

            }
            for (int i = 0; i < dryers_to_make.GetNumber(); i++)
            {
                Dryer dryer = new Dryer(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(dryer);

            }
            for (int i = 0; i < televisions_to_make.GetNumber(); i++)
            {
                Television television = new Television(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(television);

            }
            return child_creations;
        }
    }
}
