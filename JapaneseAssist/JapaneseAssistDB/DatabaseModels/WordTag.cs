using System;
using System.Collections.Generic;
using System.Text;

namespace JapaneseAssistDB.DatabaseModels
{
    class WordTag
    {
        public int ID { get; private set; }
        public int WordID { get; private set; }
        public string Tag { get; private set; }
    }
}
