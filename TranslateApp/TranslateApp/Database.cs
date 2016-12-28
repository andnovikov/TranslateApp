using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                insertUpdateAllWords(wordList, Path);
            }
            catch (SQLiteException ex)
            {
                
            }
        }

        private string insertUpdateWord(Word data, string path)
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

        private string insertUpdateAllWords(IEnumerable<Word> data, string path)
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

        public List<Word> getAllWords()
        {

            try
            {
                var db = new SQLiteConnection(Path);
                // this counts all records in the database, it can be slow depending on the size of the database
                var result = db.Table<Word>().ToList<Word>();

                // for a non-parameterless query
                // var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");

                return result;
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }
    }
}
