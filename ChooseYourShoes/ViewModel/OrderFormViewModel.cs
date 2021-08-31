using ChooseYourShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChooseYourShoes.ViewModel
{
    public class OrderFormViewModel
    {
        public Orders Orders { get; set; }
        public OrderDetails modalOrdersDetails { get; set; }
        public List<OrderDetails> listOrderDetails { get; set; }
        public List<SelectListItem> listProductCategory { get; set; }
        public List<SelectListItem> listLocation { get; set; }
        public List<SelectListItem> listProducts { get; set; }
        
        public string ActionButton { get; set; }
        public string ValidateStatus { get; set; }
        public string ValidateStatusMsg { get; set; }
    }
}