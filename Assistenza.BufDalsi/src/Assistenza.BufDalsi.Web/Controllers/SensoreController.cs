using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data;
using Microsoft.AspNetCore.Identity;
using Assistenza.BufDalsi.Web.Data;
using Assistenza.BufDalsi.Web.Models.SensoreViewModels;
using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class SensoreController : Controller
    {

        ITicketData _data;
        private readonly UserManager<ApplicationUser> _userManager;
        public SensoreController(ITicketData dat, UserManager<ApplicationUser> userMngr)
        {
            _data = dat;
            _userManager = userMngr;
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult InsertSensore(int vsc_Id, int ipt_Id, int clt_Id)
        {
            InsertSensoreViewModel model = new InsertSensoreViewModel();
            model.ipt_Id = ipt_Id;
            model.clt_Id = clt_Id;
            model.ssr_Vasca = vsc_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult InsertSensore(InsertSensoreViewModel model)
        {
            Sensore help = new Sensore();
            help.ssr_Marca= model.ssr_Marca;
            help.ssr_Modello= model.ssr_Modello;
            help.ssr_Nome= model.ssr_Nome;
            help.ssr_Serie= model.ssr_Serie;
            help.ssr_UltimaInstallazione= model.ssr_UltimaInstallazione;
            help.ssr_Vasca= model.ssr_Vasca;
            _data.InsertSensore(help);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult DeleteSensore(int Id, int ipt_Id, int clt_Id)
        {
            DeleteSensoreViewModel model = new DeleteSensoreViewModel();
            model.Id = Id;
            model.clt_Id = clt_Id;
            model.ipt_Id = ipt_Id;
            return PartialView("DeleteSensore", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult DeleteSensore(DeleteSensoreViewModel model)
        {
            _data.DeleteSensore(model.Id);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateSensore(int ssr_Id, int ipt_Id, int clt_Id)
        {
            var snsr = this._data.GetSensore(ssr_Id);
            if (snsr == null)
                return NotFound();
            var model = new UpdateSensoreViewModel(ssr_Id, snsr.ssr_Nome, snsr.ssr_Modello, snsr.ssr_Marca,  snsr.ssr_Serie, snsr.ssr_UltimaInstallazione, snsr.ssr_Vasca);
            model.ipt_Id = ipt_Id;
            model.clt_id = clt_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult UpdateSensore(UpdateSensoreViewModel model)
        {
            Sensore snsr = new Sensore(model.ssr_Id, model.ssr_Nome, model.ssr_Modello, model.ssr_Marca,  model.ssr_Serie, model.ssr_UltimaInstallazione, model.ssr_Vasca);
            this._data.UpdateSensore(snsr);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_id });
        }
    }
}
