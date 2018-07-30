using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    static class SwordsFactory
    {
        private const WeaponType coldWeapon = WeaponType.Cold;

        static public Sword CreateExcalibur()
        {
            return new Sword("Excalibur", 150, coldWeapon);
        }

        static public Sword CreateHarpy()
        {
            return new Sword("Harpy", 100, coldWeapon);
        }

        static public Sword CreateDistroyer()
        {
            return new Sword("Distroyer", 120, coldWeapon);
        }

    }
}
