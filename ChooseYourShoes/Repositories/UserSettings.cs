using ChooseYourShoes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ChooseYourShoes.Repositories
{
    public class UserSettings
    {
        public Users GetEmployeeDetails(string userId)
        {
            string query = "[CYS].[GetUserInformation]";
            string[] paramValue = new string[] { "@pUserId" };
            string[] paramName = new string[] { userId };

            DataAccessHelper db = new DataAccessHelper();
            return db.GetData<Users>(query, paramValue, paramName, CommandType.StoredProcedure)[0];
        }
        public string LogError(object model, object model2, object model3, string userId, string transactionType, string stackTrace, string exceptionMessage)
        {
            string auditId = string.Empty;

            try
            {
                string query = "dbo.InsertException";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("RequestParameter", ConvertToXMLString(model)),
                    new SqlParameter("RequestParameter2", ConvertToXMLString(model2)),
                    new SqlParameter("RequestParameter3", ConvertToXMLString(model3)),
                    new SqlParameter("UserId", userId),
                    new SqlParameter("TransactionType", transactionType),
                    new SqlParameter("StackTrace", stackTrace),
                    new SqlParameter("ExceptionMessage", exceptionMessage),
                };

                DataAccessHelper db = new DataAccessHelper();
                var dtResult = db.GetData(query, parameters, CommandType.StoredProcedure).Tables[0];

                auditId = dtResult.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {

            }

            return auditId;
        }

        private string ConvertToXMLString(object model)
        {
            if (model == null)
            {
                return string.Empty;
            }
            else
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(model.GetType());
                serializer.Serialize(stringwriter, model);
                return stringwriter.ToString();
            }
        }

    }
}