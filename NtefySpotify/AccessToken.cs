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
        private SpotifyToken _spotifyToken;

        public AccessToken()
        {
            _spotifyToken = new SpotifyToken();
        }
       

        public async Task<SpotifyToken> GetAccessToken()
        {
            //SpotifyToken token = new SpotifyToken();

            string postString = string.Format("grant_type=client_credentials");
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);

            string url = "https://accounts.spotify.com/api/token";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic YzM3OWM2YzZiYmMxNDRjOThkNzFmM2QyZmFkYjVhMWU6ODQ5OTA4ZWUxOGI1NGI5OTk3N2I4MjA3MmVkMTAwZGQ=");
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
                                _spotifyToken = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
                                _spotifyToken.createdTime = DateTime.Now;
                                var expires = _spotifyToken.expires_in;
                            }
                        }
                    }
                }

                return _spotifyToken;
            }
            catch (Exception ex)
            {
                var exception = ex;
                throw;
            }

        }

        public async Task<string> ValidateAccessToken(SpotifyToken token)
        {
            if (token != null)
            {
                if (token.createdTime > DateTime.Now.AddHours(-1))
                {
                    return token.access_token;
                }
            }
            var spotifyToken = await GetAccessToken();
            return spotifyToken.access_token;
        }
       
    }

    [Serializable]
    public class SpotifyToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public DateTime createdTime { get; set; }
    }
}
