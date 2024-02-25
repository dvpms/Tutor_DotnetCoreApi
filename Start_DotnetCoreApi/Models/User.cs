using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApi.Models
{
    public class User
    {
        public int Id {get;set;}
        public string UserName {get;set;}=string.Empty;
        public string PassWord {get;set;}
        public string Email {get;set;}
        public string Name {get;set;}
        public bool IsActive {get;set;}= true;
        public DateTime CreatedDate {get;set;}=DateTime.UtcNow;
        public Role Role {get;set;}   
    }
}