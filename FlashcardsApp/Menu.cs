using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class Menu
    {

        internal static void StartMenu()
        {
            Console.WriteLine("********************");
            Console.WriteLine("Enter a number:");
            Console.WriteLine("(1) Study");
            Console.WriteLine("(2) Manage Flashcards");
            Console.WriteLine("(3) Manage Stacks");
            Console.WriteLine("(4) View Past Study Sessions");
            Console.WriteLine("(0) Press at any input to exit application");
            Console.WriteLine("********************");

            string input = Console.ReadLine();
            int choice;

            if (!Validation.ValidateInput(input))
                StartMenu();
            choice = int.Parse(input);
            switch(choice)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Study.StudyMenu();
                    break;
                case 2:
                    ManageCards.CardsMenu();
                    break;
                case 3:
                    ManageStacks.StacksMenu();
                    break;
                case 4:
                    ViewSessions.SessionsMenu();
                    break;
                default:
                    Console.WriteLine("Not a valid number! Try again.");StartMenu();
                    break;

            }

        }
    }
}
