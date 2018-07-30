using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class BowmanFactory : HeroFactory
    { 
        public override int Health { get { return 500; } }

        public override Armor CreateArmor()
        {
            return HeroArmorFactory.CreateLightArmor();
        }
        public override Weapon CreateWeapon()
        {
            return BowFactory.CreateHuntingBow();
        }
    }

}
