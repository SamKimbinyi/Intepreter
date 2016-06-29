using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intepreter
{
    class Program
    {
       public static  Cradle cradle = new Cradle();
        static void Main(string[] args)
        {
           
                cradle.initialize();
                cradle.Expression();
            Console.ReadLine();
        }
    }
}
