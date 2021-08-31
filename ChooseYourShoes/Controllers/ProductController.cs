using ChooseYourShoes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChooseYourShoes.Repositories;
using ChooseYourShoes.Models;
using System.Data;

namespace ChooseYourShoes.Controllers
{
    public class ProductController : Controller
    {

        // GET: Product
        [HttpGet]
        public ActionResult Index()
        { 
            ProductRepositories repos = new ProductRepositories();
            var employeeDetails = (Users)Session["USER_SESSION"];
            var results = repos.GetProducts(employeeDetails.UserId);
            return View(results);
        }

        // GET: Product/Edit/5 
        [HttpGet]
        public ActionResult Details(string id)
        {
            var currentuser = (Users)Session["USER_SESSION"];
            ProductRepositories repos = new ProductRepositories();
            ProductFormViewModel model = new ProductFormViewModel
            {
                Products = new Products(),
                listProductCategory = new List<SelectListItem>(),
            };

            DataSet ds = repos.GetProductById(id);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataMapper<Products> mapper1 = new DataMapper<Products>();
                    var Products = mapper1.Map(ds.Tables[0].Rows[0]);

                    var employeeDetails = (Users)Session["USER_SESSION"];
                    if (employeeDetails.RoleName == "Dev-Administrator")
                    {
                        model.Products.Id = id;
                        model.Products.ProductNm = Products.ProductNm;
                        model.Products.ProductDesc = Products.ProductDesc;
                        model.Products.StockAvailable = Products.StockAvailable;
                        model.Products.Category = Products.Category;
                        model.Products.CategoryDesc = Products.CategoryDesc;
                        model.Products.CreateDttm = Products.CreateDttm;
                        model.Products.CreateUser = Products.CreateUser;
                        model.Products.InputDttm = Products.InputDttm;
                        model.Products.InputUser = Products.InputUser;


                        model.listProductCategory = new List<SelectListItem>();
                        model.listProductCategory.Add(new SelectListItem { Text = "Select Category", Value = "" });

                        var listProductCategory = repos.GetCodeHeaderDetails("Product Category", "All");
                        listProductCategory.ForEach(x => model.listProductCategory.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));
                    }
                    else
                    {
                        ViewBag.Status = "0";
                        ViewBag.Message = "You have no right to access this.";
                    }

                }
                else
                {
                    ViewBag.Status = "0";
                    ViewBag.Message = "Product {id} does not exist.";
                }
            }
            else
            {
                ViewBag.Status = "0";
                ViewBag.Message = "Product {id} does not exist.";
            }

            return View(model);
        }
        // GET: Product/Create
        public ActionResult Create()
        {
            ProductFormViewModel viewModel = new ProductFormViewModel();
            InitializeCreate(viewModel);
            return View(viewModel);
        }
        public void InitializeCreate(ProductFormViewModel viewModel) {

            ProductRepositories repos = new ProductRepositories();
            viewModel.listProductCategory = new List<SelectListItem>();
            viewModel.listProductCategory.Add(new SelectListItem { Text = "Select Category", Value = "", Selected = true });

            var listProductCategory = repos.GetCodeHeaderDetails("Product Category", "All");
            listProductCategory.ForEach(x => viewModel.listProductCategory.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

        }
        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel viewModel)
        {

            ProductRepositories repos = new ProductRepositories();
            var employeeDetails = (Users)Session["USER_SESSION"];
            try
            {
                // TODO: Add insert logic here
                switch (viewModel.ActionButton) {
                    case "Create":
                        var transactionResult = repos.SaveProduct(viewModel.Products, viewModel.ActionButton, employeeDetails.UserId);

                        ViewBag.Status = transactionResult.Code;
                        ViewBag.Message = transactionResult.Message;
                        
                        if (transactionResult.Code == "1")
                        {
                            viewModel.ActionButton = string.Empty;  
                        }

                        InitializeCreate(viewModel);
                        break; 
                }
            }
            catch
            {
                return View();
            }

            return View(viewModel);
        }

        // GET: Product/Edit/5 
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var currentuser = (Users)Session["USER_SESSION"];
            ProductRepositories repos = new ProductRepositories();
            ProductFormViewModel model = new ProductFormViewModel
            {
                Products = new Products(),
                listProductCategory = new List<SelectListItem>(),
            };

            DataSet ds = repos.GetProductById(id);
            
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataMapper<Products> mapper1 = new DataMapper<Products>();
                    var Products = mapper1.Map(ds.Tables[0].Rows[0]);

                    var employeeDetails = (Users)Session["USER_SESSION"];
                    if (employeeDetails.RoleName == "Dev-Administrator")
                    {
                        model.Products.ReferenceId = Products.ReferenceId;
                        model.Products.Id = id;
                        model.Products.ProductNm = Products.ProductNm;
                        model.Products.ProductDesc = Products.ProductDesc;
                        model.Products.StockAvailable = Products.StockAvailable;
                        model.Products.Category = Products.Category;
                        model.Products.CreateDttm = Products.CreateDttm;
                        model.Products.CreateUser = Products.CreateUser;
                        model.Products.InputDttm = Products.InputDttm;
                        model.Products.InputUser = Products.InputUser;
                        model.Products.IsActive = Products.IsActive;

                        InitializeEdit(model);
                    }
                    else
                    {
                        ViewBag.Status = "0";
                        ViewBag.Message = "You have no right to access this.";
                    }

                }
                else
                {
                    ViewBag.Status = "0";
                    ViewBag.Message = "Product {id} does not exist.";
                }
            }
            else
            {
                ViewBag.Status = "0";
                ViewBag.Message = "Product {id} does not exist.";
            }
 
            return View(model);
        }
        public void InitializeEdit(ProductFormViewModel model) {

            ProductRepositories repos = new ProductRepositories();
            model.listProductCategory = new List<SelectListItem>();
            model.listProductCategory.Add(new SelectListItem { Text = "Select Category", Value = "" });

            var listProductCategory = repos.GetCodeHeaderDetails("Product Category", "All");
            listProductCategory.ForEach(x => model.listProductCategory.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductFormViewModel viewModel)
        {

            ProductRepositories repos = new ProductRepositories();
            var employeeDetails = (Users)Session["USER_SESSION"];
            try
            {
                // TODO: Add insert logic here
                switch (viewModel.ActionButton)
                {                    
                    case "Update":
                        var updatetransactionResult = repos.SaveProduct(viewModel.Products, viewModel.ActionButton, employeeDetails.UserId);

                        ViewBag.Status = updatetransactionResult.Code;
                        ViewBag.Message = updatetransactionResult.Message;

                        if (updatetransactionResult.Code == "1")
                        {
                            viewModel.ActionButton = string.Empty; 
                        }
                        InitializeEdit(viewModel);
                        break;
                }
            }
            catch
            {
                return View();
            }
            return View(viewModel);
        }

         

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            ProductRepositories repos = new ProductRepositories();
            ProductFormViewModel viewModel = new ProductFormViewModel();
            var employeeDetails = (Users)Session["USER_SESSION"];
            try
            {
                // TODO: Add insert logic here 
                var transactionResult = repos.DeleteProduct(id);

                ViewBag.Status = transactionResult.Code;
                ViewBag.Message = transactionResult.Message;

                if (transactionResult.Code == "1")
                {
                    viewModel.ActionButton = string.Empty;
                    return RedirectToAction("Index");
                }
                       
                 
            }
            catch
            {
                return View();
            }
            return View(viewModel);
        }
    }
}
