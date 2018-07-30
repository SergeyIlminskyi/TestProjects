using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {

        static void Main(string[] args)
        {
            Creator creator;

            creator = new CurrentAccountCreator();
            var curr = creator.FactoryMethod();

            creator = new LoanCreator();
            var loan = creator.FactoryMethod();

            creator = new DepositCreator();
            var depo = creator.FactoryMethod();

            Console.ReadLine();
        }

    }
}
