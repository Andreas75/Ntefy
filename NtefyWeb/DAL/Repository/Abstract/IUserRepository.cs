using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb.DAL.Repository.Abstract
{
    public interface IUserRepository
    {
        string GetCurrentUserId();
        string GetCurrentUserMarket();
    }
}
