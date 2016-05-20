using NtefyWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb.DAL
{
    public interface IRequestCache
    {
        void UpDateAllCache();
        List<Request> GetAllFromCache();
        void UpDateAllUnfilledRequests();
        List<Request> GetAllUnfilledRequests();
    }
}
