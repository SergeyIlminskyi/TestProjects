using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class HeroArmor : Armor
    {
        public HeroArmor(string name, int armorPoints, ArmorType type) : base(name, armorPoints, type) { }
    }
}
