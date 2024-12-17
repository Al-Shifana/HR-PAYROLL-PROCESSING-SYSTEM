using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PREmployeePayroll
{
    public class PREmployeePayrollManager
    {
        public int IsPRYearMonthExist(PREmployeePayroll objPREmployeePayroll)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYMM"] = objPREmployeePayroll.prYYYYMM;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_PAYROLL WHERE PR_YYYMM=:pYYMM";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LoadGridDetails(string pMonth, string pYear)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYMM"] = pYear + pMonth;
                string sql = $"SELECT PR_EMP_NO,(SELECT EMP_NAME FROM PR_EMPLOYEE WHERE EMP_NO=PR_EMP_NO) PR_EMP_NAME," +

                    $" TO_CHAR(TO_DATE(SUBSTR(PR_YYYMM, 1, 6), 'YYYYMM'), 'YYYY') || ' ' || TO_CHAR(TO_DATE(SUBSTR(PR_YYYMM, 1, 6), 'YYYYMM'), 'Month') PR_YYYMM," +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = PR_DESIGNATION AND CM_TYPE = 'DESIGNATION') PR_DESIGNATION," +
                    $"PR_DAYS_PRESENT,PR_DAYS_ABSENT,PR_BASIC,PR_HRA,PR_CONV,PR_DA,PR_TDS,PR_ESI,PR_TOT_EARNINGS,PR_TOT_DEDU,PR_NET_PAYABLE FROM PR_EMPLOYEE_PAYROLL WHERE PR_YYYMM=:pYYMM";
                DataTable dt = DBConnection.ExecuteQuerySelect(dict, sql).Tables[0];
                return dt;
                // $"PR_YYYMM ," +
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LoadGridforSlip(string pMonth, string pYear,string pEmpNo)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYMM"] = pYear + pMonth;
                dict["pEmpNo"] = pEmpNo;
                string sql = $"SELECT PR_EMP_NO,(SELECT EMP_NAME FROM PR_EMPLOYEE WHERE EMP_NO=PR_EMP_NO) PR_EMP_NAME,PR_YYYMM,(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = PR_DESIGNATION AND CM_TYPE = 'DESIGNATION') PR_DESIGNATION,PR_DAYS_PRESENT,PR_DAYS_ABSENT,PR_BASIC,PR_HRA,PR_CONV,PR_DA,PR_TDS,PR_ESI,PR_TOT_EARNINGS,PR_TOT_DEDU,PR_NET_PAYABLE FROM PR_EMPLOYEE_PAYROLL WHERE PR_YYYMM=:pYYMM AND PR_EMP_NO=:pEmpNo";
                DataTable dt = DBConnection.ExecuteQuerySelect(dict, sql).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int FnIsPayrollProcessedOrNot(string pYYYYMM)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYMM"] = pYYYYMM;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_PAYROLL WHERE PR_YYYMM=:pYYMM";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int FnIsPayrollProcessedOrNotForPrevMonth(string pYYYYMM)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYMM"] = pYYYYMM;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_PAYROLL WHERE PR_YYYMM=:pYYMM";
                dt = DBConnection.ExecuteQuerySelect(dict, query).Tables[0];
                int result = !string.IsNullOrEmpty(dt.Rows[0]["COUNT"].ToString()) ? Convert.ToInt32(dt.Rows[0]["COUNT"].ToString()) : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool IsPayrollProcessed(string pYYYYMM)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pYYMM"] = pYYYYMM;
                string query = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_PAYROLL WHERE PR_YYYMM=:pYYMM";
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
        public DataTable LoadDetails(string str, string pYYMM)
        {
            try
            {

                string sql = $"SELECT h.EH_EMP_NO,a.ATT_YYYYMM,e.EMP_NAME," +
                   $" DECODE(EMP_DEPTNO, EMP_DEPTNO, (SELECT DEPT_NAME FROM DEPARTMENT_MASTER WHERE DEPT_NO = EMP_DEPTNO), NULL) AS EMP_DEPTNO," +
                    $" TO_CHAR(TO_DATE(SUBSTR(a.ATT_YYYYMM, 1, 4) || SUBSTR(a.ATT_YYYYMM, 5, 2), 'YYYYMM'), 'Month') AS MM_ATT_YYYYMM," +
                    $"TO_CHAR(TO_DATE(SUBSTR(a.ATT_YYYYMM, 1, 4) || SUBSTR(a.ATT_YYYYMM, 5, 2), 'YYYYMM'), 'YYYY') AS YYYY_ATT_YYYYMM, " +
                    "DECODE(EH_DESIGNATION,EH_DESIGNATION,(SELECT CM_DESC FROM CODES_MASTER WHERE  CM_CODE = EH_DESIGNATION AND CM_TYPE='DESIGNATION'),NULL)EH_DESIGNATION," +
                    "h.EH_BASIC, h.EH_HRA, h.EH_CONV, h.EH_DA, h.EH_TDS, h.EH_ESI,p.PR_TOT_EARNINGS, p.PR_TOT_DEDU,p.PR_NET_PAYABLE," +
                    "number_to_words(p.PR_NET_PAYABLE) AS PR_NET_PAYABLE_IN_WORDS " +
                    " FROM PR_EMPLOYEE e LEFT JOIN PR_EMPLOYEE_HR h ON e.EMP_NO = h.EH_EMP_NO LEFT JOIN PR_EMPLOYEE_ATTENDANCE a ON h.EH_EMP_NO = a.ATT_EMP_NO LEFT JOIN PR_EMPLOYEE_PAYROLL p ON a.ATT_EMP_NO = p.PR_EMP_NO " +
                    "WHERE h.EH_EMP_NO='" + str + "' AND ROWNUM = 1 AND a.ATT_YYYYMM='" + pYYMM + "'";
                DataTable dt = DBConnection.ExecuteDataset(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int FnProcessPayroll(string pYYYYMM, string pCrBy)
        {
            int status = DBConnection.ProcessPayroll(pYYYYMM, pCrBy);
            return status;
        }
    }
}
