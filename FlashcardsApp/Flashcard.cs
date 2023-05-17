using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    internal class Flashcard
    {
        public int FlashcardID { get; set; }

        public int StackID { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }
    }
}
