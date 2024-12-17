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
        public int IsUserMasterExist(string userId)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM USER_MASTER WHERE USER_ID= {userId}";
                int result = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int InsertUserMasterDetails(UserMasterEntity objUserMasterEntity)
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
                dict["userUpBy"] = "ADMIN";
                dict["userUpDt"] = DateTime.Now;
                dict["userActiveYN"] = objUserMasterEntity.userActiveYN.Substring(0, 1);

                string insertQuery = $"INSERT INTO USER_MASTER VALUES(:userId,:userName,:userPassword,:userType,:userCrBy,:userCrDt,:userUpBy,:userUpDt,:userActiveYN)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
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
                string str1 = $"SELECT USER_ID, USER_NAME,USER_PASSWORD,USER_TYPE,USER_ACTIVE_YN FROM USER_MASTER WHERE USER_TYPE!='ADMIN'";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UpdateUserOption(string userid, string username, string pswd, string type, string active)
        {
            try
            {
                string sql = $"UPDATE USER_MASTER SET USER_NAME = '{username}',USER_PASSWORD='{pswd}',USER_TYPE='{type}',USER_ACTIVE_YN='{active}' WHERE USER_ID = '{userid}'";
                return sql;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public string DeleteOption(string userid)
        {
            try
            {
                string sql = $"DELETE FROM USER_MASTER  WHERE USER_ID = '{userid}'";
                return sql;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
