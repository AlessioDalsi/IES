using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ClientViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ViewModelClient> Clients { get; set; }
    }
}
