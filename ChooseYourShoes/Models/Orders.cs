using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Models
{
    public class Orders
    {
        [Display(Name = "Order No")]
        public string OrderId { get; set; }
        public string Id { get; set; } 
        [Display(Name = "Reference No")]
        public string ReferenceId { get; set; }
        [Display(Name = "Requestor Id")]
        public string RequestorId { get; set; }
        [Display(Name = "Requestor Name")]
        public string RequestorNm { get; set; }
        [Display(Name = "Activity Name")]
        public string ActivityNm { get; set; }
        [Display(Name = "Country")]
        public string Location { get; set; }
        [Display(Name = "Country")]
        public string LocationDesc { get; set; }
        [Display(Name = "Active?")]
        public string IsActive { get; set; }
        public string CreateDttm { get; set; }
        public string CreateUser { get; set; }
        public string InputDttm { get; set; }
        public string InputUser { get; set; }
    }
}