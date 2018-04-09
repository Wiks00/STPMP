using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Bitcoin.Curses.Models;
using Bitcoin.Curses.Services.Interfaces;
using SQLite;

namespace Bitcoin.Curses.Services
{
    public class DatabaseService : ICacheService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService() : this("SQLite.db3")
        {
        }

        public DatabaseService(string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                throw new Exception("database name can not be null or empty!");
            }
           
            try
            {
                _database = new SQLiteAsyncConnection(dbPath);

                _database.CreateTableAsync<BitcoinExchangeRate>().Wait();
            }
            catch (Exception ex) //when (ex.InnerException is SQLiteException inner && inner.Message.Contains("duplicate"))
            {
            }
        }
       
        public async Task<IDictionary<string, BitcoinExchangeRate>> LoadAsync()
        {
            var list = await _database.Table<BitcoinExchangeRate>().ToListAsync();

            return list.ToDictionary(arg => arg.Currency, arg => arg);
        }

        public async Task SaveAsync(IDictionary<string, BitcoinExchangeRate> bitcoinRateValues)
        {
            List<Task> tasks = new List<Task>(bitcoinRateValues.Count);

            foreach (var keyValue in bitcoinRateValues)
            {
                var bitcoinExchangeRate = keyValue.Value;
                bitcoinExchangeRate.Currency = keyValue.Key;

                tasks.Add(_database.InsertOrReplaceAsync(bitcoinExchangeRate));
            }

            await Task.WhenAll(tasks);
        }
    }
}
