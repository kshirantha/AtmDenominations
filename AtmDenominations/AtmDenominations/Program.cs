using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Denomination denomination = new Denomination();
            String denominations = denomination.getDenominations(230);

            Console.WriteLine(denominations);
        }
    }
}
