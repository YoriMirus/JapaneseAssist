using System;
using System.Collections.Generic;
using System.Text;

namespace JapaneseAssistDB.DatabaseModels
{
    class WordMeaning
    {
        public int ID { get; private set; }
        public int WordID { get; private set; }
        public string Meaning { get; private set; }
        public string PartsOfSpeech { get; private set; }
    }
}
