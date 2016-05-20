using NtefySpotify.Models;
using NtefyWeb.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NtefyWeb.Business
{
    public class HandleSpotifyResult : IHandleSpotifyResult
    {
        //private SpotifyIntegration _spotifyIntegration;

        public HandleSpotifyResult()
        {
            //_spotifyIntegration = new SpotifyIntegration();
        }
        public async Task<FullAlbum> CompareFoundAlbumToRequest(string requestedArtist, string requestedTitle, List<SimpleAlbum> foundAlbums, string token)
        {            
            foreach (var album in foundAlbums)
            {
                var fullAlbum = await new SpotifyIntegration().GetAlbum(album.Id, token);
                if (fullAlbum.Artists.Any(x => x.Name.ToLower() == requestedArtist.ToLower()))
                {
                    return fullAlbum;
                }
            }
            return null;
        }
    }
}