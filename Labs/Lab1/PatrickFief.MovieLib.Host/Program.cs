using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickFief.MovieLib.Host
{
    class Program
    {
        static void Main( string[] args )
        {
            bool quit = false;
            while (!quit)
            {
                //Display the menu
                char choice = DisplayMenu();

                //Process menu selection
                switch (choice)
                {
                    case 'L':
                    ListProducts();
                    break;
                    case 'A':
                    AddProduct();
                    break;
                    case 'Q':
                    quit = true;
                    break;
                }
            }
        }

        static void AddProduct()
        {
            //Get name
            _name = ReadString("Enter name: ", true);

            //Get price
            _length = ReadDecimal("Enter optional length: ", 0);

            //Get description
            _description = ReadString("Enter optional description: ", false);

            //Get owned

        }

        private static string ReadString( string message, bool isRequired )
        {
            do
            {
                Console.Write(message);

                string value = Console.ReadLine();

                //If not required or not empty
                if (!isRequired || value != "")
                    return value;

                Console.WriteLine("Value is required");
            } while (true);
        }

        private static decimal ReadDecimal( string message, decimal minValue )
        {
            do
            {
                Console.Write(message);

                string value = Console.ReadLine();

                if (Decimal.TryParse(value, out decimal result))
                {
                    //If not required or not empty
                    if (result >= minValue)
                        return result;
                }

                Console.WriteLine("Value must be >= {0}", minValue);
            } while (true);
        }

        private static char DisplayMenu()
        {
            do
            {
                Console.WriteLine("L)ist Movies");
                Console.WriteLine("A)dd Movie");
                Console.WriteLine("R)emove Movie");
                Console.WriteLine("Q)uit");

                string input = Console.ReadLine().ToUpper();

                if (input == "L")
                    return input[0];
                else if (input == "A")
                    return input[0];
                else if (input == "Q")
                    return input[0];
                else if (input == "R")
                    return input[0];

                Console.WriteLine("Please choose a valid option");
            } while (true);
        }

        static void ListProducts()
        {
            //Are there any products?
            if (_name != null && _name != "")
            {
                //Display a product
                Console.WriteLine(_name);
                Console.WriteLine(_length);
                Console.WriteLine(_description);
            } else
                Console.WriteLine("No products");
        }

        //Data for a product
        static string _name;
        static decimal _length;
        static string _description;
        static bool _owned;
    }
}
