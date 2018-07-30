using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class SwordsmanFactory : HeroFactory
    {
        public override int Health { get { return 800; } }

        public override Armor CreateArmor()
        {
            return HeroArmorFactory.CreateMiddleArmor();
        }
        public override Weapon CreateWeapon()
        {
            return SwordsFactory.CreateHarpy();
        }
    }
}
