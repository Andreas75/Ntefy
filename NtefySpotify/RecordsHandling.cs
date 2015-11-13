using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NtefySpotify.Models;


namespace NtefySpotify
{
    public class RecordsHandling
    {
        public async Task<T> GetSpecificRecord<T>(string recordId, string token)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://api.spotify.com/v1/albums/");
            sb.Append(recordId);
            var result = await DoRequest<T>(sb.ToString(), token);
            return result;
        }

        public async Task<T> SearchForRecord<T>(string artist, string title, string token, string userMarket)
        {            
            var encodedTitle = Uri.EscapeDataString(title);
            var encodedArtist = Uri.EscapeDataString(artist);
            string searchTermsAlbumAndArtist = string.Format("album:{0}{1}artist:{2}", encodedTitle, "%20", encodedArtist);
            
            var searchTermType = "&type=album";
            var searchMarket = string.Format("&market={0}", userMarket);

            StringBuilder sb = new StringBuilder();
            sb.Append("https://api.spotify.com/v1/search?q=");
            sb.Append(searchTermsAlbumAndArtist);
            sb.Append(searchTermType);
            sb.Append(searchMarket);
            var result = await DoRequest<T>(sb.ToString(), token);
            return result;            
        }

        public async Task<T> GetMultipleRecords<T>(string recordIds, string token)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://api.spotify.com/v1/albums/?ids=");
            sb.Append(recordIds);
            var result = await DoRequest<T>(sb.ToString(), token);
            return result;
        }

        private async Task<T> DoRequest<T>(string url, string token)
        {
            try
            {       
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json; charset=utf-8";

                T type = default(T);

                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            type = JsonConvert.DeserializeObject<T>(responseFromServer);
                        }
                    }
                }
                return type;
            }
            catch (Exception ex)
            {
                var exception = ex;
                throw;
            }
        }

        
    }
}
