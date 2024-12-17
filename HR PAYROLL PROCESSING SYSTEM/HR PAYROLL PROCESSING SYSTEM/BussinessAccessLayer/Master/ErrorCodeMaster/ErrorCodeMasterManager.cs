using DataAccessLayer;
using System;
using BussinessAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BussinessAccessLayer.Master.ErrorCodeMaster
{
    public class ErrorCodeMasterManager
    {
        public DataTable LoadGridDetails()
        {
            try
            {
                string str1 = $"SELECT ERR_CODE,ERR_TYPE,ERR_DESC FROM ERROR_CODE_MASTER";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertErrorCodeMasterDetails(ErrorCodeMasterEntity objerrorcodemasterentity)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["errCode"] = objerrorcodemasterentity.errCode;
                dict["errType"] = objerrorcodemasterentity.errType;
                dict["errDesc"] = objerrorcodemasterentity.errDesc;
                dict["errCrBy"] = "ADMIN";
                dict["errCrDt"] = DateTime.Now;
                dict["errUpBy"] = "ADMIN";
                dict["errUpDt"] = DateTime.Now;

                string insertQuery = $"INSERT INTO ERROR_CODE_MASTER VALUES(:errCode,:errType,:errDesc,:errCrBy,:errCrDt,:errUpBy,:errUpDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                return rtval;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int IsErrorCodeMasterExist(string errCode, string errType)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM ERROR_CODE_MASTER WHERE ERR_CODE= {errCode} AND ERR_TYPE='{errType}'";
                int result = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    
}
