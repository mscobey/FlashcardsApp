using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class ManageCards
    {
       internal static void CardsMenu()
        {
            Console.WriteLine("********************");
            Console.WriteLine("Enter a number:");
            Console.WriteLine("(1) Create New Flashcards");
            Console.WriteLine("(2) View Flashcards");
            Console.WriteLine("(3) Update a Flashcard");
            Console.WriteLine("(4) Delete a Flashcard");
            Console.WriteLine("(5) Return to Main Menu");
            Console.WriteLine("(0) Press at any input to exit application");
            Console.WriteLine("********************");
            string input = Console.ReadLine();
            int choice;

            if (!Validation.ValidateInput(input))
                CardsMenu();
            choice = int.Parse(input);
            switch (choice)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    AddCard();
                    break;
                case 2:
                    ViewCards();
                    break;
                case 3:
                    UpdateCard();
                    break;
                case 4:
                    DeleteCard();
                    break;
                case 5:
                    Menu.StartMenu();
                    break;
                default:
                    Console.WriteLine("Not a valid number! Try again."); CardsMenu();
                    break;
            }
        }
       internal static void AddCard()
        {
            Flashcard card = new Flashcard();
            Console.WriteLine("");
            List<Stack> list = SQLDataAccess.LoadStacks();

            //TableView tv = new TableView();
            //tv.DisplayTable(list);
            Console.WriteLine("Current Stacks:");
            foreach (Stack stk in list)
            {
                Console.WriteLine($"{stk.StackID}:{stk.Name}");
            }
            Console.WriteLine("Enter the ID for the Stack you want the new flashcard in:");
            string input = Console.ReadLine();
            int choiceId = int.Parse(input);
            if (!Validation.ValidateInput(input))
                AddCard();
            Stack stack = list.Find(x => x.StackID == choiceId);
            if (!Validation.ValidateInput(stack))
                AddCard();
            card.StackID = choiceId;

            Console.WriteLine("Enter the information for the front of the flashcard:");
            input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                AddCard();
            card.Front = input;
            Console.WriteLine("Enter the information for the back of the flashcard:");
            input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                AddCard();
            card.Back = input;

            SQLDataAccess.AddFlashcard(card);
            Console.WriteLine("Flashcard added! Returning to Flashcard menu...");

            CardsMenu();
        }
       
       internal static void ViewCards() { 
            List<Flashcard> list = SQLDataAccess.LoadCards();
            Console.WriteLine("Stack: Front | Back");
            foreach (Flashcard card in list)
            {
                List<Stack> sList = SQLDataAccess.LoadStack(card.StackID);
                foreach(Stack stk in sList)
                {
                    Console.WriteLine($"{stk.Name}: {card.Front} | {card.Back}");
                }
                
            }
            Console.WriteLine("");
            Console.WriteLine("Returning to Flashcard Menu");

            CardsMenu();
        }
       internal static void UpdateCard()
        {
            Console.WriteLine("");
            List<Flashcard> list = SQLDataAccess.LoadCards();

            //TableView tv = new TableView();
            //tv.DisplayTable(list);
            Console.WriteLine("Current Flashcards:");
            foreach (Flashcard crd in list)
            {
                Console.WriteLine($"{crd.FlashcardID}:{crd.Front}|{crd.Back}");
            }
            Console.WriteLine("Enter the ID for Flashcard you want to change:");
            string input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                UpdateCard();
            
            int choiceId = int.Parse(input);
            
            Flashcard card = list.Find(x => x.FlashcardID == choiceId);
            if (!Validation.ValidateInput(card))
                UpdateCard();
            Console.WriteLine("What do you want to change?");
            Console.WriteLine("(1) Front");
            Console.WriteLine("(2) Back");
            input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    Console.WriteLine("Enter new front for the flashcard: ");
                    input= Console.ReadLine();
                    if (!Validation.ValidateInput(input))
                        UpdateCard();
                    card.Front = input;
                    SQLDataAccess.UpdateCard(card);
                    break;
                case "2":
                    Console.WriteLine("Enter new back for the flashcard: ");
                    input = Console.ReadLine();
                    if (!Validation.ValidateInput(input))
                        UpdateCard();
                    card.Back = input;
                    SQLDataAccess.UpdateCard(card);
                    break;
                default:
                    Console.WriteLine("Invalid entry! Returning to flashcard menu..."); CardsMenu();
                    break;
            }
            Console.WriteLine("Card updated! Returning to flashcard menu...");
            CardsMenu();
        }
       internal static void UpdateCards(int stackID)
        {
            List<Flashcard> list = SQLDataAccess.LoadCards(stackID);
            Console.WriteLine("Current Flashcards:");
            foreach (Flashcard crd in list)
            {
                Console.WriteLine($"{crd.FlashcardID}:{crd.Front}|{crd.Back}");
            }
            Console.WriteLine("Enter the ID for Flashcard you want to change:");
            string input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                UpdateCards(stackID);
            int choiceId = int.Parse(input);
            
            Flashcard card = list.Find(x => x.FlashcardID == choiceId);
            if (!Validation.ValidateInput(card))
                ManageStacks.UpdateStack();
            Console.WriteLine("What do you want to change?");
            Console.WriteLine("(1) Front");
            Console.WriteLine("(2) Back");
            input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    Console.WriteLine("Enter new front for the flashcard: ");
                    input = Console.ReadLine();
                    if (!Validation.ValidateInput(input))
                        ManageStacks.UpdateStack();
                    card.Front = input;
                    SQLDataAccess.UpdateCard(card);
                    break;
                case "2":
                    Console.WriteLine("Enter new back for the flashcard: ");
                    input = Console.ReadLine();
                    if (!Validation.ValidateInput(input))
                        ManageStacks.UpdateStack();
                    card.Back = input;
                    SQLDataAccess.UpdateCard(card);
                    break;
                default:
                    Console.WriteLine("Invalid entry! Returning to update stack menu..."); ManageStacks.UpdateStack();
                    break;
            }
            Console.WriteLine("Card updated! Returning to stack menu...");
            ManageStacks.StacksMenu();
        }
        internal static void DeleteCard()
        {
            List<Flashcard> list = SQLDataAccess.LoadCards();

            foreach (Flashcard crd in list)
            {
                Console.WriteLine($"{crd.FlashcardID}:{crd.Front} | {crd.Back}");
            }
            Console.WriteLine("");
            Console.WriteLine("Enter the ID for the Flashcard you want to delete:");
            string input = Console.ReadLine();
            if (!Validation.ValidateInput(input))
                DeleteCard();
            int choice = int.Parse(input);
            Flashcard card = list.Find(x => x.FlashcardID == choice);

            if (!Validation.ValidateInput(card))
                DeleteCard();

            Console.WriteLine("Are you sure you want to delete this flashcard? Yes/No:");
            string answer = Console.ReadLine().ToLower();
            switch (answer)
            {
                case "no":
                    CardsMenu();
                    break;
                case "yes":
                    SQLDataAccess.DeleteCard(card);
                    break;
                default:
                    Console.WriteLine("Invalid entry! Try again!");
                    DeleteCard();
                    break;
            }

            Console.WriteLine($"The flashcard has been deleted from the database. Returning to flashcard menu...");
            Console.WriteLine("");
            CardsMenu();
        }
    }
}
