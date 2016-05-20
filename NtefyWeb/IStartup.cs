using NtefyWeb.Business.Abstract;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb
{
    public interface IStartup
    {        
        void Configuration(IAppBuilder app);
    }
}
