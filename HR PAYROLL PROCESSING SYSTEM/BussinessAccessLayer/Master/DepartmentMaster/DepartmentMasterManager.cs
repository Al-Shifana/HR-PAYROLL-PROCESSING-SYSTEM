using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Master.DepartmentMaster
{
    public class DepartmentMasterManager
    {
        DepartmentMasterEntity objDepartmentMasterEntity = new DepartmentMasterEntity();
        public DataTable LoadGridDetails()
        {
            try
            {
                string str1 = $"SELECT DEPT_NO, DEPT_NAME FROM DEPARTMENT_MASTER ORDER BY DEPT_NO ASC";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertDepartmentMasterDetails(DepartmentMasterEntity objDepartmentMasterEntity, out string pCode)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pdeptNo"] = objDepartmentMasterEntity.deptNo;
                dict["pdeptName"] = objDepartmentMasterEntity.deptName;
                dict["pdeptCrBy"] = "ADMIN";
                dict["pdeptCrDt"] = DateTime.Now;

                string insertQuery = $"INSERT INTO DEPARTMENT_MASTER(DEPT_NO,DEPT_NAME,DEPT_CR_BY,DEPT_CR_DT) VALUES(:pdeptNo,:pdeptName,:pdeptCrBy,:pdeptCrDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                pCode = objDepartmentMasterEntity.deptNo;
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int IsDepartmentMasterExist(DepartmentMasterEntity objDeptMasterEntity)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pNo"] = objDeptMasterEntity.deptNo;
                string query = $"SELECT COUNT(*) COUNT FROM DEPARTMENT_MASTER WHERE DEPT_NO=:pNo";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int UpdateOption(DepartmentMasterEntity objDepartmentMasterEntity, out string pCode)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["deptNo"] = objDepartmentMasterEntity.deptNo;
                dict["deptName"] = objDepartmentMasterEntity.deptName;
                dict["deptUpBy"] = "ADMIN";
                dict["deptUpDt"] = DateTime.Now;
                string sql = "UPDATE DEPARTMENT_MASTER SET DEPT_NAME=:deptName, DEPT_UP_BY=:deptUpBy, DEPT_UP_DT=:deptUpDt WHERE DEPT_NO = :deptNo";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                pCode = objDepartmentMasterEntity.deptNo;
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int DeleteOption(DepartmentMasterEntity objDepartmentMasterEntity)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pNo"] = objDepartmentMasterEntity.deptNo;
                string sql = $"DELETE FROM DEPARTMENT_MASTER  WHERE DEPT_NO=:pNo";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable DeptDropDowns()
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["DEPT_NO"] = objDepartmentMasterEntity.deptNo;
                string query = "SELECT DEPT_NAME,DEPT_NO FROM DEPARTMENT_MASTER ORDER BY :DEPT_NO";
                dt = DBConnection.ExecuteQuerySelect(objDict, query).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DepartmentMasterEntity FnGetCodesMaster(string code)
        {
            try
            {
                DataTable dt = new DataTable();
                DepartmentMasterEntity objDepMasterEntity = new DepartmentMasterEntity();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["pCode"] = code;
                string str1 = "SELECT DEPT_NO,DEPT_NAME FROM DEPARTMENT_MASTER WHERE DEPT_NO=:pCode";
                dt = DBConnection.ExecuteQuerySelect(objDict, str1).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    objDepMasterEntity.deptNo= row["DEPT_NO"].ToString();
                    objDepMasterEntity.deptName = row["DEPT_NAME"].ToString();
                }
                return objDepMasterEntity;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the Department master data.", ex);
            }
        }
    }
}
