using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ClientViewModels
{
    public class UpdateClienteViewModel
    {
        public UpdateClienteViewModel(){}
        public UpdateClienteViewModel(int id,string rs ,string idz,string  mail,string tel,string mob)
        {
            clt_Id = id;
            clt_RagioneSociale = rs;
            clt_Indirizzo = idz;
            clt_Mail = mail;
            clt_Telefono = tel;
            clt_Mobile = mob;
        }
        public int clt_Id{ get; set; }
        public string clt_RagioneSociale { get; set; }
        public string clt_Indirizzo { get; set; }
        public string clt_Mail { get; set; }
        public string clt_Telefono { get; set; }
        public string clt_Mobile { get; set; }
    }
}
