using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Web.Models.ClientViewModels;
using Assistenza.BufDalsi.Data;
using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class ClientController : Controller
    {
        ITicketData _data;
        public ClientController(ITicketData nwd)
        {
            _data = nwd;
        }
        // GET: /<controller>/

        [Authorize(Roles ="Operator, Admin, User")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ViewModelClient();
            model.Customers = _data.GetClients();
            return View(model);
        }

        [Authorize(Roles = "Operator, Admin, User")]
        public IActionResult Details(int id)
        {
            var model = new DetailViewModel();
            model.Implants = _data.GetImpiantiByClient(id);
            model.Client = _data.GetClient(id);
            return PartialView(model);
        }

        [Authorize(Roles = "Operator, Admin")]
        [HttpGet]
        public IActionResult InsertCliente()
        {
            InsertClientViewModel model = new InsertClientViewModel();
            return PartialView(model);
        }

        [Authorize(Roles = "Operator, Admin")]
        [HttpPost]
        public IActionResult InsertCliente(InsertClientViewModel model)
        {
            _data.InsertCliente(model.c);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Operator, Admin")]
        [HttpGet]
        public ActionResult DeleteCliente(int clt_Id)
        {
            DeleteClienteViewModel model = new DeleteClienteViewModel();
            model.clt_Id = clt_Id;
            return PartialView("DeleteCliente", model);
        }

        [Authorize(Roles = "Operator, Admin")]
        [HttpPost]
        public ActionResult DeleteCliente(DeleteClienteViewModel model)
        {
            _data.DeleteCliente(model.clt_Id);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpGet]
        public IActionResult UpdateCliente(int clt_Id)
        {
            var clit = this._data.GetClient(clt_Id);
            if (clit == null)
                return NotFound();
            var model = new UpdateClienteViewModel();
            model.clt_Id = clt_Id;
            model.clt_RagioneSociale = clit.clt_RagioneSociale;
            model.clt_Indirizzo= clit.clt_Indirizzo;
            model.clt_Telefono= clit.clt_Telefono;
            model.clt_Mobile= clit.clt_Mobile;
            model.clt_Mail= clit.clt_Mail;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin,Operator")]
        [HttpPost]
        public IActionResult UpdateCliente(UpdateClienteViewModel model)
        {
            Client clit = new Client(model.clt_Id,model.clt_RagioneSociale,model.clt_Indirizzo,model.clt_Mail,model.clt_Telefono,model.clt_Mobile);
            this._data.UpdateCliente(clit);
            return RedirectToAction("Index");
        }
    }
}