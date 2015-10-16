using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NtefyWeb.Models.ViewModels
{
    public class RecordViewModel
    {
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Title { get; set; }
    }
}