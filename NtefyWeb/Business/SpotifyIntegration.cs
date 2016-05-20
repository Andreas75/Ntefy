using Hangfire;
using NtefySpotify;
using NtefySpotify.Models;
using NtefyWeb.DAL.Models;
using NtefyWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NtefyWeb.Business.Abstract;

namespace NtefyWeb.Business
{
    public class SpotifyIntegration : ISpotifyIntegration
    {        

        public async Task<AlbumSearch> SerachForAlbum(string artist, string title, string token, string userMarket)
        {
            var recordsHandling = new RecordsHandling();
            var albumIds = new List<string>();
            var album = await recordsHandling.SearchForRecord<AlbumSearch>(artist, title, token, userMarket);          
            return album;
        }

        public async Task<FullAlbum> GetAlbum(string id, string token)
        {
            var album = await new RecordsHandling().GetSpecificRecord<FullAlbum>(id, token);
            return album;
        }

        public async Task<MultipleAlbumSearch> GetMultibleAlbums(string ids, string token)
        {
            return await new RecordsHandling().GetMultipleRecords<MultipleAlbumSearch>(ids, token);
        }        
    }
}