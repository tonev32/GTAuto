using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }

       // public Order Orders { get; set; }
    }
}
