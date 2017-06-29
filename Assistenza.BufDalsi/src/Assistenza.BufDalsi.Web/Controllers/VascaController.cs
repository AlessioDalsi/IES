using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Data;
using Assistenza.BufDalsi.Web.Data;
using Microsoft.AspNetCore.Identity;
using Assistenza.BufDalsi.Web.Models.VascaViewModels;
using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class VascaController : Controller
    {

        ITicketData _data;
        private readonly UserManager<ApplicationUser> _userManager;
        public VascaController(ITicketData dat, UserManager<ApplicationUser> userMngr)
        {
            _data = dat;
            _userManager = userMngr;
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult InsertVasca(int ipt_Id,int clt_Id)
        {
            InsertVascaViewModel model = new InsertVascaViewModel();
            model.ipt_Id = ipt_Id;
            model.clt_Id = clt_Id;
            model.v.vsc_Impianto = ipt_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult InsertVasca(InsertVascaViewModel model)
        {
            _data.InsertVasca(model.v);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult DeleteVasca(int vsc_Id, int ipt_Id, int clt_Id)
        {
            DeleteVascaViewModel model = new DeleteVascaViewModel();
            model.Id = vsc_Id;
            model.ipt_Id = ipt_Id;
            model.clt_Id = clt_Id;
            return PartialView("DeleteVasca", model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult DeleteVasca(DeleteVascaViewModel model)
        {
            _data.DeleteVasca(model.Id);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_Id });
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateVasca(int vsc_Id, int ipt_Id, int clt_Id)
        {
            var vsc = this._data.GetVasca(vsc_Id);
            if (vsc == null)
                NotFound();
            var model = new UpdateVascaViewModel(vsc.vsc_Nome, vsc_Id, vsc.vsc_Altezza, vsc.vsc_Coperta, vsc.vsc_Riscaldata, vsc.vsc_Recupero, vsc.vsc_Interrata, vsc.vsc_Interramento, vsc.vsc_NSoffiantine, vsc.vsc_Diametro, vsc.vsc_Impianto);
            model.ipt_Id = ipt_Id;
            model.clt_id = clt_Id;
            model.vsc_Id = vsc_Id;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult UpdateVasca(UpdateVascaViewModel model)
        {
            Vasca vsc = new Vasca(model.vsc_Nome, model.vsc_Id, model.vsc_Altezza, model.vsc_Coperta, model.vsc_Riscaldata, model.vsc_Recupero, model.vsc_Interrata, model.vsc_Interramento, model.vsc_NSoffiantine, model.vsc_Diametro, model.vsc_Impianto);
            this._data.UpdateVasca(vsc);
            return RedirectToAction("ImpiantoFullInfo", "Impianto", new { ipt_Id = model.ipt_Id, clt_Id = model.clt_id });
        }
    }
}
