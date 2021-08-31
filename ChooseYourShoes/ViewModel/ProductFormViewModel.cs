using ChooseYourShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChooseYourShoes.ViewModel
{
    public class ProductFormViewModel
    {
        public List<Products> listProducts { get; set; }
        public Products Products { get; set; }
        public string ActionButton { get; set; }

        public List<SelectListItem> listProductCategory { get; set; }
    }
}