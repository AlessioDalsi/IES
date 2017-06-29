using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ManutenzioneViewModels
{
    public class UpdateManutenzioneViewModel
    {
        public UpdateManutenzioneViewModel() {
            mtz_Data = DateTime.Today;
            mtz_Scadenza = DateTime.Today;
        }
        public UpdateManutenzioneViewModel(int Id, DateTime Scadenza, string Addetto, DateTime Data, float OreLavoro, int Impianto, Boolean Effettuato, string Descrizione, string RagioneSociale)
        {
            mtz_Id = Id;
            mtz_Scadenza = Scadenza;
            mtz_Addetto = Addetto;
            mtz_Data = Data;
            mtz_OreLavoro = OreLavoro;
            mtz_Impianto = Impianto;
            mtz_Effettuato = Effettuato;
            mtz_Descrizione = Descrizione;
            ipt_RagioneSociale = RagioneSociale;
        }
        public int mtz_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime mtz_Scadenza { get; set; }
        public string mtz_Addetto { get; set; }
        [DataType(DataType.Date)]
        public DateTime mtz_Data { get; set; }
        public float mtz_OreLavoro { get; set; }
        public int mtz_Impianto { get; set; }
        public Boolean mtz_Effettuato { get; set; }
        public string mtz_Descrizione { get; set; }
        public string ipt_RagioneSociale { get; set; }
    }
}
