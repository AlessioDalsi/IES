using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.VascaViewModels
{
    public class UpdateVascaViewModel
    {

        public int ipt_Id { get; set; }
        public int clt_id { get; set; }
        public string vsc_Nome { get; set; }
        public int vsc_Id { get; set; }
        public double vsc_Altezza { get; set; }
        public Boolean vsc_Coperta { get; set; }
        public Boolean vsc_Riscaldata { get; set; }
        public Boolean vsc_Recupero { get; set; }
        public Boolean vsc_Interrata { get; set; }
        public double vsc_Interramento { get; set; }
        public int vsc_NSoffiantine { get; set; }
        public double vsc_Diametro { get; set; }
        public int vsc_Impianto { get; set; }

        public UpdateVascaViewModel() { }

        public UpdateVascaViewModel(string Nome, int Id, double Altezza, Boolean Coperta, Boolean Riscaldata, Boolean Recupero, Boolean Interrata, double Interramento, int nSoffiantine, double Diametro, int Impianto)
        {
            vsc_Nome = Nome;
            vsc_Id = Id;
            vsc_Altezza = Altezza;
            vsc_Coperta = Coperta;
            vsc_Riscaldata = Riscaldata;
            vsc_Recupero = Recupero;
            vsc_Interrata = Interrata;
            vsc_Interramento = Interramento;
            vsc_NSoffiantine = nSoffiantine;
            vsc_Diametro = Diametro;
            vsc_Impianto = Impianto;
        }
    }
}
