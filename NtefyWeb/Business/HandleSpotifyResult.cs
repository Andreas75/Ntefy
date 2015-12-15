using NtefySpotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NtefyWeb.Business
{
    public static class HandleSpotifyResult
    {
        public static async Task<FullAlbum> CompareFoundAlbumToRequest(string requestedArtist, string requestedTitle, List<SimpleAlbum> foundAlbums, string token)
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