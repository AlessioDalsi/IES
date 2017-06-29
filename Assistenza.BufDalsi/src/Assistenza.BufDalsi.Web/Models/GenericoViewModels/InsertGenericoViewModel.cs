using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.GenericoViewModels
{
    public class InsertGenericoViewModel
    {
        public InsertGenericoViewModel(int gnr_Id, string gnr_Nome, DateTime gnr_UltimaInstallazione, DateTime gnr_UltimoIntervento, float gnr_OreUltimoIntervento, string gnr_Marca, string gnr_Modello, string gnr_Serie, bool gnr_Rimosso, string gnr_Descrizione, int gnr_Impianto)
        {
            this.gnr_Id = gnr_Id;
            this.gnr_Nome = gnr_Nome;
            this.gnr_UltimaInstallazione = gnr_UltimaInstallazione;
            this.gnr_UltimoIntervento = gnr_UltimoIntervento;
            this.gnr_OreUltimoIntervento = gnr_OreUltimoIntervento;
            this.gnr_Marca = gnr_Marca;
            this.gnr_Modello = gnr_Modello;
            this.gnr_Serie = gnr_Serie;
            this.gnr_Rimosso = gnr_Rimosso;
            this.gnr_Descrizione = gnr_Descrizione;
            this.gnr_Impianto = gnr_Impianto;
        }

        public InsertGenericoViewModel()
        {
            gnr_UltimaInstallazione = new DateTime(1900, 01, 01);
            gnr_UltimoIntervento = new DateTime(1900, 01, 01);
        }

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
        public int clt_Id { get; set; }
        public int ipt_Id { get; set; }
    }
}
