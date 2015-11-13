using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefySpotify.Models
{
    public class MultipleAlbumSearch
    {
        [JsonProperty("albums")]
        public List<FullAlbum> Albums { get; set; }
    }
}
