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

        public SQLiteAsyncConnection connectionAsync;
        public SQLiteConnection connectionSync;

        public Database()
        {
            // create DB path
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            Path = System.IO.Path.Combine(docsFolder, "db_sqlnet.db");

            try
            {
                connectionAsync = new SQLiteAsyncConnection(Path);
                connectionSync = new SQLiteConnection(Path);
            }
            catch (SQLiteException ex)
            {
                
            }
        }

        public static bool TableExists<T>(SQLiteConnection connection)
        {
            const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
            var cmd = connection.CreateCommand(cmdText, typeof(T).Name);
            return cmd.ExecuteScalar<string>() != null;
        }

        public void DBInit()
        {
            if (!TableExists<Word>(connectionSync))
            {
                connectionAsync.CreateTableAsync<Word>();
                var wordList = new List<Word>
                {
                    new Word { SourceWord = "dog", TranslateWord = "собака" },
                    new Word { SourceWord = "cat", TranslateWord = "кошка" },
                    new Word { SourceWord = "table", TranslateWord = "стол" }
                };

                insertUpdateAllWords(wordList);
            }
        }

        public string insertUpdateWord(Word data)
        {
            try
            {
                var db = new SQLiteConnection(Path);
                if (db.Insert(data) != 0)
                    db.Update(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public string insertUpdateAllWords(IEnumerable<Word> data)
        {
            try
            {
                var db = new SQLiteConnection(Path);
                if (db.InsertAll(data) != 0)
                    db.UpdateAll(data);
                return "List of data inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public Task<List<Word>> getAllWords()
        {

            try
            {
                var db = new SQLiteAsyncConnection(Path);
                // this counts all records in the database, it can be slow depending on the size of the database
                var result = db.Table<Word>().ToListAsync();

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
