using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NtefyWeb.Models.ViewModels
{
    public class RecordModel : IRecordModel
    {
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Title { get; set; }


        public RecordModel CreateRecord(string recordTitle, string recordArtist)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Artist))
            {
                return new RecordModel { Title = recordTitle, Artist = recordArtist };
            }
            return null;
        }
    }
}