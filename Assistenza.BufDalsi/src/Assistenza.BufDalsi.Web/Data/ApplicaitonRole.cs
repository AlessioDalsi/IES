using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Web.Data
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public string IPAddress { get; set; }
    }
}
