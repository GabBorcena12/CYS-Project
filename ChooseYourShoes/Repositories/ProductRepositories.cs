using ChooseYourShoes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Repositories
{
    public class ProductRepositories
    {  
        public DataSet GetProductById(string id)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[CYS].[SelectByIdProducts]";

                SqlParameter[] parameters = new SqlParameter[] {
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
        public List<Products> GetProducts(string userId)
        {
            string query = "[CYS].[SelectProducts]";
            string[] paramName = new string[] { "@pUserId" };
            string[] paramValue = new string[] { userId };

            DataAccessHelper db = new DataAccessHelper();
            return db.GetData<Products>(query, paramName, paramValue, CommandType.StoredProcedure);
        }

        public TransactionResult SaveProduct(Products model, string Action, string userId)
        {
            TransactionResult result = new TransactionResult();
            try
            { 
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@ProductNm", model.ProductNm),
                    new SqlParameter("@ProductDesc", model.ProductDesc),
                    new SqlParameter("@StockAvailable", model.StockAvailable),
                    new SqlParameter("@IsActive", model.IsActive),
                    new SqlParameter("@Category", model.Category),
                    new SqlParameter("@UserId", userId), 
                    new SqlParameter("@Action", Action),
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("CYS.InsertProducts", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            { 
                //Insert error log here
            }

            return result;
        }

        public TransactionResult DeleteProduct(string id)
        {
            TransactionResult result = new TransactionResult();
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Id", id)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[CYS].[DeleteProducts]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                //Insert error log here
            }

            return result;
        }
        public List<CodeDecode> GetCodeHeaderDetails(string type, string location)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {
                string query = "[CYS].[GetCodeHeaderDetails]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Type", type),
                    new SqlParameter("@Location", location)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }


    }
}