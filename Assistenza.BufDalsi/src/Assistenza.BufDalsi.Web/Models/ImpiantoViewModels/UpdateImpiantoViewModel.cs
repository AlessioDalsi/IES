using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ImpiantoViewModels
{
    public class UpdateImpiantoViewModel
    {
        public UpdateImpiantoViewModel() { }
        public UpdateImpiantoViewModel(int id, double lat, double lon, int pot, string rag, int clt, string tor, string sep, string sof, string pom, int reg)
        {
            ipt_Id = id;
            ipt_PosizioneLat = lat;
            ipt_PosizioneLong = lon;
            ipt_PotenzaNominale = pot;
            ipt_RagioneSociale = rag;
            ipt_Cliente = clt;
            ipt_Torcia = tor;
            ipt_Separatore = sep;
            ipt_Soffiante = sof;
            ipt_Pompa = pom;
            ipt_Regione = reg;
        }
        public int ipt_Id { get; set; }
        public double ipt_PosizioneLat { get; set; }
        public double ipt_PosizioneLong { get; set; }
        public int ipt_PotenzaNominale { get; set; }
        public string ipt_RagioneSociale { get; set; }
        public int ipt_Cliente { get; set; }
        public string ipt_Torcia { get; set; }
        public string ipt_Separatore { get; set; }
        public string ipt_Soffiante { get; set; }
        public string ipt_Pompa { get; set; }
        public int ipt_Regione { get; set; }
        public IEnumerable<Client> clientidaiqualiscegliere { get; set; }
        public IEnumerable<Region> regionidallequaliscegliere { get; set; }
        public string RagioneSocialeClienteCorrente { get; set; }
        public string RegioneCorrente { get; set; }
    }
}
