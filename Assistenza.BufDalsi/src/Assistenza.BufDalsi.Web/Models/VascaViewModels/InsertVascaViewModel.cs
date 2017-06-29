using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.VascaViewModels
{
    public class InsertVascaViewModel
    {
        public InsertVascaViewModel()
        {
            v = new Vasca();
            v.vsc_Impianto = new int();
        }
        public int ipt_Id { get; set; }
        public int clt_Id { get; set; }
        public Vasca v { get; set; }
    }
}
