using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    abstract class Weapon
    {
        public WeaponType Type { get; }

        public string Name { get; }

        public int Damage { get; }


        public Weapon(string name, int damage, WeaponType type)
        {
            Type = type;
            Name = name;
            Damage = damage;
        }

        public abstract void Hit( Hero hero);
    }

    public enum WeaponType
    {
        Cold = 0,
        Throwing = 1,
        Magic = 2
    }
}
