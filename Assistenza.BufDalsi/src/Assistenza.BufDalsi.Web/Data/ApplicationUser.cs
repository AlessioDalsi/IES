using Assistenza.BufDalsi.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string IdEsterno { get; internal set; }
        public string Name { get; set; }
        public string RoleId { get; set; }
    }
}
