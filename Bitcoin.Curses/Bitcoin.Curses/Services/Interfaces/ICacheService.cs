using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitcoin.Curses.Models;

namespace Bitcoin.Curses.Services.Interfaces
{
    public interface ICacheService
    {
        Task<IDictionary<string, BitcoinExchangeRate>> LoadAsync();
        Task SaveAsync(IDictionary<string, BitcoinExchangeRate> bitcoinRateValues);
    }
}
