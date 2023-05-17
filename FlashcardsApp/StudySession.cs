using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class StudySession
    {
        public int StudySessionID { get; set; }

        public int StackID { get; set; }

        public DateTime Date { get; set; }

        public int Score { get; set; }
    }
}
