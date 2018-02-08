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
                switch (Char.ToUpper(choice))
                {
                    case 'L':
                    ListProducts();
                    break;
                    case 'A':
                    AddProduct();
                    break;
                    case 'R':
                    RemoveProduct();
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
            _length = ReadDecimal("Enter optional length ", 0);

            //Get description
            _description = ReadString("Enter optional description: ", false);

            //Get owned
            _owned = GetOwned();
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
                Console.Write(message + ">= {0}: ", minValue);

                string value = Console.ReadLine();

                if (String.IsNullOrEmpty(value))
                    return 0;
                
                if (Decimal.TryParse(value, out decimal result))
                {
                    //If not required or not empty
                    if (result >= minValue)
                        return result;
                }

                Console.WriteLine("Value must be >= {0}", minValue);
            } while (true);
        }

        private static bool GetOwned()
        {
            do
            {
                Console.Write("Is the movie owned (Y/N): ");

                string value = Console.ReadLine().ToUpper();
                
                if (value == "Y")
                    return true;
                if (value == "N")
                    return false;

                Console.WriteLine("Value is required");
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

                string input = Console.ReadLine().Trim();

                if (String.Compare(input, "L", true) == 0)
                    return input[0];
                else if (String.Compare(input, "A", true) == 0)
                    return input[0];
                else if (String.Compare(input, "Q", true) == 0)
                    return input[0];
                else if (String.Compare(input, "R", true) == 0)
                    return input[0];

                Console.WriteLine("Please choose a valid option");
            } while (true);
        }

        static void ListProducts()
        {
            //Are there any products? Eventually will be checking the length of the map or set used
            if (!String.IsNullOrEmpty(_name))
            {
                //Display a product
                //TODO organise list product 
                string msg = $"Title: {_name} {Environment.NewLine}" +
                    $"Length: {_length}";
                Console.WriteLine(msg);

                if (!String.IsNullOrEmpty(_description))
                    Console.WriteLine(_description);

                Console.WriteLine($"Owned: {_owned}");
            } else
                Console.WriteLine("No products");
        }

        static void RemoveProduct()
        {
            do
            {
                Console.Write("Enter the name of the movie to remove or blank to cancel: ");

                string value = Console.ReadLine().Trim();

                if (value.Equals(_name)) //TODO remove from map/set
                {
                    _name = "";
                    _length = 0;
                    _description = "";
                    _owned = false;
                    ListProducts();
                    return;
                }
                if (String.IsNullOrEmpty(_name))
                    return; 

                Console.WriteLine("Movie not found");
            } while (true);
        }

        //Data for a product
        //For multiple products, might use a map/set due to name being a unique id for movies
        static string _name;
        static decimal _length;
        static string _description;
        static bool _owned;
    }
}
