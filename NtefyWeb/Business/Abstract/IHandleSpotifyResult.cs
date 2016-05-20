using NtefySpotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb.Business.Abstract
{
    public interface IHandleSpotifyResult
    {
        Task<FullAlbum> CompareFoundAlbumToRequest(string requestedArtist, string requestedTitle, List<SimpleAlbum> foundAlbums, string token);
    }
}
