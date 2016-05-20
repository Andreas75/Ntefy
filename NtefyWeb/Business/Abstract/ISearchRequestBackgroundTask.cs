using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb.Business.Abstract
{
    public interface ISearchRequestBackgroundTask
    {        
        Task SearchForRequests();
        void FillRequest(List<string> recordIds, List<Request> requests, string token);
        void SearchRequestsSync();
        void SetUpBrackgroundTask();
    }
}
