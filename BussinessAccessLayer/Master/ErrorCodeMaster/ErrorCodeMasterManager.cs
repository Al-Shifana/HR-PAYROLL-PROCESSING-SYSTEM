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

        public int InsertErrorCodeMasterDetails(ErrorCodeMasterEntity objErrCodeMaster,out string pCode)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["errCode"] = objErrCodeMaster.errCode;
                dict["errType"] = objErrCodeMaster.errType;
                dict["errDesc"] = objErrCodeMaster.errDesc;
                dict["errCrBy"] = "ADMIN";
                dict["errCrDt"] = DateTime.Now;

                string insertQuery = $"INSERT INTO ERROR_CODE_MASTER (ERR_CODE,ERR_TYPE,ERR_DESC,ERR_CR_BY,ERR_CR_DT) VALUES(:errCode,:errType,:errDesc,:errCrBy,:errCrDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                pCode = objErrCodeMaster.errCode;
                return rtval;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int IsErrorCodeMasterExist(ErrorCodeMasterEntity objErrCodeMasterEntity)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pCode"] = objErrCodeMasterEntity.errCode;
                string query = $"SELECT COUNT(*) COUNT FROM ERROR_CODE_MASTER WHERE ERR_CODE=:pCode";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteOption(string errCode)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pCode"] = errCode;
                string sql = $"DELETE FROM ERROR_CODE_MASTER  WHERE ERR_CODE=:pCode";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ErrorCodeMasterEntity FnGetCodesMaster(string code)
        {
            try
            {
                DataTable dt = new DataTable();
                ErrorCodeMasterEntity objErrCodeMasterEntity = new ErrorCodeMasterEntity();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["pCode"] = code;
                string str1 = "SELECT ERR_CODE, ERR_TYPE,ERR_DESC FROM ERROR_CODE_MASTER WHERE ERR_CODE=:pCode";
                dt = DBConnection.ExecuteQuerySelect(objDict, str1).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    objErrCodeMasterEntity.errCode = row["ERR_CODE"].ToString();
                    objErrCodeMasterEntity.errType = row["ERR_TYPE"].ToString();
                    objErrCodeMasterEntity.errDesc = row["ERR_DESC"].ToString();
                }
                return objErrCodeMasterEntity;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the error code master data.", ex);
            }
        }
        public int UpdateOption(ErrorCodeMasterEntity objErrCodeMasterEntity, out string pCode)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pCode"] = objErrCodeMasterEntity.errCode;
                dict["pType"] = objErrCodeMasterEntity.errType;
                dict["pDesc"] = objErrCodeMasterEntity.errDesc;
                dict["pUpBy"] = "ADMIN";
                dict["pUpDt"] = DateTime.Now;
                string sql = $"UPDATE ERROR_CODE_MASTER SET ERR_TYPE=:pType,ERR_DESC=:pDesc,ERR_UP_BY=:pUpBy,ERR_UP_DT=:pUpDt WHERE ERR_CODE =:pCode";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                pCode = objErrCodeMasterEntity.errCode;
                return rtval;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string FnFetchError(string pErrCode)
        {
            try
            {
                string str = $"SELECT ERR_DESC FROM ERROR_CODE_MASTER WHERE ERR_CODE='{pErrCode}'";
                object rt = DBConnection.ExecuteScalar(str);
                string st = Convert.ToString(rt);
                return st;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
    
}
