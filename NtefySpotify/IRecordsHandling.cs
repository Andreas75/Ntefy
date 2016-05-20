using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefySpotify
{
    public interface IRecordsHandling
    {
        Task<T> GetSpecificRecord<T>(string recordId, string token);
        Task<T> SearchForRecord<T>(string artist, string title, string token, string userMarket = null);
        Task<T> GetMultipleRecords<T>(string recordIds, string token);
        Task<T> DoRequest<T>(string url, string token);
    }
}
