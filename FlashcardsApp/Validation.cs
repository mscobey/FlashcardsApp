using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class Validation
    {
        internal static bool ValidateInput(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("You must enter something! Try again!");
                Console.WriteLine("");
                return false;
            }
            else if (input == "0")
            {
                Environment.Exit(0);
                return false; 
            }
            else
                return true;
        }
        internal static bool ValidateInput(Flashcard card)
        {
            if (card == null)
            {
                Console.WriteLine("Invalid ID entered! Try again!");
                Console.WriteLine("");
                return false;
            }
            else
                return true;
        }
        internal static bool ValidateInput(Stack stack)
        {
            if (stack == null)
            {
                Console.WriteLine("Invalid ID entered! Try again!");
                Console.WriteLine("");
                return false;
            }
            else
                return true;
        }
    }
}
