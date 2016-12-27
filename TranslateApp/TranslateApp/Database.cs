using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TranslateApp.DB
{
    public class Database
    {
        public string Path;

        public Database()
        {
            // create DB path
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            Path = System.IO.Path.Combine(docsFolder, "db_sqlnet.db");

            try
            {
                var connection = new SQLiteAsyncConnection(Path);
                connection.CreateTableAsync<Word>();

                var wordList = new List<Word>
                {
                    new Word { SourceWord = "dog", TranslateWord = "собака" },
                    new Word { SourceWord = "cat", TranslateWord = "кошка" },
                    new Word { SourceWord = "table", TranslateWord = "стол" }
                };

                insertUpdateAllData(wordList, Path);
            }
            catch (SQLiteException ex)
            {
                
            }
        }

        private string insertUpdateData(Word data, string path)
        {
            try
            {
                var db = new SQLiteConnection(path);
                if (db.Insert(data) != 0)
                    db.Update(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private string insertUpdateAllData(IEnumerable<Word> data, string path)
        {
            try
            {
                var db = new SQLiteConnection(path);
                if (db.InsertAll(data) != 0)
                    db.UpdateAll(data);
                return "List of data inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}
