using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    static class HeroArmorFactory
    {
        public static Armor CreateLightArmor()
        {
            return new HeroArmor("Light Armor", 30, ArmorType.Light);
        }

        public static Armor CreateMiddleArmor()
        {
            return new HeroArmor("Middle Armor", 50, ArmorType.Middle);
        }

        public static Armor CreateMHeavyArmor()
        {
            return new HeroArmor("Heavy Armor", 80, ArmorType.Heavy);
        }
    }
}
