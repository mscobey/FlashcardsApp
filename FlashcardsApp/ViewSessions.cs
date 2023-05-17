using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class ViewSessions
    {
        internal static void SessionsMenu()
        {
            List<StudySession> list = SQLDataAccess.LoadSessions();
            foreach(StudySession session in list)
            {
                List<Stack> sList = SQLDataAccess.LoadStack(session.StackID);
                foreach(Stack stack in sList)
                {
                    Console.WriteLine($"{session.Date} | Studied: {stack.Name} | Score: {session.Score}");
                }
            }
            Menu.StartMenu();
        }
    }
}
