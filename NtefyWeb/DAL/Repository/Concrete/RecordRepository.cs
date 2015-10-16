using NtefyWeb.Business;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL.Repository.Concrete
{
    public class RecordRepository : IRecordRepository
    {
        public RequestContext dbContext;

        public RecordRepository()
        {
            dbContext = new RequestContext();
        }

        public void AddRecord(Record record)
        {
            if (record != null)
            {
                var cachedRecords = AlbumCache.GetFromCache();
                var isDublicate = cachedRecords.Any(x => x.Artist.ToLower() == record.Artist.ToLower() && x.Title.ToLower() == record.Title.ToLower());
                if(!isDublicate)
                {
                    dbContext.Records.Add(record);
                    dbContext.SaveChanges();
                    AlbumCache.UpdateCache();
                }
               
            }
        }
    }
}


                  