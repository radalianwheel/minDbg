using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Method_1();
            Method_2();
            Method_3();
        }

        static void Method_1()
        {
            Method_2();
        }

        private static void Method_2()
        {
            ThrowFirstChanceException();
            while (true)
            {
                Method_3();
            }
        }

        private static void Method_3()
        {
            Console.WriteLine("Method_3");
            ThrowSecondChanceException();
            Console.ReadKey();
        }

        private static void ThrowFirstChanceException()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
            }
            
        }

        private static void ThrowSecondChanceException()
        {
            throw new NotImplementedException();
        }
    }
}
