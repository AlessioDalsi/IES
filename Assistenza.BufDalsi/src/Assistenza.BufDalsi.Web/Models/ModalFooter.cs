using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models
{
    public class ModalFooter
    {
        public string SubmitButtonText { get; set; } = "Invia";
        public string CancelButtonText { get; set; } = "Cancella";
        public string SubmitButtonID { get; set; } = "btn-submit";
        public string CancelButtonID { get; set; } = "btn-cancel";
        public bool OnlyCancelButton { get; set; }
    }
}
