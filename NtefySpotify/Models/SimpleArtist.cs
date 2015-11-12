﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefySpotify.Models
{
    public class SimpleArtist
    {
        [JsonProperty("external_urls")]
        public Dictionary<String, String> ExternalUrls { get; set; }

        [JsonProperty("href")]
        public String Href { get; set; }

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; }

        [JsonProperty("uri")]
        public String Uri { get; set; }
    }
}
