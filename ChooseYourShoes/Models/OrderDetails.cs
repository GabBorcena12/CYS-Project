using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Models
{
    public class OrderDetails
    {
        public string Id { get; set; }
        public string ReferenceId { get; set; }
        public string ProductId { get; set; }
        public string ProductNm { get; set; }
        public string ProductDesc { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCategoryDesc { get; set; }
        public string Qty { get; set; } 
        public string CreateDttm { get; set; }
        public string CreateUser { get; set; }
        public string InputDttm { get; set; }
        public string InputUser { get; set; }
    }
}