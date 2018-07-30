using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    abstract class Armor
    {
        public ArmorType Type { get; }

        public string Name { get; }

        public int ArmorPoints { get; set; }

        public Armor(string name, int armorPoints, ArmorType type)
        {
            Type = type;
            Name = name;
            ArmorPoints = armorPoints;
        }
    }

    public enum ArmorType
    {
        Light = 0,
        Middle = 1,
        Heavy = 2
    }
}
