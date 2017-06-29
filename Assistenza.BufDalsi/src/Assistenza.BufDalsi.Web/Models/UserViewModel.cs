using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Display(Name ="Role")]
        public string ApplicationRoleId { get; set; }
        public int IdEsterno { get; set; }
        public Client client { get; set; }
        public IEnumerable<Client> ClientList { get; set; }
        public List<SelectListItem> ListaClienti { get; set; }
    }
}
