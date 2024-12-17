using BussinessAccessLayer.Transaction.PREmployee;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer
{
    public class PREmployeeManager
    {
        public DataTable LoadGridDetails()
        {
            try
            {
                string str1 = $"SELECT EMP_NO,EMP_NAME,EMP_PWD,TO_CHAR(EMP_DOB, 'DD/MM/RRRR')EMP_DOB," + 
                    $"TO_CHAR(EMP_JOIN_DATE,'DD/MM/RRRR') EMP_JOIN_DATE,EMP_SALARY," +
                    $"DECODE(EMP_DEPTNO,EMP_DEPTNO,(SELECT DEPT_NAME FROM DEPARTMENT_MASTER WHERE  DEPT_NO = EMP_DEPTNO),NULL) EMP_DEPTNO," +
                    $"DECODE(EMP_STATUS,EMP_STATUS,(SELECT CM_DESC FROM CODES_MASTER WHERE  CM_CODE = EMP_STATUS AND CM_TYPE='STATUS'),NULL) EMP_STATUS," +
                    $"EMP_ACTIVE_YN,EMP_CR_BY,EMP_CR_DT FROM PR_EMPLOYEE";

                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string FnCheckpassword(string usrid)
        {
            try
            {
                string str = $"SELECT EMP_PWD FROM PR_EMPLOYEE WHERE EMP_NO='{usrid}'";
                object rt = DBConnection.ExecuteScalar(str);
                string st = Convert.ToString(rt);
                return st;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataTable CheckJoinDate()
        {
            try
            {
                string str=$"SELECT EMP_JOIN_YEAR, EMP_JOIN_MONTH FROM(SELECT EXTRACT(YEAR FROM EMP_JOIN_DATE) EMP_JOIN_YEAR, " +
                    "EXTRACT(MONTH FROM EMP_JOIN_DATE) EMP_JOIN_MONTH FROM PR_EMPLOYEE ORDER BY EMP_JOIN_DATE) WHERE ROWNUM = 1";
                DataTable dt = DBConnection.ExecuteDataset(str);
                return dt;

            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataTable CheckDays()
        {
            try
            {
                string str = $"SELECT EMP_NO FROM PR_EMPLOYEE WHERE  EXTRACT(MONTH FROM EMP_JOIN_DATE) <= TO_NUMBER(P_MM) AND EXTRACT(YEAR FROM EMP_JOIN_DATE) = TO_NUMBER(P_YYYY)" +
                    $"AND NOT EXISTS(SELECT 1 FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_EMP_NO = EMP_NO AND ATT_YYYYMM = P_YYYYMM)";
                DataTable dt = DBConnection.ExecuteDataset(str);
                return dt;

            }
            catch (Exception)
            {
                throw;
            }

        }
        public int UpdatePassword(string pEmpId ,string pPswd)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["ehEmpNO"] = pEmpId;
                dict["empPswd"] = pPswd;
                string insertQuery = $"UPDATE PR_EMPLOYEE SET EMP_PWD=:empPswd WHERE EMP_NO=:ehEmpNO";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                if (rtval > 0)
                {
                    string upUserPwd = $"UPDATE USER_MASTER SET USER_PASSWORD=:empPswd WHERE USER_ID=:ehEmpNO";
                    int uprt = DBConnection.ExecuteQuery(dict, upUserPwd);
                }
                else
                {

                }
                return rtval;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public PREmployee LoadEmpDetails(string pEmpNo)
        {
            try
            {
                PREmployee objPREmployee = new PREmployee();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["empNO"] = pEmpNo;           

                string str1 = $"SELECT EMP_NO, EMP_NAME,TO_CHAR(EMP_DOB, 'DD/MM/RRRR') AS EMP_DOB, TO_CHAR(EMP_JOIN_DATE, 'DD/MM/RRRR') AS EMP_JOIN_DATE, " +
                    $" DECODE(EMP_DEPTNO, EMP_DEPTNO, (SELECT DEPT_NAME FROM DEPARTMENT_MASTER WHERE DEPT_NO = EMP_DEPTNO), NULL) AS EMP_DEPTNO," +
                    $"DECODE(EMP_MGRNO, EMP_MGRNO, (SELECT e1.EMP_NAME FROM PR_EMPLOYEE e1 JOIN PR_EMPLOYEE e2 ON e1.EMP_NO = e2.EMP_MGRNO WHERE e2.EMP_NO = :empNO), NULL) AS EMP_MGRNO, " +
                    $"DECODE(EMP_STATUS, EMP_STATUS, (SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = EMP_STATUS AND CM_TYPE = 'STATUS'), NULL) AS EMP_STATUS," +
                    $" EMP_ACTIVE_YN FROM PR_EMPLOYEE WHERE EMP_NO =:empNO";

                DataTable dt  = DBConnection.ExecuteQuerySelect(dict, str1).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    objPREmployee.empNO = row["EMP_NO"].ToString();
                    objPREmployee.empName = row["EMP_NAME"].ToString();
                    objPREmployee.empDOB =Convert.ToDateTime(row["EMP_DOB"].ToString());
                    objPREmployee.empJoinDate = Convert.ToDateTime(row["EMP_JOIN_DATE"].ToString());
                    objPREmployee.empDeptNo = row["EMP_DEPTNO"].ToString();
                    objPREmployee.empMgrNo = !string.IsNullOrEmpty(row["EMP_MGRNO"].ToString()) ? row["EMP_MGRNO"].ToString() : string.Empty;
                    objPREmployee.empStatus = row["EMP_STATUS"].ToString();
                    objPREmployee.empActiveYN = row["EMP_ACTIVE_YN"].ToString();
                }
                return objPREmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LoadUserDetails(string str)
        {
            try
            {
                string sql = $"SELECT EMP_NO, EMP_NAME,EMP_PWD,TO_CHAR(EMP_DOB,'DD/MM/RRRR') EMP_DOB,TO_CHAR(EMP_JOIN_DATE,'DD/MM/RRRR')" +
                    $" EMP_JOIN_DATE,EMP_SALARY,EMP_DEPTNO,EMP_MGRNO,EMP_STATUS,EMP_ACTIVE_YN,EMP_CR_BY,EMP_CR_DT FROM PR_EMPLOYEE WHERE EMP_NO='" + str + "'";
                DataTable dt = DBConnection.ExecuteDataset(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int IsEmployeeExist(string empNo)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM PR_EMPLOYEE WHERE EMP_NO= {empNo}";
                int result = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int InsertEmployeeDetails(PREmployee objPREmployee)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["empNO"] = objPREmployee.empNO;
                dict["empName"] = objPREmployee.empName;
                dict["empPwd"] = objPREmployee.empNO;
                dict["empDOB"] = objPREmployee.empDOB;
                dict["empJoinDate"] = objPREmployee.empJoinDate;
                dict["empDeptNo"] = objPREmployee.empDeptNo;
                dict["empMgrNo"] = objPREmployee.empMgrNo;
                dict["empStatus"] = objPREmployee.empStatus;
                dict["empCrBy"] = "ADMIN";
                dict["empCrDt"] = DateTime.Now;
                dict["empActiveYN"] = objPREmployee.empActiveYN.Substring(0, 1);

                string insertQuery = $"INSERT INTO PR_EMPLOYEE(EMP_NO,EMP_PWD,EMP_NAME,EMP_DOB,EMP_JOIN_DATE," +
                    $"EMP_DEPTNO,EMP_MGRNO,EMP_STATUS,EMP_ACTIVE_YN,EMP_CR_BY,EMP_CR_DT) VALUES" +
                    $"(:empNO,:empPwd,:empName,:empDOB,:empJoinDate,:empDeptNo,:empMgrNo,:empStatus,:empActiveYN,:empCrBy,:empCrDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);

                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string Funct()
        {
            try
            {
                string yr = DateTime.Now.Year.ToString().Substring(2);
                string sql = $"SELECT 'E-' || {yr} || '-' || LPAD(ACCNT_SEQ.NEXTVAL, 5, '0') FROM DUAL";
                string str1 = Convert.ToString(DBConnection.ExecuteScalar(sql));
                return str1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double InsertSalary(PREmployee objPREmployee)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["ehEmpNO"] = objPREmployee.empNO;
                dict["empSalary"] = objPREmployee.empSalary;
                string insertQuery = $"UPDATE PR_EMPLOYEE SET EMP_SALARY=:empSalary WHERE EMP_NO=:ehEmpNO";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                return rtval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public PREmployee FnGetEmployee(string pEmpNo)
        {
            try
            {
                DataTable dt = new DataTable();
                PREmployee objPREmployee = new PREmployee();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["pEmpNo"] = pEmpNo;
                string str1 = "SELECT EMP_NO, EMP_NAME, EMP_DOB, EMP_JOIN_DATE, EMP_SALARY, EMP_DEPTNO, EMP_MGRNO, EMP_STATUS, EMP_ACTIVE_YN" +
                    " FROM PR_EMPLOYEE WHERE EMP_NO=:pEmpNo";
                dt = DBConnection.ExecuteQuerySelect(objDict, str1).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    objPREmployee.empNO = !string.IsNullOrEmpty(row["EMP_NO"].ToString()) ? row["EMP_NO"].ToString() : string.Empty;
                    objPREmployee.empName = !string.IsNullOrEmpty(row["EMP_NAME"].ToString()) ? row["EMP_NAME"].ToString() : string.Empty;
                    objPREmployee.empDOB = !string.IsNullOrEmpty(row["EMP_DOB"].ToString()) ? Convert.ToDateTime(row["EMP_DOB"].ToString()) : (DateTime?)null;
                    objPREmployee.empJoinDate = !string.IsNullOrEmpty(row["EMP_JOIN_DATE"].ToString()) ? Convert.ToDateTime(row["EMP_JOIN_DATE"].ToString()) : (DateTime?)null;
                    objPREmployee.empSalary = !string.IsNullOrEmpty(row["EMP_SALARY"].ToString()) ? Convert.ToDouble(row["EMP_SALARY"].ToString()) : (double?)null;
                    objPREmployee.empDeptNo = !string.IsNullOrEmpty(row["EMP_DEPTNO"].ToString()) ? row["EMP_DEPTNO"].ToString() : string.Empty;
                    objPREmployee.empMgrNo = !string.IsNullOrEmpty(row["EMP_MGRNO"].ToString()) ? row["EMP_MGRNO"].ToString() : string.Empty;
                    objPREmployee.empStatus = !string.IsNullOrEmpty(row["EMP_STATUS"].ToString()) ? row["EMP_STATUS"].ToString() : string.Empty;
                    objPREmployee.empActiveYN = !string.IsNullOrEmpty(row["EMP_ACTIVE_YN"].ToString()) ? row["EMP_ACTIVE_YN"].ToString() : string.Empty;
                }
                return objPREmployee;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the code master data.", ex);
            }
        }
        public int UpdateOption(PREmployee objPREmployee, out string pEmpNo)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pNo"] = objPREmployee.empNO;
                dict["pName"] = objPREmployee.empName;
                dict["pDOB"] = objPREmployee.empDOB;
                dict["pJoinDate"] = objPREmployee.empJoinDate;
                dict["pSalary"] = objPREmployee.empSalary;
                dict["pDeptNo"] = objPREmployee.empDeptNo;
                dict["pMgrNo"] = objPREmployee.empMgrNo;
                dict["pStatus"] = objPREmployee.empStatus;
                dict["pUpBy"] = "ADMIN";
                dict["pUpDt"] = DateTime.Now;
                dict["pActiveYN"] = objPREmployee.empActiveYN;
                string sql = $"UPDATE PR_EMPLOYEE SET EMP_NAME =:pName,EMP_DOB=:pDOB,EMP_JOIN_DATE=:pJoinDate," +
                    $"EMP_SALARY=:pSalary,EMP_DEPTNO=:pDeptNo,EMP_MGRNO=:pMgrNo,EMP_STATUS=:pStatus,EMP_ACTIVE_YN=:pActiveYN," +
                    $"EMP_UP_BY=:pUpBy,EMP_UP_DT=:pUpDt WHERE EMP_NO =:pNo";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                pEmpNo = objPREmployee.empNO;
                return rtval;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAttendance(string pMonth, string pYear)
        {
            try
            {
                string sql = $"SELECT EMP_NO,EMP_NAME," +
                    $"(SELECT ATT_DAYS_PRESENT FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_YYYYMM = '{pYear + pMonth}' AND ATT_EMP_NO = EMP_NO) ATT_DAYS_PRESENT," +
                    $"(SELECT ATT_DAYS_ABSENT FROM PR_EMPLOYEE_ATTENDANCE WHERE ATT_YYYYMM = '{pYear + pMonth}' AND ATT_EMP_NO = EMP_NO) ATT_DAYS_ABSENT, " +
                    $"(SELECT 'Y' STATUS FROM PR_EMPLOYEE_PAYROLL WHERE PR_YYYMM = '{pYear + pMonth}' AND ROWNUM = 1 AND PR_EMP_NO = EMP_NO) STATUS " +
                    $"FROM PR_EMPLOYEE WHERE EXTRACT(MONTH FROM EMP_JOIN_DATE) <= '{pMonth}' AND EXTRACT(YEAR FROM EMP_JOIN_DATE) <= '{pYear}' AND EMP_ACTIVE_YN = 'Y'";
                DataTable dt = DBConnection.ExecuteDataset(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
