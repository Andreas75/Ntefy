using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtefyWeb.Models.ViewModels
{
    public interface IRecordModel
    {        
        string Artist { get; set; }
        string Title { get; set; }
        RecordModel CreateRecord(string Title, string Artist);
    }
}
