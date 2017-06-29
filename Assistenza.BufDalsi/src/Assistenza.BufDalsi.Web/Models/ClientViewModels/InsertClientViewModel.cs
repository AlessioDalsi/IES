using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ClientViewModels
{
    public class InsertClientViewModel
    {
        public InsertClientViewModel()
        {
            c = new Client();
        }
        public Client c { get; set; }
    }
}
