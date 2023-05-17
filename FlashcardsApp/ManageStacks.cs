using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class ManageStacks
    {

        internal static void StacksMenu()
        {
            Console.WriteLine("********************");
            Console.WriteLine("Enter a number:");
            Console.WriteLine("(1) Create New Flashcard Stack");
            Console.WriteLine("(2) View Current Stacks");
            Console.WriteLine("(3) Update a Stack");
            Console.WriteLine("(4) Delete a Stack");
            Console.WriteLine("(5) Return to Main Menu");
            Console.WriteLine("(0) Press at any input to exit application");
            Console.WriteLine("********************");
            string input = Console.ReadLine();
            int choice;

            if (!Validation.ValidateInput(input))
                StacksMenu();
            choice = int.Parse(input);
            switch (choice)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    NewStack();
                    break;
                case 2:
                    ViewStacks();
                    break;
                case 3:
                    UpdateStack();
                    break;
                case 4:
                    DeleteStack();
                    break;
                case 5:
                    Menu.StartMenu();
                    break;
                default:
                    Console.WriteLine("Not a valid number! Try again."); StacksMenu();
                    break;
            }
        }
        internal static void NewStack()
        {
            Stack stack = new Stack();
            Console.WriteLine("");
            Console.WriteLine("Enter a subject name for your new stack:");
            string input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                NewStack();
            stack.Name = input;
            SQLDataAccess.AddStack(stack);
            Console.WriteLine("Stack added! Do you want to add flashcards now (1), return to stacks menu (2), or return to main menu (3)?: ");
            input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                StacksMenu();
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    ManageCards.AddCard();
                    break;
                case 2:
                    StacksMenu();
                    break;
                case 3:
                    Menu.StartMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input! Returning to stacks menu!");
                    StacksMenu();
                    break;
            }
            
        }
        internal static void ViewStacks()
        {
            List<Stack> list = SQLDataAccess.LoadStacks();

            //TableView tv = new TableView();
            //tv.DisplayTable(list);
            Console.WriteLine("Current Stacks:");
            foreach(Stack stk in list)
            {
                Console.WriteLine($"{stk.StackID}:{stk.Name}");
            }
            Console.WriteLine("Enter a Stack ID to view the flashcards in it:");
            string input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                ViewStacks();
            int choice = int.Parse(input);
            Stack stack = list.Find(x => x.StackID == choice);

            if (!Validation.ValidateInput(stack))
                ViewStacks();

            List<Flashcard> cardList = SQLDataAccess.LoadCards(stack);
            Console.WriteLine("");
            Console.WriteLine("Front | Back");
            foreach (Flashcard card in cardList)
            {
                Console.WriteLine($"{card.Front} | {card.Back}");
            }
            Console.WriteLine("Returning to Stacks menu...");

       
            StacksMenu();
        }
        internal static void UpdateStack()
        {
            List<Stack> list = SQLDataAccess.LoadStacks();

            foreach (Stack stk in list)
            {
                Console.WriteLine($"{stk.StackID}:{stk.Name}");
            }
            Console.WriteLine("Enter the ID for the Stack you want to update:");
            string input = Console.ReadLine();
            int choiceId = int.Parse(input);
            if (!Validation.ValidateInput(input))
                UpdateStack();
            Stack stack = list.Find(x => x.StackID == choiceId);
            if (!Validation.ValidateInput(stack))
                UpdateStack();
            Console.WriteLine("(1) Edit name of the stack");
            Console.WriteLine("(2) Edit flashcards in the stack");
            Console.WriteLine("");
            input = Console.ReadLine();
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter new stack name:");
                    input = Console.ReadLine();
                    if (!Validation.ValidateInput(input))
                        UpdateStack();
                    stack.Name = input;
                    SQLDataAccess.UpdateStack(stack);
                    break;
                case 2:
                    ManageCards.UpdateCards(stack.StackID);
                    break;
                default:
                    Console.WriteLine("Invalid input! Try again!");UpdateStack();
                    break;

            }
            Console.WriteLine("Stack has been updated! Returning to stacks menu...");
            StacksMenu();
        }
        internal static void DeleteStack()
        {
            List<Stack> list = SQLDataAccess.LoadStacks();

            foreach(Stack stk in list)
            {
                Console.WriteLine($"{stk.StackID}:{stk.Name}");
            }
            Console.WriteLine("");
            Console.WriteLine("Enter the ID for the Stack you want to delete:");
            string input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                DeleteStack();
            int choice = int.Parse(input);
            Stack stack = list.Find(x => x.StackID == choice);

            if (!Validation.ValidateInput(stack))
                DeleteStack();
            Console.WriteLine("This will delete ALL flashcards and study sessions associated with this stack.");
            Console.WriteLine("Are you sure you want to delete this stack? Yes/No:");
            string answer = Console.ReadLine().ToLower();
            switch (answer)
            {
                case "no":
                    StacksMenu();
                    break;
                case "yes":
                    SQLDataAccess.DeleteStack(stack);
                    break;
                default:
                    Console.WriteLine("Invalid entry! Try again!");
                    DeleteStack();
                    break;
            }

            Console.WriteLine($"The stack has been deleted from the database. Returning to stack menu...");
            Console.WriteLine("");
            StacksMenu();
        }
    }
}
