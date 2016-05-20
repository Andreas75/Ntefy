
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace NtefyWeb.DAL.Repository.Concrete
{
    public class RecordRepository : IRecordRepository
    {
        //public RequestContext dbContext;        
        private RequestRepository _requestRepository;

        public RecordRepository()
        {
            //dbContext = new RequestContext();
            _requestRepository = new RequestRepository();
        }

        public void AddRecord(Record record)
        {
            if (record != null)
            {
                var dbContext = new RequestContext();
                    var cachedRecords = AlbumCache.GetAllRecordsFromCache();
                    var isDublicate = cachedRecords.Any(x => x.Artist.ToLower() == record.Artist.ToLower() && x.Title.ToLower() == record.Title.ToLower());
                    if (!isDublicate)
                    {
                        dbContext.Records.Add(record);
                        dbContext.SaveChanges();
                        AlbumCache.UpdateCache();
                    }
                
                
                           
            }
        }

        public Guid GetIdForRecord(Record record)
        {
            var cachedRecord = AlbumCache.GetRecordFromCache(record.Artist, record.Title);
            if (cachedRecord != null)
            {
                return cachedRecord.RecordId;
            }
            else
            {
                return Guid.Empty;
            }            
        }

        public void UpdateRecordTitle(Record record, string spotifyTitle)
        {
            var dbContext = new RequestContext();
                var albums = dbContext.Records.Where(x => x.Artist == record.Artist && x.Title == record.Title);
                foreach (var album in albums)
                {
                    album.Title = spotifyTitle;
                }
                dbContext.SaveChanges();
                AlbumCache.UpdateCache();
            
            
        }

        
    }
}


                  