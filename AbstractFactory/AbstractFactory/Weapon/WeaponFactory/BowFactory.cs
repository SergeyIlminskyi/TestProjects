using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    static class BowFactory
    {
        static public Bow CreateHuntingBow()
        {
            return new Bow("Hunting bow", 200, WeaponType.Throwing);
        }
    }
}
