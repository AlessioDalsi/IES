using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ImpiantoViewModels
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        { 
            imp = new Impianto();
            clt = new Client();
            cogeneratori = new List<Cogeneratore>();
            vasche = new List<Vasca>();
            sensori = new List<Sensore>();
            agitatori = new List<Agitatore>();
            componentiGenerici = new List<Generico>();
        }
        public DetailsViewModel(Impianto I,Client C,List<Cogeneratore> Co, List<Vasca> Va,
                                List<Sensore> Se, List<Agitatore> Agi,List<Generico> Gen)
        {
            imp = I;
            clt = C;
            cogeneratori = Co;
            vasche = Va;
            sensori = Se;
            agitatori = Agi;
            componentiGenerici = Gen;
        }
        public Impianto imp { get; set; }
        public Client clt { get; set; }
        public List<Cogeneratore> cogeneratori { get; set; }
        public List<Vasca> vasche { get; set; }
        public List<Sensore> sensori { get; set; }
        public List<Agitatore> agitatori { get; set; }
        public List<Generico> componentiGenerici { get; set; }
    }
}
