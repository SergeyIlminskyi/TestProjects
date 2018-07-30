using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var bowman = new Hero(new BowmanFactory(), "Sergey", Race.Human);


            var sworsman = new Hero(new SwordsmanFactory(), "Anton", Race.Human);

            bowman.Hit(sworsman);


            Console.ReadKey();
        }
    }
}
