using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class Study
    {
        internal static void StudyMenu()
        {
            List<Stack> stacks = SQLDataAccess.LoadStacks();
            foreach (Stack stack in stacks)
            {
                Console.WriteLine($"({stack.StackID}){stack.Name}");
            }
            Console.WriteLine("Enter the ID for the subject you want to study:");
            string input = Console.ReadLine();
            int choiceId;

            if (!Validation.ValidateInput(input))
                StudyMenu();
            choiceId = int.Parse(input);

            StudyCards(choiceId);

        }
        internal static void StudyCards(int stackID)
        {
            List<Flashcard> list = SQLDataAccess.LoadCards(stackID);
            int score = 0;
            string input;
            foreach (Flashcard card in list)
            {
                Console.WriteLine($"{card.Front}");
                Console.WriteLine("Answer:");
                input = Console.ReadLine();

                if (input.ToLower() == card.Back.ToLower())
                {
                    score++;
                    Console.WriteLine("Correct!");
                    
                }
                else
                    Console.WriteLine($"Incorrect, the correct answer is: {card.Back}");
                Console.WriteLine("-----------------------------");
            }
            Console.WriteLine($"Stack completed! Your total score was: {score}");
            StudySession session = new StudySession();
            session.StackID = stackID;
            session.Score = score;
            session.Date = DateTime.Now;
            SQLDataAccess.AddStudySession(session);
            Console.WriteLine("(1) Try again");
            Console.WriteLine("(2) Study a different subject");
            Console.WriteLine("(3) Return to Main Menu");
            Console.WriteLine("(0) Exit application");
            input = Console.ReadLine();
            switch(input)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    StudyCards(stackID);
                    break;
                case "2":
                    StudyMenu();
                    break;
                case "3":
                    Menu.StartMenu();
                    break;
            }
        }
    }
}
