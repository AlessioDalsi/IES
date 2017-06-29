using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ClientViewModels
{
    public class ViewModelClient
    {
        public IEnumerable<Client> Customers { get; set; }
        public IEnumerable<Impianto> Implants { get; set; }

        public ViewModelClient()
        {

        }

        public ViewModelClient(Client client, Impianto implant)
        {
            if (client == null)
                return;
            this.clt_Id = client.clt_Id;
            this.clt_RagioneSociale = client.clt_RagioneSociale;
            this.clt_Indirizzo = client.clt_Indirizzo;
            this.clt_Mail = client.clt_Mail;
            this.clt_Telefono = client.clt_Telefono;
            this.clt_Mobile = client.clt_Mobile;

            this.ipt_Id = implant.ipt_Id;
            this.ipt_PosizioneLat = implant.ipt_PosizioneLat;
            this.ipt_PosizioneLong = implant.ipt_PosizioneLong;
            this.ipt_PotenzaNominale = implant.ipt_PotenzaNominale;
            this.ipt_RagioneSociale = implant.ipt_RagioneSociale;
            this.ipt_Cliente = implant.ipt_Cliente;
            this.ipt_Torcia = implant.ipt_Torcia;
            this.ipt_Separatore = implant.ipt_Separatore;
            this.ipt_Soffiante = implant.ipt_Soffiante;
            this.ipt_Pompa = implant.ipt_Pompa;
            this.ipt_Regione = implant.ipt_Regione;
        }


        public int clt_Id { get; set; }
        public string clt_RagioneSociale { get; set; }
        public string clt_Indirizzo { get; set; }
        public string clt_Mail { get; set; }
        public string clt_Telefono { get; set; }
        public string clt_Mobile { get; set; }


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
    }
}
