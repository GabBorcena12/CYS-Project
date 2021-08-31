using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Models
{
    public class UserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public string IsActive { get; set; }
    }
}