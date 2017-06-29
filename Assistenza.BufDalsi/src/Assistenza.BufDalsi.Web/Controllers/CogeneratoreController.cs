using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data;
using Microsoft.AspNetCore.Identity;
using Assistenza.BufDalsi.Web.Data;
using Assistenza.BufDalsi.Web.Models.CogeneratoreViewModels;
using Assistenza.BufDalsi.Web.Models.ImpiantoViewModels;
using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class CogeneratoreController : Controller
    {
        ITicketData _data;

        public CogeneratoreController(ITicketData dat)
        {
            _data = dat;
        }

        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult InsertCogeneratore(int clt_Id, int ipt_Id)
        {
            InsertCogeneratoreViewModel model = new InsertCogeneratoreViewModel();
            model.cgn_Impianto = ipt_Id;
            model.clt_Id = clt_Id;
            model.ipt_Id = ipt_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult InsertCogeneratore(InsertCogeneratoreViewModel model)
        {
            Cogeneratore c = new Cogeneratore();
            c.cgn_Impianto = model.cgn_Impianto;
            c.cgn_Marca = model.cgn_Marca;
            c.cgn_Modello = model.cgn_Modello;
            c.cgn_Potenza = model.cgn_Potenza;
            c.cgn_Serie = model.cgn_Serie;
            _data.InsertCogeneratore(c);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });

        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult DeleteCogeneratore(int cgn_Id, int ipt_Id, int clt_Id)
        {
            DeleteCogeneratoreViewModel model = new DeleteCogeneratoreViewModel();
            model.Id = cgn_Id;
            model.clt_Id = clt_Id;
            model.ipt_Id = ipt_Id;
            return PartialView("DeleteCogeneratore", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult DeleteCogeneratore(DeleteCogeneratoreViewModel model)
        {
            DetailsViewModel model2 = new DetailsViewModel();
            model2.clt = _data.GetClient(model.clt_Id);
            model2.imp = _data.GetImpiantoById(model.ipt_Id);
            _data.DeleteCogeneratore(model.Id);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model2.imp.ipt_Id, clt_Id = model2.clt.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateCogeneratore(int cgn_Id, int ipt_Id, int clt_Id)
        {
            var cgn = this._data.GetCogeneratore(cgn_Id);
            if (cgn == null)
                return NotFound();
            var model = new UpdateCogeneratoreViewModel(cgn_Id, cgn.cgn_Potenza, cgn.cgn_Marca, cgn.cgn_Modello, cgn.cgn_Serie, cgn.cgn_Impianto);
            model.ipt_Id = ipt_Id;
            model.clt_id = clt_Id;
            model.cgn_Id = cgn_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult UpdateCogeneratore(UpdateCogeneratoreViewModel model)
        {
            Cogeneratore cgn = new Cogeneratore(model.cgn_Id, model.cgn_Potenza, model.cgn_Marca, model.cgn_Modello, model.cgn_Serie, model.cgn_Impianto);
            this._data.UpdateCogeneratore(cgn);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_id });
        }
    }
}
