using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Data.Models
{
    public class Agitatore
    {
        public int agt_Id { get; set; }
        public string agt_Nome { get; set; }
        [DataType(DataType.Date)]
        public DateTime agt_UltimaInstallazione { get; set; }
        [DataType(DataType.Date)]
        public DateTime agt_UltimoIntervento { get; set; }
        public int agt_OreUltimoIntervento { get; set; }
        public string agt_Marca { get; set; }
        public string agt_Modello { get; set; }
        public string agt_SerialNumber { get; set; }
        public Boolean agt_Rimosso { get; set; }
        public int agt_Vasca { get; set; }

        public Agitatore()
        {
            agt_UltimaInstallazione = new DateTime(1900, 01, 01);
            agt_UltimoIntervento = new DateTime(1900, 01, 01);
        }
        public Agitatore(int id, string nome, DateTime ultinst, DateTime ultint, int oreuint, string mar, string mod, string sernum, Boolean rimo, int vsc)
        {
            agt_Id = id;
            agt_Nome = nome;
            agt_UltimaInstallazione = ultinst;
            agt_UltimoIntervento = ultint;
            agt_OreUltimoIntervento = oreuint;
            agt_Marca = mar;
            agt_Modello = mod;
            agt_SerialNumber = sernum;
            agt_Rimosso = rimo;
            agt_Vasca = vsc;
        }
    }
}
