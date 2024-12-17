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
        public int InsertCodeMasterDetails(CodeMasterEntity objCodeMasterEntity, out string pCode, out string pType)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pCode"] = objCodeMasterEntity.cmCode;
                dict["pType"] = objCodeMasterEntity.cmType;
                dict["pDesc"] = objCodeMasterEntity.cmDesc;
                dict["pValue"] = objCodeMasterEntity.cmValue;
                dict["pCrBy"] = "ADMIN";
                dict["pCrDt"] = DateTime.Now;
                dict["pActiveYN"] = objCodeMasterEntity.cmActiveYN;

                string insertQuery = $"INSERT INTO CODES_MASTER (CM_CODE, CM_TYPE, CM_DESC,CM_VALUE,CM_CR_BY, CM_CR_DT, CM_ACTIVE_YN) " +
                    $"VALUES(:pCode,:pType,:pDesc,:pValue,:pCrBy,:pCrDt,:pActiveYN)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                pCode = objCodeMasterEntity.cmCode;
                pType = objCodeMasterEntity.cmType;
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int IsCodeMasterExist(CodeMasterEntity objCodeMasterEntity)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pCode"] = objCodeMasterEntity.cmCode;
                dict["pType"] = objCodeMasterEntity.cmType;
                string query = $"SELECT COUNT(*) COUNT FROM CODES_MASTER WHERE CM_CODE=:pCode AND CM_TYPE=:pType";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public int UpdateOption(CodeMasterEntity objCodeMasterEntity, out string pCode, out string pType)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pCode"] = objCodeMasterEntity.cmCode;
                dict["pType"] = objCodeMasterEntity.cmType;
                dict["pDesc"] = objCodeMasterEntity.cmDesc;
                dict["pValue"] = objCodeMasterEntity.cmValue;
                dict["pUpBy"] = "ADMIN";
                dict["pUpDt"] = DateTime.Now;
                dict["pActiveYN"] = objCodeMasterEntity.cmActiveYN;

                string sql = "UPDATE CODES_MASTER SET CM_ACTIVE_YN = :pActiveYN, CM_DESC = :pDesc, CM_VALUE = :pValue, CM_UP_BY = :pUpBy, CM_UP_DT = :pUpDt WHERE CM_CODE = :pCode AND CM_TYPE = :pType";

                int rtval = DBConnection.ExecuteQuery(dict, sql);

                pCode = objCodeMasterEntity.cmCode;
                pType = objCodeMasterEntity.cmType;

                return rtval;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeleteOption(CodeMasterEntity objCodeMaster)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pCode"] = objCodeMaster.cmCode;
                dict["pType"] = objCodeMaster.cmType;
                
                string sql = $"DELETE FROM CODES_MASTER  WHERE CM_CODE=:pCode AND CM_TYPE=:pType AND CM_ACTIVE_YN='N'";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                return rtval;
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
                objDict["pCM_TYPE"] = pType;
                string query = "SELECT CM_CODE,CM_DESC FROM CODES_MASTER WHERE CM_TYPE=:pCM_TYPE AND CM_ACTIVE_YN='Y' ORDER BY CM_CODE ASC";
                dt = DBConnection.ExecuteQuerySelect(objDict, query).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FnDropDownManager()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT EMP_NO,EMP_NO || ' - ' || EMP_NAME EMP_NAME FROM PR_EMPLOYEE,PR_EMPLOYEE_HR WHERE EMP_NO=EH_EMP_NO AND EH_DESIGNATION='105' AND EMP_ACTIVE_YN='Y' ORDER BY EMP_NO";
                dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CodeMasterEntity FnGetCodesMaster(string code, string type)
        {
            try
            {
                DataTable dt = new DataTable();
                CodeMasterEntity objCodeMasterEntity = new CodeMasterEntity();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["pCode"] = code;
                objDict["pType"] = type;
                string str1 = "SELECT CM_CODE, CM_TYPE,CM_VALUE,CM_DESC,CM_ACTIVE_YN FROM CODES_MASTER WHERE CM_CODE=:pCode AND CM_TYPE=:pType";
                dt = DBConnection.ExecuteQuerySelect(objDict, str1).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    objCodeMasterEntity.cmCode = row["CM_CODE"].ToString();
                    objCodeMasterEntity.cmType = row["CM_TYPE"].ToString();
                    objCodeMasterEntity.cmValue = row["CM_VALUE"] == null || string.IsNullOrEmpty(row["CM_VALUE"].ToString()) ? 0 : Convert.ToInt32(row["CM_VALUE"].ToString());
                    objCodeMasterEntity.cmDesc = row["CM_DESC"].ToString();
                    objCodeMasterEntity.cmActiveYN= row["CM_ACTIVE_YN"].ToString();
                }
                return objCodeMasterEntity;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the code master data.", ex);
            }
        }
    }
}
