using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Sword : Weapon
    {
        public Sword(string name, int damage, WeaponType type) : base(name, damage, type) { }

        public override void Hit(Hero hero)
        {
            hero.HandleDamage(this.Damage);
        }
    }
}
