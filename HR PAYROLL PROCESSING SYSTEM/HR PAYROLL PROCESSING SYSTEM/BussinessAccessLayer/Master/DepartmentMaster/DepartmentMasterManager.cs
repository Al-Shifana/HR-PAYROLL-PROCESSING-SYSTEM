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
        public DataTable LoadGridDetails()
        {
            try
            {
                string str1 = $"SELECT DEPT_NO, DEPT_NAME FROM DEPARTMENT_MASTER";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertDepartmentMasterDetails(DepartmentMasterEntity objDepartmentMasterEntity)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["deptNo"] = objDepartmentMasterEntity.deptNo;
                dict["deptName"] = objDepartmentMasterEntity.deptName;
                dict["deptCrBy"] = "ADMIN";
                dict["deptCrDt"] = DateTime.Now;

                string insertQuery = $"INSERT INTO DEPARTMENT_MASTER(DEPT_NO,DEPT_NAME,DEPT_CR_BY,DEPT_CR_DT) VALUES(:deptNo,:deptName,:deptCrBy,:deptCrDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int IsDepartmentMasterExist(string deptNo)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM DEPARTMENT_MASTER WHERE DEPT_NO= '{deptNo}'";
                int result = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                return result;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public string UpdateOption(string deptNo, string deptName)
        {
            try
            {
                string sql = $"UPDATE DEPARTMENT_MASTER SET DEPT_NAME = '{deptName}' WHERE DEPT_NO = '{deptNo}'";
                return sql;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string DeleteOption(string deptNo)
        {
            try
            {
                string sql = $"DELETE FROM DEPARTMENT_MASTER  WHERE DEPT_NO= '{deptNo}'";
                return sql;

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
                //objDict["DEPT_NO"] = dNo;
                string query = "SELECT DEPT_NAME,DEPT_NO FROM DEPARTMENT_MASTER ORDER BY DEPT_NO";
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
