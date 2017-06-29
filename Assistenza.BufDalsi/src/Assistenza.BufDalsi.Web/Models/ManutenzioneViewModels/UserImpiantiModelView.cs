using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models.ManutenzioneViewModels
{
    public class UserImpiantiModelView
    {
        public IEnumerable<Impianto> imp { get; set; }
    }
}
