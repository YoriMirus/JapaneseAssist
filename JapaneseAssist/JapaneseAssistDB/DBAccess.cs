using System;
using Dapper;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

using JapaneseAssistDB.DatabaseModels;
using System.Data;

namespace JapaneseAssistDB
{
    public static class DBAccess
    {
        private static readonly SqliteConnectionStringBuilder connectionString = new SqliteConnectionStringBuilder()
        {
            DataSource = "Database.db",
            Mode = SqliteOpenMode.ReadWrite
        };

        public static async Task<List<char>> GetIgnoredKanjiAsync()
        {
            List<IgnoredKanji> ignoredKanjiEntries;
            List<char> ignoredKanji = new List<char>();

            //Get all the IgnoredKanji from the database
            using (IDbConnection connection = new SqliteConnection(connectionString.ToString()))
            {
                ignoredKanjiEntries = (await connection.QueryAsync<IgnoredKanji>("SELECT * FROM IgnoredKanjis")).ToList();
            }

            //ID is only used for storing the kanji inside the database, so they are removed when returning the results
            foreach(IgnoredKanji k in ignoredKanjiEntries)
            {
                ignoredKanji.Add(k.Kanji[0]);
            }
            return ignoredKanji;
        }
        public static List<char> GetIgnoredKanji()
        {
            List<IgnoredKanji> ignoredKanjiEntries;
            List<char> ignoredKanji = new List<char>();

            //Get all the IgnoredKanji from the database
            using (IDbConnection connection = new SqliteConnection(connectionString.ToString()))
            {
                ignoredKanjiEntries = connection.Query<IgnoredKanji>("SELECT * FROM IgnoredKanjis").ToList();
            }

            //ID is only used for storing the kanji inside the database, so they are removed when returning the results
            foreach (IgnoredKanji k in ignoredKanjiEntries)
            {
                ignoredKanji.Add(k.Kanji[0]);
            }
            return ignoredKanji;
        }

        public static async Task AddIgnoredKanjiAsync(char kanji)
        {
            using IDbConnection connection = new SqliteConnection(connectionString.ToString());
            await connection.ExecuteAsync($"INSERT INTO IgnoredKanjis(Kanji) VALUES('{kanji}')");
        }
        public static void AddIgnoredKanji(char kanji)
        {
            using IDbConnection connection = new SqliteConnection(connectionString.ToString());
            connection.Execute($"INSERT INTO IgnoredKanjis(Kanji) VALUES('{kanji}')");
        }

        public static async Task RemoveIgnoredKanjiAsync(char kanji)
        {
            using IDbConnection connection = new SqliteConnection(connectionString.ToString());
            await connection.ExecuteAsync($"DELETE FROM IgnoredKanjis WHERE Kanji='{kanji}'");
        }
        public static void RemoveIgnoredKanji(char kanji)
        {
            using IDbConnection connection = new SqliteConnection(connectionString.ToString());
            connection.Execute($"DELETE FROM IgnoredKanjis WHERE Kanji='{kanji}'");
        }
    }
}
