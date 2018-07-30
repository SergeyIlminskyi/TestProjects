using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    abstract class HeroFactory
    {
        public abstract int Health { get; }

        public HeroFactory() { }
    
        public abstract Armor CreateArmor();
        public abstract Weapon CreateWeapon();
    }
}