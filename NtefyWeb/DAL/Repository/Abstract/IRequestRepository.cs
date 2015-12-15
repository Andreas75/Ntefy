using NtefyWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb.DAL.Repository.Abstract
{
    public interface IRequestRepository
    {
        void AddRequest(Guid recordId, string userId);
        List<Request> GetAllRequestsForUser(string userId);
        List<Request> GetAllRequestForAlbum(Guid recordId);
        void SetRequestAsFilled(Guid recordId, string country);
        List<Request> GetAllRequests();
    }
}
