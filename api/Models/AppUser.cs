using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        public string OTC {get; set;} = string.Empty;
        
    }
}