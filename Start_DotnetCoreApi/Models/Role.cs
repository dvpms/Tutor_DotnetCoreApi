using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApi.Models
{
    
    public class Role
    {
        [Key]
        public int Id {get;set;}
        public string RoleName {get;set;}=string.Empty;

        public IEnumerable<User> Users {get;set;}
    }
}