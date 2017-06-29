using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ClientViewModels
{
    public class DetailViewModel
    {
        public Client Client { get; set; }
        public IEnumerable<Impianto> Implants { get; set; }
    }
}
