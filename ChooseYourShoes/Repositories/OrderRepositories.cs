using ChooseYourShoes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Repositories
{
    public class OrderRepositories
    {
        public List<Orders> GetOrders(string userId)
        {
            string query = "[ORDER].[SelectOrders]";
            string[] paramName = new string[] { "@pUserId" };
            string[] paramValue = new string[] { userId };

            DataAccessHelper db = new DataAccessHelper();
            return db.GetData<Orders>(query, paramName, paramValue, CommandType.StoredProcedure);
        }
        public List<Orders> GetReleasedOrders(string userId)
        {
            string query = "[ORDER].[SelectReleasedOrders]";
            string[] paramName = new string[] { "@pUserId" };
            string[] paramValue = new string[] { userId };

            DataAccessHelper db = new DataAccessHelper();
            return db.GetData<Orders>(query, paramName, paramValue, CommandType.StoredProcedure);
        }

        
        public DataSet GetOrderById(string userId, string id)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[ORDER].[SelectByIdOrders]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@pUserId", userId),
                    new SqlParameter("@Id", id)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public TransactionResult SaveOrder(Orders order, List<OrderDetails> orderDetails, string Action, string userId)
        {
            TransactionResult result = new TransactionResult();
            DataTable tblOrderDetails = BuildOrderDetails(orderDetails);
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Id", order.Id),
                    new SqlParameter("@ReferenceId", order.ReferenceId),
                    new SqlParameter("@RequestorId", order.RequestorId),
                    new SqlParameter("@RequestorNm", order.RequestorNm),
                    new SqlParameter("@ActivityNm", order.ActivityNm),
                    new SqlParameter("@Location", order.Location),
                    new SqlParameter("@IsActive", order.IsActive),
                    new SqlParameter("@CreateDttm", order.CreateDttm),
                    new SqlParameter("@CreateUser", order.CreateUser),
                    new SqlParameter("@InputDttm", order.InputDttm),
                    new SqlParameter("@InputUser", order.InputUser),
                    new SqlParameter("@pUserId", userId),
                    new SqlParameter("@Action", Action),
                    new SqlParameter("@OrderDetails", tblOrderDetails),
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("ORDER.InsertOrders", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            { 
                // Log exception here
                UserSettings logRepo = new UserSettings();
                string auditId = logRepo.LogError(order, orderDetails, "", userId, "Order Request Form", ex.StackTrace, ex.GetBaseException().Message);
                result.Code = "0";
                result.Message = "An error has encountered during the process. Kindly file a ticket in AskICT with this reference number: " + auditId;
            }

            return result;
        }

        private DataTable BuildOrderDetails(List<OrderDetails> list)
        {
            if (list == null)
            {
                list = new List<OrderDetails>();
            }

            DataTable dtResult = new DataTable();
            DataRow dr;

            dtResult.Columns.Add("Id");
            dtResult.Columns.Add("ReferenceId");
            dtResult.Columns.Add("ProductId");
            dtResult.Columns.Add("ProductNm");
            dtResult.Columns.Add("ProductDesc");
            dtResult.Columns.Add("ProductCategory");
            dtResult.Columns.Add("Qty");
            dtResult.Columns.Add("CreateDttm");
            dtResult.Columns.Add("CreateUser");
            dtResult.Columns.Add("InputDttm");
            dtResult.Columns.Add("InputUser");
            int itemId = 0;
            foreach (var item in list)
            {
                dr = dtResult.NewRow();
                dr["Id"] = item.Id;
                dr["ReferenceId"] = item.ReferenceId;
                dr["ProductId"] = item.ProductId;
                dr["ProductNm"] = item.ProductNm;
                dr["ProductDesc"] = item.ProductDesc;
                dr["ProductCategory"] = item.ProductCategory;
                dr["Qty"] = item.Qty;
                dr["CreateDttm"] = item.CreateDttm;
                dr["CreateUser"] = item.CreateUser;
                dr["InputDttm"] = item.InputDttm;
                dr["InputUser"] = item.InputUser;
                dtResult.Rows.Add(dr);
            }

            return dtResult;
        }
        public OrderDetails GetProductDetails(string prefix)
        {
            OrderDetails result = new OrderDetails();

            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Prefix", prefix)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<OrderDetails>("[ORDER].[GetProductDetails]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                // Log exception here

            }

            return result;
        }

        public CodeDecode CheckStockAvailable(string param1,string param2)
        {
            CodeDecode result = new CodeDecode();

            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@ReferenceId", param1),
                    new SqlParameter("@Qty", param2)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<CodeDecode>("[ORDER].[CheckStockAvailable]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                // Log exception here
                UserSettings logRepo = new UserSettings();
                string auditId = logRepo.LogError(param1, param2, "", "", "Stocks Checking", ex.StackTrace, ex.GetBaseException().Message);
                result.Code = "0";
                result.Message = "An error has encountered during the process. Kindly file a ticket in AskICT with this reference number: " + auditId;

            }

            return result;
        }

        public CodeDecode ReleaseOrder(string orderno,string action,string userid)
        {
            CodeDecode result = new CodeDecode();

            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@OrderNo", orderno) ,
                    new SqlParameter("@Action", action),
                    new SqlParameter("@UserId", userid)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<CodeDecode>("[ORDER].[UpdateOrderRequest]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                // Log exception here
                UserSettings logRepo = new UserSettings();
                string auditId = logRepo.LogError(orderno, action, "", userid, "Releasing", ex.StackTrace, ex.GetBaseException().Message);
                result.Code = "0";
                result.Message = "An error has encountered during the process. Kindly file a ticket in AskICT with this reference number: " + auditId;

            }

            return result;
        } 
    }
    
}