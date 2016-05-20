using NtefySpotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb.Business.Abstract
{
    public interface ISpotifyIntegration
    {
        Task<AlbumSearch> SerachForAlbum(string artist, string title, string token, string userMarket);
        Task<FullAlbum> GetAlbum(string id, string token);
        Task<MultipleAlbumSearch> GetMultibleAlbums(string ids, string token);
    }
}
