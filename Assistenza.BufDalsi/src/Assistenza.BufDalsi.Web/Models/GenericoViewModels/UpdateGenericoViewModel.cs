using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.GenericoViewModels
{
    public class UpdateGenericoViewModel
    {
        public int ipt_Id { get; set; }
        public int clt_Id { get; set; }
        public int gnr_Id { get; set; }
        public string gnr_Nome { get; set; }
        public string gnr_Marca { get; set; }
        public string gnr_Modello { get; set; }
        public string gnr_Serie { get; set; }
        public string gnr_Descrizione { get; set; }
        [DataType(DataType.Date)]
        public DateTime gnr_UltimaInstallazione { get; set; }
        [DataType(DataType.Date)]
        public DateTime gnr_UltimoIntervento { get; set; }
        public float gnr_OreUltimoIntervento { get; set; }
        public int gnr_Impianto { get; set; }
        public Boolean gnr_Rimosso { get; set; }
        public UpdateGenericoViewModel()
        {
            gnr_UltimaInstallazione = new DateTime(1900, 01, 01);
            gnr_UltimoIntervento = new DateTime(1900, 01, 01);
        }
        public UpdateGenericoViewModel(int id, string nome, DateTime ultinst, DateTime ultint, float oreuint, string mar, string mod, string sernum, Boolean rimo,string desc, int ipt)
        {
            gnr_Id = id;
            gnr_Nome = nome;
            gnr_UltimaInstallazione = ultinst;
            gnr_UltimoIntervento = ultint;
            gnr_OreUltimoIntervento = oreuint;
            gnr_Marca = mar;
            gnr_Modello = mod;
            gnr_Serie = sernum;
            gnr_Rimosso = rimo;
            gnr_Impianto = ipt;
            gnr_Descrizione =desc;
        }
    }
}
