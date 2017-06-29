using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data;
using Assistenza.BufDalsi.Web.Models.GenericoViewModels;
using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class GenericoController : Controller
    {
        ITicketData _data;
        public GenericoController(ITicketData dat)
        {
            _data = dat;
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult InsertGenerico(int clt_Id, int ipt_Id)
        {
            InsertGenericoViewModel model = new InsertGenericoViewModel();
            model.clt_Id = clt_Id;
            model.ipt_Id = ipt_Id;
            model.gnr_Impianto = ipt_Id;
            return PartialView("InsertGenerico", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult InsertGenerico(InsertGenericoViewModel model)
        {
            Generico help = new Generico();
            help.gnr_Descrizione= model.gnr_Descrizione;
            help.gnr_Impianto= model.gnr_Impianto;
            help.gnr_Marca= model.gnr_Marca;
            help.gnr_Modello= model.gnr_Modello;
            help.gnr_Nome= model.gnr_Nome;
            help.gnr_OreUltimoIntervento= model.gnr_OreUltimoIntervento;
            help.gnr_Rimosso= model.gnr_Rimosso;
            help.gnr_Serie= model.gnr_Serie;
            help.gnr_UltimaInstallazione= model.gnr_UltimaInstallazione;
            help.gnr_UltimoIntervento= model.gnr_UltimoIntervento;
            _data.InsertGenerico(help);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public ActionResult DeleteGenerico(int Id, int ipt_Id, int clt_Id)
        {
            DeleteGenericoViewModel model = new DeleteGenericoViewModel();
            model.Id = Id;
            model.clt_Id = clt_Id;
            model.ipt_Id = ipt_Id;
            return PartialView("DeleteGenerico", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult DeleteGenerico(DeleteGenericoViewModel model)
        {
            _data.DeleteGenerico(model.Id);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateGenerico(int gnr_Id, int ipt_Id, int clt_Id)
        {
            var gener = this._data.GetGenerico(gnr_Id);
            if (gener == null)
                return NotFound();

            var model = new UpdateGenericoViewModel(    gener.gnr_Id,
                                                        gener.gnr_Nome,
                                                        gener.gnr_UltimaInstallazione,
                                                        gener.gnr_UltimoIntervento,
                                                        gener.gnr_OreUltimoIntervento,
                                                        gener.gnr_Marca,
                                                        gener.gnr_Modello,
                                                        gener.gnr_Serie,
                                                        gener.gnr_Rimosso,
                                                        gener.gnr_Descrizione,
                                                        gener.gnr_Impianto
                                                     );
            model.ipt_Id = ipt_Id;
            model.clt_Id = clt_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public ActionResult UpdateGenerico(UpdateGenericoViewModel Model)
        {
            Generico gen = new Generico(
                                                        Model.gnr_Id,
                                                        Model.gnr_Nome,
                                                        Model.gnr_UltimaInstallazione,
                                                        Model.gnr_UltimoIntervento,
                                                        Model.gnr_OreUltimoIntervento,
                                                        Model.gnr_Marca,
                                                        Model.gnr_Modello,
                                                        Model.gnr_Serie,
                                                        Model.gnr_Rimosso,
                                                        Model.gnr_Descrizione,
                                                        Model.gnr_Impianto
            );
            this._data.UpdateGenerico(gen);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = Model.ipt_Id, clt_Id = Model.clt_Id });
        }

    }
}
