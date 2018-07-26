using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class CurrentAccount : Product
    {
        public CurrentAccount()
        {
            Console.WriteLine("Текущий счет создан");
        }
    }
}
