using System;
using System.Collections.Generic;
using System.Text;

namespace JapaneseAssistDB.DatabaseModels
{
    class WordWriting
    {
        public int ID { get; private set; }
        public int WordID { get; private set; }
        public string Writing { get; private set; }
        public string Kana { get; private set; }
    }
}
