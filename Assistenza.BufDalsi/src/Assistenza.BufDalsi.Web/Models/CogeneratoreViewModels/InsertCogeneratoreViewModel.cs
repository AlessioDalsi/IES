using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assistenza.BufDalsi.Web.Models.CogeneratoreViewModels
{
    public class InsertCogeneratoreViewModel
    {
        public InsertCogeneratoreViewModel(){}
        public int cgn_Id { get; set; }
        public int cgn_Potenza { get; set; }
        public string cgn_Marca { get; set; }
        public string cgn_Modello { get; set; }
        public string cgn_Serie { get; set; }
        public int cgn_Impianto { get; set; }
        public int ipt_Id { get; set; }
        public int clt_Id { get; set; }

    }
}
