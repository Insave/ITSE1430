using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Host
{
    class Program
    {
        static void Main( string[] args )
        {
            
        }

        static void PlayingWithPrimitives()
        {
            //Primitive
            decimal unitPrice = 10.5M;

            //Real declaration
            System.Decimal unitPrice2 = 10.5M;

            //Current time
            DateTime now = DateTime.Now;

            System.Collections.ArrayList items;
            
        }

        static void PlayingWithVariables()
        {
            int hours = 0;
            double rate = 10.25;

            int hours2 = hours;
            string firstName, lastName;

            //string @class;

            firstName = "Bob";
            lastName = "Miller";

            firstName = lastName = "Sue";

            //Math ops
            int x = 0, y = 10;
            int add = x + y;
            int subtract = x - y;
            int multiply = x * y;
            int divide = x / y;
            int modulos = x % y;

            x += 10;
            double ceiling = Math.Ceiling(rate);
            double floor = ceiling;
        }
    }
}
