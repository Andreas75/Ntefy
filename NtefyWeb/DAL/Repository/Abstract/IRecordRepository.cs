using NtefyWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL.Repository.Abstract
{
    public interface IRecordRepository
    {
        void AddRecord(Record record);
        void UpdateRecordTitle(Record record, string spotifyTitle);
        Guid GetIdForRecord(Record record);
    }
}