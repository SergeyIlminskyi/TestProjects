using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Hero
    {
        private Armor armor;

        private Weapon weapon;

        public string Name { get; }

        public int Health { get; private set; }

        public Race Race { get; }

        public HeroStatus Status
        {
            get { return Health > 0 ? HeroStatus.Alive : HeroStatus.Dead; }
        }

        public Hero(HeroFactory factory, string name, Race race)
        {
            Name = name;
            Race = race;
            Health = factory.Health;
            armor = factory.CreateArmor();
            weapon = factory.CreateWeapon();
        }

        public void Hit(Hero hero)
        {
            weapon.Hit(hero);
        }

        public void HandleDamage(int damage)
        {
            if(armor != null)
            {
                damage -= armor.ArmorPoints;
            }

            Health -= damage;
        }
    }

    public enum Race
    {
        Human = 0,
        Elf = 1,
    }

    public enum HeroStatus
    {
        Alive = 0,
        Dead = 1
    }
}
