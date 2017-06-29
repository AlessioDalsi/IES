using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assistenza.BufDalsi.Web.Models.ManutenzioneViewModels
{
    public class ManutenzioneViewModel
    {
        public ManutenzioneViewModel()
        { }
        public ManutenzioneViewModel(IEnumerable<Manutenzione> de, IEnumerable<Manutenzione> s)
        {
            DaEseguire = de;
            Storico = s;
        }

        public IEnumerable<Manutenzione> DaEseguire { get; set; }
        public IEnumerable<Manutenzione> Storico { get; set; }
    }
}
