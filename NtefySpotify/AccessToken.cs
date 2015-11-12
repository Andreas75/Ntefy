using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace NtefySpotify
{
    public class AccessToken
    {
        public async Task<string> GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();

            string postString = string.Format("grant_type=client_credentials");
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);

            string url = "https://accounts.spotify.com/api/token";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic YjlkZjg0NGQzNjRjNGY5OWFhM2JhMzdmNTYyNjI5Nzc6MjU1MTU0ZmQ5NjRhNGVkYWI5NmIzMTUyMjRkZGY1ZmI=");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;


            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse response = await request.GetResponseAsync())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                string responseFromServer = reader.ReadToEnd();
                                token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
                                var expires = token.expires_in;
                            }
                        }
                    }
                }
                return token.access_token;
            }
            catch (Exception ex)
            {
                var exception = ex;
                throw;
            }

        }

       
    }

    public class SpotifyToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
