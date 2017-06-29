using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data;
using Assistenza.BufDalsi.Web.Models.ImpiantoViewModels;
using Microsoft.AspNetCore.Authorization;
using Assistenza.BufDalsi.Web.Data;
using Microsoft.AspNetCore.Identity;
using Assistenza.BufDalsi.Data.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class ImpiantoController : Controller
    {
        ITicketData _data;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImpiantoController (ITicketData dat,UserManager<ApplicationUser> userMngr)
        {
            _data = dat;
            _userManager = userMngr;
        }
        // GET: /<controller>/
        [Authorize(Roles ="Operator,Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            ImpiantoIndexViewModel model = new ImpiantoIndexViewModel();
            model.imp     =    _data.GetImpianti();
            model.regioni =    _data.GetRegions();
            return View(model);
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> ViewImpiantiByClient()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ImpiantoIndexViewModel model = new ImpiantoIndexViewModel();
            model.imp = _data.GetImpiantiByClient(Convert.ToInt32(user.IdEsterno));
            model.regioni = _data.GetRegions();
            return View(model);
        }
        [Authorize(Roles = "Admin,Operator,User")]
        [HttpGet]
        public IActionResult ImpiantoFullInfo(int ipt_Id,int clt_Id)
        {
            DetailsViewModel model = new DetailsViewModel();
            model.clt = _data.GetClient(clt_Id);
            model.imp = _data.GetImpiantoById(ipt_Id);
            if (_data.GetCogeneratoreByImpianto(ipt_Id) != null)
            {
                model.cogeneratori = _data.GetCogeneratoreByImpianto(ipt_Id).ToList();
            }
            if (_data.GetVascheByImpianto(ipt_Id)!=null)
            {
                model.vasche = _data.GetVascheByImpianto(ipt_Id).ToList();             
                foreach (var v in model.vasche)
                {   
                    if(_data.GetAgitatoriByVasche(v.vsc_Id)!=null)
                    {
                        model.agitatori.AddRange( _data.GetAgitatoriByVasche(v.vsc_Id).ToList());
                    }
                    if(_data.GetSensoriByVasche(v.vsc_Id) != null)
                    {
                        model.sensori.AddRange(_data.GetSensoriByVasche(v.vsc_Id).ToList());
                    }
                }
            }
            if (_data.GetGenericoByImpianto(ipt_Id) != null)
            {
                model.componentiGenerici.AddRange(_data.GetGenericoByImpianto(ipt_Id).ToList());
            }

            return View(model);
        }//funzione che torna tutti i dati ed i controlli  riguardo l'impianto
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult InsertImpianto()
        {
            var model = new InsertImpiantoViewModel();
            model.clientidaiqualiscegliere = _data.GetClients();
            model.regionidallequaliscegliere = _data.GetRegions();
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult InsertImpianto(InsertImpiantoViewModel model)
        {
             _data.InsertImpianto(model.imp);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult DeleteImpianto(int ipt_Id)
        {
            if (ipt_Id == 0)
            {
                return View("Index");
            }
            DeleteImpiantoViewModel model = new DeleteImpiantoViewModel();
            model.i = ipt_Id;
            return PartialView("DeleteImpianto", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost, ActionName("DeleteImpianto")]
        [HttpPost]
        public ActionResult DeleteConfirmed(int ipt_Id)
        {
            DeleteImpiantoViewModel model = new DeleteImpiantoViewModel();
            model.i = ipt_Id;
            _data.DeleteImpianto(model.i);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult UpdateImpianto(int ipt_Id, int clt_Id)
        {
            var facility = this._data.GetImpiantoById(ipt_Id);
            if (facility == null)
                return NotFound();

            var model = new UpdateImpiantoViewModel(ipt_Id,
                                                        facility.ipt_PosizioneLat,
                                                        facility.ipt_PosizioneLong,
                                                        facility.ipt_PotenzaNominale,
                                                        facility.ipt_RagioneSociale,
                                                        facility.ipt_Cliente,
                                                        facility.ipt_Torcia,
                                                        facility.ipt_Separatore,
                                                        facility.ipt_Soffiante,
                                                        facility.ipt_Pompa,
                                                        facility.ipt_Regione);
            model.clientidaiqualiscegliere = _data.GetClients();
            model.regionidallequaliscegliere = _data.GetRegions();
            model.RagioneSocialeClienteCorrente = _data.GetClient(facility.ipt_Cliente).clt_RagioneSociale;
            model.RegioneCorrente = _data.GetRegion(facility.ipt_Regione).rgn_Nome;
            return PartialView("UpdateImpianto",model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult UpdateImpianto(UpdateImpiantoViewModel m)
        {
            Impianto impia = new Impianto(
                   m.ipt_Id,
                   m.ipt_PosizioneLat,
                   m.ipt_PosizioneLong,
                   m.ipt_PotenzaNominale,
                   m.ipt_RagioneSociale,
                   m.ipt_Cliente,
                   m.ipt_Torcia,
                   m.ipt_Separatore,
                   m.ipt_Soffiante,
                   m.ipt_Pompa,
                   m.ipt_Regione);
            this._data.UpdateImpianto(impia);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = m.ipt_Id, clt_Id = m.ipt_Cliente });

        }
    }
}
