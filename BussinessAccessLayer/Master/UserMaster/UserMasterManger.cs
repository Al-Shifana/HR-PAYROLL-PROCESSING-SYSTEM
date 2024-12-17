using BussinessAccessLayer.Transaction.PREmployee;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Master.UserMaster
{
    public class UserMasterManger
    {
        public string Loginfn(string usrname, string pswd)
        {
            try
            {
                string str = $"SELECT USER_TYPE FROM USER_MASTER WHERE USER_ID='{usrname}' AND USER_PASSWORD='{pswd}'";
                object rt = DBConnection.ExecuteScalar(str);
                string st = Convert.ToString(rt);
                return st;
            }
            catch (Exception)
            {
                throw;
            }

        }     
        public string LoginId(string usrname, string pswd)
        {
            try
            {
                string str = $"SELECT USER_ID FROM USER_MASTER WHERE USER_ID='{usrname}' AND USER_PASSWORD='{pswd}'";
                object rt = DBConnection.ExecuteScalar(str);
                string st = Convert.ToString(rt);
                return st;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public int IsUserMasterExist(string userId)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM USER_MASTER WHERE USER_ID= '{userId}'";
                int result = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int InsertUserMasterDetails(UserMasterEntity objUserMasterEntity,out string userId)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["userId"] = objUserMasterEntity.userId;
                dict["userName"] = objUserMasterEntity.userName;
                dict["userPassword"] = objUserMasterEntity.userPassword;
                dict["userType"] = objUserMasterEntity.userType;
                dict["userCrBy"] = "ADMIN";
                dict["userCrDt"] = DateTime.Now;
                dict["userActiveYN"] = objUserMasterEntity.userActiveYN.Substring(0, 1);

                string insertQuery = $"INSERT INTO USER_MASTER (USER_ID,USER_NAME,USER_PASSWORD,USER_TYPE,USER_CR_BY,USER_CR_DT,USER_ACTIVE_YN)" +
                    $" VALUES(:userId,:userName,:userPassword,:userType,:userCrBy,:userCrDt,:userActiveYN)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                userId = objUserMasterEntity.userId;
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable LoadGridDetails()
        {
            try
            {
                string str1 = $"SELECT USER_ID, USER_NAME,'********' USER_PASSWORD," +    
                    $"DECODE(USER_TYPE, 'USR2', 'Employee', USER_TYPE) AS USER_TYPE," +
                    $"USER_ACTIVE_YN FROM USER_MASTER WHERE USER_TYPE!='Admin'";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateUserOption(string userid, string username, string pswd, string type, string active)
        {
            try
            {
                string sql = $"UPDATE USER_MASTER SET USER_NAME = '{username}',USER_PASSWORD='{pswd}',USER_TYPE='{type}',USER_ACTIVE_YN='{active}' WHERE USER_ID = '{userid}'";
                int n = DBConnection.ExecuteQuery(sql);
                return n;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int DeleteOption(string userid)
        {
            try
            {
                string sql = $"DELETE FROM USER_MASTER  WHERE USER_ID = '{userid}'";
                int n = DBConnection.ExecuteQuery(sql);
                return n;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeleteFromPREmployee(string userid)
        {
            try
            {
                PREmployee objPREmployee = new PREmployee();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["ehEmpNO"] = userid;
                string insertQuery = $"UPDATE PR_EMPLOYEE SET EMP_ACTIVE_YN='N' WHERE EMP_NO=:ehEmpNO";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeleteFromPREmployeeHR(string userid)
        {
            try
            {
                string sql = $"DELETE FROM PR_EMPLOYEE_HR  WHERE EH_EMP_NO = '{userid}'";
                int n = Convert.ToInt32(DBConnection.ExecuteQuery(sql));
                return n;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertUserMasterEmployeeDetails(UserMasterEntity objUserMasterEntity)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pUserId"] = objUserMasterEntity.userId;
                dict["pUserName"] = objUserMasterEntity.userName;
                dict["pUserPswd"] = objUserMasterEntity.userId;
                dict["pUserType"] = objUserMasterEntity.userType;
                dict["pUserCrBy"] = "ADMIN";
                dict["pUserCrDt"] = DateTime.Now;
                dict["pUserActiveYN"] = objUserMasterEntity.userActiveYN.Substring(0, 1);

                string insertQuery = $"INSERT INTO USER_MASTER(USER_ID,USER_NAME,USER_PASSWORD,USER_TYPE,USER_CR_BY,USER_CR_DT,USER_ACTIVE_YN)" +
                    $" VALUES(:pUserId,:pUserName,:pUserPswd,:pUserType,:pUserCrBy,:pUserCrDt,:pUserActiveYN)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int UpdateOption(UserMasterEntity objUserMasterEntity, out string pId)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pUserId"] = objUserMasterEntity.userId;
                dict["pUsername"] = objUserMasterEntity.userName;
                dict["pPswd"] = objUserMasterEntity.userPassword;
                dict["pType"] = objUserMasterEntity.userType;
                dict["pUpBy"] = "ADMIN";
                dict["pUpDt"] = DateTime.Now;
                dict["pActiveYN"] = objUserMasterEntity.userActiveYN;
                string sql = $"UPDATE USER_MASTER SET USER_ACTIVE_YN =:pActiveYN,USER_NAME=:pUsername,USER_PASSWORD=:pPswd,USER_TYPE=:pType," +
                    $"USER_UP_BY=:pUpBy,USER_UP_DT=:pUpDt" +
                    $" WHERE USER_ID =:pUserId";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                pId = objUserMasterEntity.userId;
                return rtval;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserMasterEntity FnGetUserMaster(string pId)
        {
            try
            {
                DataTable dt = new DataTable();
                UserMasterEntity objUserMasterEntity = new UserMasterEntity();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["pId"] = pId;
                string str1 = "SELECT USER_ID, USER_NAME,USER_PASSWORD,USER_TYPE,USER_ACTIVE_YN FROM USER_MASTER WHERE USER_ID=:pId";
                dt = DBConnection.ExecuteQuerySelect(objDict, str1).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    objUserMasterEntity.userId = row["USER_ID"].ToString();
                    objUserMasterEntity.userName = row["USER_NAME"].ToString();
                    objUserMasterEntity.userPassword = row["USER_PASSWORD"].ToString();
                    objUserMasterEntity.userType = row["USER_TYPE"].ToString();
                    objUserMasterEntity.userActiveYN = row["USER_ACTIVE_YN"].ToString();
                }
                return objUserMasterEntity;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the user master data.", ex);
            }
        }
    }
}
