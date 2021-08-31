using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Models
{
    public class Products
    {  
        [Required]
        [Display(Name = "Product Name")]
        public string ProductNm { get; set; } 
        [Display(Name = "Product Description")]
        public string ProductDesc { get; set; }
        [Required]
        [Display(Name = "Available Stocks")]
        public int StockAvailable { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        public string Category { get; set; }
        public string CategoryDesc { get; set; }
        [Display(Name = "Active")]
        public string IsActive { get; set; }
        public string CreateDttm { get; set; }
        public string CreateUser { get; set; }
        public string InputUser { get; set; }
        public string InputDttm { get; set; }
        [Display(Name = "Product Id")]
        public string Id { get; set; }
        [Display(Name = "Product Id")]
        public string ReferenceId { get; set; }
    }
}