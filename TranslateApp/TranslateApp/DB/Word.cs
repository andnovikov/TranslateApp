using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TranslateApp.DB
{
    public class Word
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string SourceWord { get; set; }

        public string TranslateWord { get; set; }

        public override string ToString()
        {
            return string.Format("[Word: ID={0}, SourceWord={1}, TranslateWord={2}]", ID, SourceWord, TranslateWord);
        }
    }
}