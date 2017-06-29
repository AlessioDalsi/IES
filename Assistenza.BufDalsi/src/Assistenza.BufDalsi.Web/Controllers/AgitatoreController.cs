using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data;
using Assistenza.BufDalsi.Web.Models.AgitatoreViewModels;
using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class AgitatoreController : Controller
    {
        ITicketData _data;
        public AgitatoreController(ITicketData dat)
        {
            _data = dat;
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult InsertAgitatore(int vsc_Id,int clt_Id, int ipt_Id)
        {
            InsertAgitatoreViewModel model = new InsertAgitatoreViewModel();
            model.agt_Vasca = vsc_Id;
            model.clt_Id = clt_Id;
            model.ipt_Id = ipt_Id;
            return PartialView("InsertAgitatore",model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult InsertAgitatore(InsertAgitatoreViewModel model)
        {
            Agitatore help = new Agitatore();
            help.agt_Marca = model.agt_Marca;
            help.agt_Modello= model.agt_Modello;
            help.agt_Nome= model.agt_Nome;
            help.agt_OreUltimoIntervento= model.agt_OreUltimoIntervento;
            help.agt_Rimosso= model.agt_Rimosso;
            help.agt_SerialNumber= model.agt_SerialNumber;
            help.agt_UltimaInstallazione= model.agt_UltimaInstallazione;
            help.agt_UltimoIntervento= model.agt_UltimoIntervento;
            help.agt_Vasca= model.agt_Vasca;
            _data.InsertAgitatore(help);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult DeleteAgitatore(int Id, int ipt_Id, int clt_Id)
        {
            DeleteAgitatoreViewModel model = new DeleteAgitatoreViewModel();
            model.Id = Id;
            model.clt_Id = clt_Id;
            model.ipt_Id = ipt_Id;
            return PartialView("DeleteAgitatore", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult DeleteAgitatore(DeleteAgitatoreViewModel model)
        {
            _data.DeleteAgitatore(model.Id);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateAgitatore(int agt_Id,int ipt_Id,int clt_Id)
        {
            var agit = this._data.GetAgitatore(agt_Id);
            if (agit == null)
                return NotFound();

            var model = new UpdateAgitatoreViewModel(agt_Id,
                                                        agit.agt_Nome,
                                                        agit.agt_UltimaInstallazione,
                                                        agit.agt_UltimoIntervento,
                                                        agit.agt_OreUltimoIntervento,
                                                        agit.agt_Marca,
                                                        agit.agt_Modello,
                                                        agit.agt_SerialNumber,
                                                        agit.agt_Rimosso,
                                                        agit.agt_Vasca
                                                     );
            model.ipt_Id = ipt_Id;
            model.clt_Id = clt_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult UpdateAgitatore(UpdateAgitatoreViewModel Model)
        {
            Agitatore agt = new Agitatore(
                                                        Model.agt_Id,
                                                        Model.agt_Nome,
                                                        Model.agt_UltimaInstallazione,
                                                        Model.agt_UltimoIntervento,
                                                        Model.agt_OreUltimoIntervento,
                                                        Model.agt_Marca,
                                                        Model.agt_Modello,
                                                        Model.agt_SerialNumber,
                                                        Model.agt_Rimosso,
                                                        Model.agt_Vasca
            );    
            this._data.UpdateAgitatore(agt);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = Model.ipt_Id, clt_Id = Model.clt_Id });
        }
    }
}
