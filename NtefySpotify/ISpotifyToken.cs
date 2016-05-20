using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefySpotify
{
    public interface ISpotifyToken
    {
        string access_token { get; set; }
        string token_type { get; set; }
        int expires_in { get; set; }
        DateTime createdTime { get; set; }
    }
}
