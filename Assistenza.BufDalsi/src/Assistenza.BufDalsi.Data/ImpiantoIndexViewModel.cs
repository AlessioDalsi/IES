using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ImpiantoViewModels
{
    public class ImpiantoIndexViewModel
    {
        public IEnumerable<Impianto> imp { get; set; }
        public IEnumerable<Region> regioni { get; set; }
    }
}
