using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data;
using Assistenza.BufDalsi.Web.Models.ManutenzioneViewModels;
using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Assistenza.BufDalsi.Web.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class ManutenzioneController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        ITicketData _data;
        public ManutenzioneController(ITicketData nwd,UserManager<ApplicationUser> um)
        {
            _data = nwd;
            userManager = um;
        }
        // GET: /<controller>/
        [Authorize(Roles ="Admin,Operator")]
        [HttpGet]
        public IActionResult ViewManutenzioni()
        {
            var model = new ManutenzioneViewModel();
            model.DaEseguire = _data.GetManutenzioniDaEseguire();
            return View(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult ViewManutenzioniStorico()
        {
            var model = new ManutenzioneViewModel();
            model.Storico = _data.GetManutenzioniStorico();
            return View(model);
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> ViewManutenzioniByCliente()
        {
            var model = new ManutenzioneViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            model.DaEseguire = _data.GetManutenzioniDaEseguireByCliente(Convert.ToInt32(user.IdEsterno));
            return View(model);
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> ViewManutenzioniStoricoByCliente()
        {
            var model = new ManutenzioneViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            model.Storico = _data.GetManutenzioniStoricoByCliente(Convert.ToInt32(user.IdEsterno));
            return View(model);
        }
        //___________________________________________________________________________________________________________
        [Authorize(Roles = "User,Admin,Operator")]
        [HttpGet]
        public async Task<IActionResult> InsertManutenzioneCliente()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);//prendo l'id del utente correntemente in utilizzo della pagina
            var model = new InsertManutenzioneViewModel();
            model.impiantidaiqualiscegliere = _data.GetImpiantiByClient(Convert.ToInt32(user.IdEsterno));
            
            return PartialView(model);
        }
        [Authorize(Roles = "User,Admin,Operator")]
        [HttpPost]
        public IActionResult InsertManutenzioneCliente(InsertManutenzioneViewModel model)
        {
            var manut = new Manutenzione(model.mtz_Id, model.mtz_Scadenza, model.mtz_Addetto, model.mtz_Data, model.mtz_OreLavoro, model.mtz_Impianto, model.mtz_Effettuato, model.mtz_Descrizione, model.ipt_RagioneSociale);
            _data.InsertManutenzione(manut);

            return RedirectToAction("ViewManutenzioniByCliente");
        }
        //__________________________________________________________________________________________________________________
        //la get per l'insert si puo' evitare benissimo basta un post con una query semplice 
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult InsertManutenzione()//fare due pagine : ViewManutenzioni e ViewManutenzioniCliente e rispettivamente due funzioni:InsertManutenzione() e InsertManutenzioneCliente(id)
        {
            var model = new InsertManutenzioneViewModel();
            model.impiantidaiqualiscegliere = _data.GetImpianti();
           
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult InsertManutenzione(InsertManutenzioneViewModel model)
        {
            var manut = new Manutenzione(model.mtz_Id, model.mtz_Scadenza, model.mtz_Addetto, model.mtz_Data, model.mtz_OreLavoro, model.mtz_Impianto, model.mtz_Effettuato, model.mtz_Descrizione, model.ipt_RagioneSociale);
            Console.WriteLine("test");
            _data.InsertManutenzione(manut);
            
            return RedirectToAction("ViewManutenzioni");
            
        }
        //ZONA DELETE MANUTENZIONE________________________________________________________________________________
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult DeleteManutenzione(int mtz_Id)
        {
            if (mtz_Id == 0)
            {
                return View("ViewManutenzioni");
            }
            DeleteViewModel model = new DeleteViewModel();
            model.m = mtz_Id;
            return PartialView("DeleteManutenzione", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult DeleteManutenzioneCliente(int mtz_Id)
        {
            if (mtz_Id == 0)
            {
                return View("ViewManutenzioni");
            }
            DeleteViewModel model = new DeleteViewModel();
            model.m = mtz_Id;
            return PartialView("DeleteManutenzioneCliente", model);
        }
        [Authorize(Roles = "User,Admin,Operator")]
        [HttpPost, ActionName("DeleteManutenzioneCliente")]
        [HttpPost]
        public ActionResult DeleteConfirmedCliente(int mtz_Id)
        {
            DeleteViewModel model = new DeleteViewModel();
            model.m = mtz_Id;
            _data.DeleteManutenzione(model.m);
            return RedirectToAction("ViewManutenzioniByCliente");
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost, ActionName("DeleteManutenzione")]
        [HttpPost]
        public ActionResult DeleteConfirmed(int mtz_Id)
        {
            DeleteViewModel model = new DeleteViewModel();
            model.m = mtz_Id;
            _data.DeleteManutenzione(model.m);
            return RedirectToAction("ViewManutenzioni");
        }
        //ZONA UPDATE MANUTENZIONE________________________________________________________________________________
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateManutenzione(int mtz_Id)
        {
            var maintenance = this._data.GetManutenzione(mtz_Id);
            if (maintenance == null)
                return NotFound();

            var model = new UpdateManutenzioneViewModel(mtz_Id,
                                                        maintenance.mtz_Scadenza,
                                                        maintenance.mtz_Addetto,
                                                        maintenance.mtz_Data,
                                                        maintenance.mtz_OreLavoro,
                                                        maintenance.mtz_Impianto,
                                                        maintenance.mtz_Effettuato,
                                                        maintenance.mtz_Descrizione,
                                                        maintenance.ipt_RagioneSociale);
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult UpdateManutenzione(UpdateManutenzioneViewModel m)
        {
                Manutenzione man = new Manutenzione(
                       m.mtz_Id,
                       m.mtz_Scadenza,
                       m.mtz_Addetto,
                       m.mtz_Data,
                       m.mtz_OreLavoro,
                       m.mtz_Impianto,
                       m.mtz_Effettuato,
                       m.mtz_Descrizione,
                       m.ipt_RagioneSociale
                       );
                this._data.UpdateManutenzione(man);
                return RedirectToAction("ViewManutenzioni");
        }
        [Authorize(Roles = "User,Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateManutenzioneCliente(int mtz_Id)
        {
            var maintenance = this._data.GetManutenzione(mtz_Id);
            if (maintenance == null)
                return NotFound();

            var model = new UpdateManutenzioneViewModel(mtz_Id,
                                                        maintenance.mtz_Scadenza,
                                                        maintenance.mtz_Addetto,
                                                        maintenance.mtz_Data,
                                                        maintenance.mtz_OreLavoro,
                                                        maintenance.mtz_Impianto,
                                                        maintenance.mtz_Effettuato,
                                                        maintenance.mtz_Descrizione,
                                                        maintenance.ipt_RagioneSociale);
            return PartialView(model);
        }
        [Authorize(Roles = "User,Admin,Operator")]
        [HttpPost]
        public ActionResult UpdateManutenzioneCliente(UpdateManutenzioneViewModel m)
        {
            Manutenzione man = new Manutenzione(
                   m.mtz_Id,
                   m.mtz_Scadenza,
                   m.mtz_Addetto,
                   m.mtz_Data,
                   m.mtz_OreLavoro,
                   m.mtz_Impianto,
                   m.mtz_Effettuato,
                   m.mtz_Descrizione,
                   m.ipt_RagioneSociale
                   );
            this._data.UpdateManutenzione(man);
            return RedirectToAction("ViewManutenzioniByCliente");
        }

    }
}
