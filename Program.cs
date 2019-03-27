using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab04
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree("rezervasyon.txt");
            Menu menu = new Menu(tree);
            Console.ReadKey();
        }
    }
}