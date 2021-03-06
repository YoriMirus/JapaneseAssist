using System;
using System.Collections.Generic;
using System.Text;

namespace JapaneseAssistDB.Database_Models
{
    class Word
    {
        public int ID { get; private set; }
        public string MainWriting { get; private set; }
        public string MainReading { get; private set; }
    }
}
