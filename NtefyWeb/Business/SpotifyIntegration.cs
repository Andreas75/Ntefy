using Hangfire;
using NtefySpotify;
using NtefySpotify.Models;
using NtefyWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NtefyWeb.Business
{
    public class SpotifyIntegration
    {
        private RecordsHandling recordsHandling;
        public SpotifyIntegration()
        {
            recordsHandling = new RecordsHandling();
        }
        public async Task<AlbumSearch> SerachForAlbum(Record record, string token, string userMarket)
        {
            var albumIds = new List<string>();
            var album = await recordsHandling.SearchForRecord<AlbumSearch>(record.Artist, record.Title, token, userMarket);
            //if (album.Albums.Items.Count > 0)
            //{                
            //    var foundAlbum = await HandleSpotifyResult.CompareFoundAlbumToRequest(record.Artist, record.Title, album.Albums.Items, token);
            //    if (foundAlbum != null)
            //    {
            //        return foundAlbum;
            //    }                
            //}                  
           
            return album;
        }

        public async Task<FullAlbum> GetAlbum(string id, string token)
        {
            var album = await recordsHandling.GetSpecificRecord<FullAlbum>(id, token);
            return album;
        }

        public async Task<MultipleAlbumSearch> GetMultibleAlbums(string ids, string token)
        {
            return await recordsHandling.GetMultipleRecords<MultipleAlbumSearch>(ids, token);
        }        
    }
}