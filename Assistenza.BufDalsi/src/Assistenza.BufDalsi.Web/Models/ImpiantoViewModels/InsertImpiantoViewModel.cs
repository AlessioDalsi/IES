using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ImpiantoViewModels
{
    public class InsertImpiantoViewModel
    {
        public Impianto imp { get; set; }
        public IEnumerable<Client> clientidaiqualiscegliere { get; set; }
        public IEnumerable<Region> regionidallequaliscegliere { get; set; }
    }
}
