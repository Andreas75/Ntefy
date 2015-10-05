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
        void AddRequest(Request request);
    }
}
