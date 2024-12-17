using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PrEmployeeAttendence
{
    public class PREmployeeAttendenceManager
    {
        public DataTable FnLoadEmployeeAttendance(string yymm)
        {
            try
            {
                   string sql = $"SELECT ATT_EMP_NO FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_YYYYMM={yymm}";
                    DataTable dt = DBConnection.ExecuteDataset(sql);
                    return dt;    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsAttendanceFound(PREmployeeAttendence objPrEmployeeAttendence)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYMM"] = objPrEmployeeAttendence.attYYYYMM;
                dict["pEmpNo"]= objPrEmployeeAttendence.attEmpNO;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_YYYYMM=:pYYMM AND ATT_EMP_NO=:pEmpNo";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;

                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateAttendanceDetails(PREmployeeAttendence objPrEmployeeAttendence, out string pEmpId, out string pYYMM)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pEmpNo"] = objPrEmployeeAttendence.attEmpNO;
                dict["pYYYYMM"] = objPrEmployeeAttendence.attYYYYMM;
                dict["pPresentdays"] = objPrEmployeeAttendence.attDaysPresent;
                dict["pAbsentdays"] = objPrEmployeeAttendence.attDaysAbsent;
                dict["pUpBy"] = "ADMIN";
                dict["pUpDt"] = DateTime.Now;

                string insertQuery = $"UPDATE PR_EMPLOYEE_ATTENDANCE SET ATT_DAYS_PRESENT=:pPresentdays, ATT_DAYS_ABSENT=:pAbsentdays, ATT_UP_BY=:pUpBy, ATT_UP_DT=:pUpDt " +
                    $"WHERE ATT_EMP_NO=:pEmpNo AND ATT_YYYYMM=:pYYYYMM";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                pEmpId = objPrEmployeeAttendence.attEmpNO;
                pYYMM = objPrEmployeeAttendence.attYYYYMM;
                return rtval;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int InsertAttendanceDetails(PREmployeeAttendence objPrEmployeeAttendence, out string pEmpId, out string pYYMM)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pEmpNo"] = objPrEmployeeAttendence.attEmpNO;
                dict["pYYYYMM"] = objPrEmployeeAttendence.attYYYYMM;
                dict["pPresentdays"] = objPrEmployeeAttendence.attDaysPresent;
                dict["pAbsentdays"] = objPrEmployeeAttendence.attDaysAbsent;
                dict["pCrBy"] = "ADMIN";
                dict["pCrDt"] = DateTime.Now;

                string insertQuery = $"INSERT INTO PR_EMPLOYEE_ATTENDANCE (ATT_EMP_NO, ATT_YYYYMM, ATT_DAYS_PRESENT, ATT_DAYS_ABSENT, ATT_CR_BY, ATT_CR_DT) " +
                    $"VALUES(:pEmpNo,:pYYYYMM,:pPresentdays,:pAbsentdays,:pCrBy,:pCrDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                pEmpId = objPrEmployeeAttendence.attEmpNO;
                pYYMM = objPrEmployeeAttendence.attYYYYMM;
                return rtval;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int InsertAttendanceNewEmp(PREmployeeAttendence objPrEmployeeAttendence)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pEmpNo"] = objPrEmployeeAttendence.attEmpNO;
                dict["pYYYYMM"] = objPrEmployeeAttendence.attYYYYMM;
                dict["pPresentdays"] = objPrEmployeeAttendence.attDaysPresent;
                dict["pAbsentdays"] = objPrEmployeeAttendence.attDaysAbsent;
                dict["pCrBy"] = "ADMIN";
                dict["pCrDt"] = DateTime.Now;

                string insertQuery = $"INSERT INTO PR_EMPLOYEE_ATTENDANCE (ATT_EMP_NO, ATT_YYYYMM, ATT_DAYS_PRESENT, ATT_DAYS_ABSENT, ATT_CR_BY, ATT_CR_DT) " +
                    $"VALUES(:pEmpNo,:pYYYYMM,:pPresentdays,:pAbsentdays,:pCrBy,:pCrDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                return rtval;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public int FnCheckPreviousAttendanceProcessed(string pYYYYMM)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dict = new Dictionary<string, object>();
        //        dict["pEmpNo"] = objPrEmployeeAttendence.attEmpNO;


        //        return 1;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        public int FnAttendanceProcessedOrNot(string pYYYY, string pMM)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYYYMM"] = pYYYY + pMM;
                dict["pYYYY"] = pYYYY;
                dict["pMM"] = pMM;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_YYYYMM = :pYYYYMM AND " +
                    $"ATT_EMP_NO IN (SELECT EMP_NO FROM PR_EMPLOYEE WHERE EXTRACT(YEAR FROM EMP_JOIN_DATE) <= :pYYYY AND EXTRACT(MONTH FROM EMP_JOIN_DATE) <= :pMM)";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int FnAttendanceProcess(string pYYYY, string pMM)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYYYMM"] = pYYYY + pMM;
                dict["pYYYY"] = pYYYY;
                dict["pMM"] = pMM;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_YYYYMM = :pYYYYMM";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int FnInsertAttendance(string pYYYYMM,string pCrBy)
        {
            int status = DBConnection.ProcessAttendance(pYYYYMM, pCrBy);
            return status;
        }


        public int FnAttendanceProcessedForCurrMonth(string pYYYY, string pMM)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYYYMM"] = pYYYY + pMM;
                dict["pYYYY"] = pYYYY;
                dict["pMM"] = pMM;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_YYYYMM = :pYYYYMM AND ATT_EMP_NO IN (SELECT EMP_NO FROM PR_EMPLOYEE WHERE EXTRACT(YEAR FROM EMP_JOIN_DATE) <= :pYYYY AND EXTRACT(MONTH FROM EMP_JOIN_DATE) <= :pMM)";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
