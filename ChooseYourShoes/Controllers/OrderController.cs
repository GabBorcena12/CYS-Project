using ChooseYourShoes.Models;
using ChooseYourShoes.Repositories;
using ChooseYourShoes.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChooseYourShoes.Controllers
{
    public class OrderController : Controller
    {
        // GET: Transaction 
        [HttpGet]
        public ActionResult Index()
        {
            OrderRepositories repos = new OrderRepositories();
            var employeeDetails = (Users)Session["USER_SESSION"];
            var results = repos.GetOrders(employeeDetails.UserId);
            return View(results);
        }

        [HttpGet]
        public ActionResult Released()
        {
            OrderRepositories repos = new OrderRepositories();
            var employeeDetails = (Users)Session["USER_SESSION"];
            var results = repos.GetReleasedOrders(employeeDetails.UserId);
            return View(results);
        }

        public ActionResult RequestForm(string referenceid)
        {

            var employeeDetails = (Users)Session["USER_SESSION"];
            OrderRepositories repos = new OrderRepositories();
            OrderFormViewModel model = new OrderFormViewModel
            {
                Orders = new Orders(),
                listOrderDetails = new List<OrderDetails>(),
            };

            model.Orders.RequestorId = employeeDetails.UserId;
            model.Orders.RequestorNm = employeeDetails.FirstNm +" "+ employeeDetails.LastNm;
            InitializeRequestForm(model);
            return View(model);
        }
        public void InitializeRequestForm(OrderFormViewModel viewModel)
        {
            ProductRepositories repos = new ProductRepositories(); 

            //Location Dropdown
            viewModel.listLocation = new List<SelectListItem>();
            viewModel.listLocation.Add(new SelectListItem { Text = "Select Location", Value = "" });

            var listLocation = repos.GetCodeHeaderDetails("Location", "All");
            listLocation.ForEach(x => viewModel.listLocation.Add(new SelectListItem { Text = x.DecodeTxt +" ("+ x.CodeTxt +")", Value = x.CodeTxt }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestForm(OrderFormViewModel viewModel)
        {

            OrderRepositories repos = new OrderRepositories();
            if (ModelState.IsValid)
            {
                var employeeDetails = (Users)Session["USER_SESSION"];
                try
                {
                    // TODO: Add insert logic here
                    switch (viewModel.ActionButton)
                    {
                        case "Create":
                            var updatetransactionResult = repos.SaveOrder(viewModel.Orders, viewModel.listOrderDetails, viewModel.ActionButton, employeeDetails.UserId);

                            ViewBag.Status = updatetransactionResult.Code;
                            ViewBag.Message = updatetransactionResult.Message;

                            if (updatetransactionResult.Code == "1")
                            {
                                viewModel.ActionButton = string.Empty;
                            }
                            InitializeRequestForm(viewModel);
                            break;
                    }
                }
                catch
                {
                    return View();
                }
            }
            return View(viewModel);
        }
        public ActionResult Details(string referenceid)
        {
            OrderRepositories repos = new OrderRepositories();
            OrderFormViewModel model = new OrderFormViewModel
            {
                Orders = new Orders(),
                modalOrdersDetails = new OrderDetails(),
                listOrderDetails = new List<OrderDetails>(),
                listLocation = new List<SelectListItem>()
            };
            var employeeDetails = (Users)Session["USER_SESSION"];
            DataSet ds = repos.GetOrderById(employeeDetails.UserId, referenceid);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataMapper<Orders> mapper1 = new DataMapper<Orders>();
                    model.Orders = mapper1.Map(ds.Tables[0].Rows[0]);

                    DataMapper<OrderDetails> mapper2 = new DataMapper<OrderDetails>();
                    model.listOrderDetails = mapper2.Map(ds.Tables[1]).ToList();

                    if (employeeDetails.RoleName == "Dev-Administrator")
                    {
                        model.Orders.ReferenceId = model.Orders.ReferenceId;
                        model.Orders.RequestorId = model.Orders.RequestorId;
                        model.Orders.RequestorNm = model.Orders.RequestorNm;
                        model.Orders.ActivityNm = model.Orders.ActivityNm;
                        model.Orders.Location = model.Orders.Location;
                        model.Orders.LocationDesc = model.Orders.LocationDesc;
                        model.Orders.CreateDttm = model.Orders.CreateDttm;
                        model.Orders.CreateDttm = Convert.ToDateTime(model.Orders.CreateDttm).ToShortDateString();
                        model.Orders.CreateUser = model.Orders.CreateUser;
                        model.Orders.InputDttm = Convert.ToDateTime(model.Orders.InputDttm).ToShortDateString();
                        model.Orders.InputUser = model.Orders.InputUser;
                        model.Orders.IsActive = model.Orders.IsActive;

                        InitializeEditForm(model);
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
                    ViewBag.Message = "Product {"+ referenceid +"} does not exist.";
                }
            }
            else
            {
                ViewBag.Status = "0";
                ViewBag.Message = "Product {"+ referenceid +"} does not exist.";
            }

            return View(model);
        }
        public ActionResult EditForm(string referenceid)
        {
            OrderRepositories repos = new OrderRepositories();
            OrderFormViewModel model = new OrderFormViewModel
            {
                Orders = new Orders(),
                modalOrdersDetails = new OrderDetails(),
                listOrderDetails = new List<OrderDetails>(),
                listLocation = new List<SelectListItem >()
            };
            var employeeDetails = (Users)Session["USER_SESSION"];
            DataSet ds = repos.GetOrderById(employeeDetails.UserId, referenceid);

            if (ds.Tables.Count > 0) {
                if (ds.Tables[0].Rows.Count > 0) {

                    DataMapper<Orders> mapper1 = new DataMapper<Orders>();
                    model.Orders = mapper1.Map(ds.Tables[0].Rows[0]);
                     
                    DataMapper<OrderDetails> mapper2 = new DataMapper<OrderDetails>();
                    model.listOrderDetails = mapper2.Map(ds.Tables[1]).ToList();
                     
                    if (employeeDetails.RoleName == "Dev-Administrator")
                    {
                        model.Orders.ReferenceId = model.Orders.ReferenceId; 
                        model.Orders.RequestorId = model.Orders.RequestorId;
                        model.Orders.RequestorNm = model.Orders.RequestorNm;
                        model.Orders.ActivityNm = model.Orders.ActivityNm;
                        model.Orders.Location = model.Orders.Location;
                        model.Orders.LocationDesc = model.Orders.LocationDesc;
                        model.Orders.CreateDttm = model.Orders.CreateDttm;
                        model.Orders.CreateDttm = Convert.ToDateTime(model.Orders.CreateDttm).ToShortDateString();
                        model.Orders.CreateUser = model.Orders.CreateUser;
                        model.Orders.InputDttm = Convert.ToDateTime(model.Orders.InputDttm).ToShortDateString();
                        model.Orders.InputUser = model.Orders.InputUser;
                        model.Orders.IsActive = model.Orders.IsActive;

                        InitializeEditForm(model);
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
                    ViewBag.Message = "Product {"+ referenceid +"} does not exist.";
                }
            }
            else
            {
                ViewBag.Status = "0";
                ViewBag.Message = "Product {"+ referenceid +"} does not exist.";
            }

            return View(model);
        }
        public void InitializeEditForm(OrderFormViewModel viewModel) {
            ProductRepositories repos = new ProductRepositories(); 

            viewModel.listLocation = new List<SelectListItem>();
            viewModel.listLocation.Add(new SelectListItem { Text = "Select Location", Value = "" });

            var listLocation = repos.GetCodeHeaderDetails("Location", "All");
            listLocation.ForEach(x => viewModel.listLocation.Add(new SelectListItem { Text = x.DecodeTxt + " (" + x.CodeTxt + ")", Value = x.CodeTxt }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForm(OrderFormViewModel viewModel)
        {

            OrderRepositories repos = new OrderRepositories();
            var employeeDetails = (Users)Session["USER_SESSION"];
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    switch (viewModel.ActionButton)
                    {
                        case "Update":
                            var updatetransactionResult = repos.SaveOrder(viewModel.Orders, viewModel.listOrderDetails, viewModel.ActionButton, employeeDetails.UserId);

                            ViewBag.Status = updatetransactionResult.Code;
                            ViewBag.Message = updatetransactionResult.Message;

                            if (updatetransactionResult.Code == "1")
                            {
                                viewModel.ActionButton = string.Empty;
                            }
                            break;
                    }
                }
                catch
                {
                    return View();
                }
            }

            InitializeEditForm(viewModel);
            return View(viewModel);
        } 

        [HttpPost]
        public ActionResult GetProductList(string Prefix)
        {
            List<CodeDecode> pq = new List<CodeDecode>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection sqlconn = new SqlConnection(constr))
            {
                SqlCommand sqlcomm = new SqlCommand("[ORDER].[GetProductList]", sqlconn);
                sqlcomm.Parameters.Add("@Prefix", SqlDbType.VarChar);
                sqlcomm.Parameters["@Prefix"].Value = Prefix;

                sqlconn.Open();
                sqlcomm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sdr = sqlcomm.ExecuteReader();

                while (sdr.Read())
                {
                    CodeDecode ptq = new CodeDecode();
                    ptq.CodeTxt = sdr["CodeTxt"].ToString();
                    ptq.DecodeTxt = sdr["DecodeTxt"].ToString();
                    pq.Add(ptq);
                }

                return Json(pq, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult GetProductDetails(string Prefix)
        {
            OrderRepositories repos = new OrderRepositories();
            OrderFormViewModel viewmodel = new OrderFormViewModel();

            var result = repos.GetProductDetails(Prefix); 
            return Json(result, JsonRequestBehavior.AllowGet);  

        }

        [HttpPost]
        public ActionResult CheckStockAvailable(string param1,string param2)
        {
            OrderRepositories repos = new OrderRepositories();
            OrderFormViewModel viewmodel = new OrderFormViewModel();

            var result = repos.CheckStockAvailable(param1,param2); 
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult ReleaseOrder(string param1,string param2)
        {
            OrderRepositories repos = new OrderRepositories();
            OrderFormViewModel viewmodel = new OrderFormViewModel();
            var employeeDetails = (Users)Session["USER_SESSION"];

            var result = repos.ReleaseOrder(param1, param2, employeeDetails.UserId); 
            return Json(result, JsonRequestBehavior.AllowGet);

        }
         
        public ActionResult ReleaseForm(string referenceid)
        {
            OrderRepositories repos = new OrderRepositories();
            OrderFormViewModel model = new OrderFormViewModel
            {
                Orders = new Orders(),
                modalOrdersDetails = new OrderDetails(),
                listOrderDetails = new List<OrderDetails>(),
                listLocation = new List<SelectListItem>()
            };
            var employeeDetails = (Users)Session["USER_SESSION"];
            DataSet ds = repos.GetOrderById(employeeDetails.UserId, referenceid);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataMapper<Orders> mapper1 = new DataMapper<Orders>();
                    model.Orders = mapper1.Map(ds.Tables[0].Rows[0]);

                    DataMapper<OrderDetails> mapper2 = new DataMapper<OrderDetails>();
                    model.listOrderDetails = mapper2.Map(ds.Tables[1]).ToList();

                    if (employeeDetails.RoleName == "Dev-Administrator")
                    {
                        model.Orders.ReferenceId = model.Orders.ReferenceId;
                        model.Orders.RequestorId = model.Orders.RequestorId;
                        model.Orders.RequestorNm = model.Orders.RequestorNm;
                        model.Orders.ActivityNm = model.Orders.ActivityNm;
                        model.Orders.Location = model.Orders.Location;
                        model.Orders.LocationDesc = model.Orders.LocationDesc;
                        model.Orders.CreateDttm = model.Orders.CreateDttm;
                        model.Orders.CreateDttm = Convert.ToDateTime(model.Orders.CreateDttm).ToShortDateString();
                        model.Orders.CreateUser = model.Orders.CreateUser;
                        model.Orders.InputDttm = Convert.ToDateTime(model.Orders.InputDttm).ToShortDateString();
                        model.Orders.InputUser = model.Orders.InputUser;
                        model.Orders.IsActive = model.Orders.IsActive;

                        InitializeReleaseForm(model);
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
                    ViewBag.Message = "Product {"+ referenceid +"} does not exist.";
                }
            }
            else
            {
                ViewBag.Status = "0";
                ViewBag.Message = "Product {" + referenceid + "} does not exist.";
            }

            return View(model);
        }
        public void InitializeReleaseForm(OrderFormViewModel viewModel)
        {
            ProductRepositories repos = new ProductRepositories();

            viewModel.listLocation = new List<SelectListItem>();
            viewModel.listLocation.Add(new SelectListItem { Text = "Select Location", Value = "" });

            var listLocation = repos.GetCodeHeaderDetails("Location", "All");
            listLocation.ForEach(x => viewModel.listLocation.Add(new SelectListItem { Text = x.DecodeTxt + " (" + x.CodeTxt + ")", Value = x.CodeTxt }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReleaseForm(OrderFormViewModel viewModel)
        {

            OrderRepositories repos = new OrderRepositories();
            var employeeDetails = (Users)Session["USER_SESSION"];
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    switch (viewModel.ActionButton)
                    {
                        case "Update":
                            var updatetransactionResult = repos.SaveOrder(viewModel.Orders, viewModel.listOrderDetails, viewModel.ActionButton, employeeDetails.UserId);

                            ViewBag.Status = updatetransactionResult.Code;
                            ViewBag.Message = updatetransactionResult.Message;

                            if (updatetransactionResult.Code == "1")
                            {
                                viewModel.ActionButton = string.Empty;
                            }
                            break;
                    }
                }
                catch
                {
                    return View();
                }
            }

            InitializeEditForm(viewModel);
            return View(viewModel);
        }
    }
}