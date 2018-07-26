using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Deposit : Product
    {
        public Deposit()
        {
            Console.WriteLine("Депозит создан");
        }
    }
}
