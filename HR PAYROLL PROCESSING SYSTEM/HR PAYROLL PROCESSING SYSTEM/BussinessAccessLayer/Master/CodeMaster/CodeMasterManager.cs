using BussinessAccessLayer.Master.CodeMaster;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Master.CodeMaster
{
    public class CodeMasterManager
    {
        public DataTable LoadGridDetails()
        {
            try
            {
            string str1 = $"SELECT CM_CODE, CM_TYPE,CM_DESC,CM_VALUE,CM_ACTIVE_YN FROM CODES_MASTER ";
            DataTable dt = DBConnection.ExecuteDataset(str1);
            return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        public int InsertCodeMasterDetails(CodeMasterEntity objCodeMasterEntity)
        {
          try
          {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["cmCode"] = objCodeMasterEntity.cmCode;
            dict["cmType"] = objCodeMasterEntity.cmType;
            dict["cmDesc"] = objCodeMasterEntity.cmDesc;
            dict["cmValue"] = objCodeMasterEntity.cmValue;
            dict["cmCrBy"] = "ADMIN";
            dict["cmCrDt"] = DateTime.Now;
            dict["cmUpBy"] = "ADMIN";
            dict["cmUpDt"] = DateTime.Now;
            dict["cmActiveYN"] = objCodeMasterEntity.cmActiveYN.Substring(0,1);

            string insertQuery = $"INSERT INTO CODES_MASTER VALUES(:cmCode,:cmType,:cmDesc,:cmValue,:cmCrBy,:cmCrDt,:cmUpBy,:cmUpDt,:cmActiveYN)";
            int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
            return rtval;
          }
          catch (Exception ex)
            {
                throw ex;
            }

        }
        public int IsCodeMasterExist(string cmCode,string cmType)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM CODES_MASTER WHERE CM_CODE= '{cmCode}' AND CM_TYPE= '{cmType}'";
                int result = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
        public string UpdateOption(string cmCode, string cmType, string cmDesc, string cmValue, string cmActiveYN)
        {
            try
            {
                string sql = $"UPDATE CODES_MASTER SET CM_ACTIVE_YN = '{cmActiveYN}',CM_DESC='{cmDesc}',CM_VALUE='{cmValue}' WHERE CM_CODE = '{cmCode}' AND CM_TYPE='{cmType}'";
                return sql;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public string DeleteOption(string cmCode, string cmType)
        {
            try
            {
                string sql = $"DELETE FROM CODES_MASTER  WHERE CM_CODE= '{cmCode}' AND CM_TYPE= '{cmType}'";
                return sql;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public DataTable FnDropDowns(string pType)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["CM_TYPE"] = pType;
                string query = "SELECT CM_CODE,CM_DESC FROM CODES_MASTER WHERE CM_TYPE=:CM_TYPE AND CM_ACTIVE_YN='Y' ORDER BY CM_CODE";
                dt = DBConnection.ExecuteQuerySelect(objDict, query).Tables[0];
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        
    }
}
