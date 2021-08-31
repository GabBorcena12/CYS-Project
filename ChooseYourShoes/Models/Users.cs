using ChooseYourShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Models
{
    public class Users:UserRoles
    { 
        //public string UserId { get; set; }
        public string FirstNm { get; set; }
        public string MiddleNm { get; set; }
        public string LastNm { get; set; }
        public string PasswordHash { get; set; }
        public string VerifyPasswordHash { get; set; }
        public string EmailAddr { get; set; }
        //public string IsActive { get; set; }
        //public string RoleName { get; set; }
    }
}