using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace api.Models
{
    [Table("RefreshTokens")]    
    public class RefreshToken
    {
        public int Id {get; set;}
        public string Token {get; set;} = string.Empty;
        public string OwnedBy {get; set;} = string.Empty;
        public DateTime CreatedOn {get; set;} = DateTime.Now;
        
    }
}