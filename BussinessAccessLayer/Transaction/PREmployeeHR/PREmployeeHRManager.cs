using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PrEmployeeHR
{
    public class PREmployeeHRManager
    {
        public (int, double) InsertPrEmpHR(PREmployeeHR objPREmployeeHR, out string pENo)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pEmpNO"] = objPREmployeeHR.ehEmpNO;
                dict["pDesignation"] = objPREmployeeHR.ehDesignation;
                dict["pGrade"] = objPREmployeeHR.ehGrade;
                dict["pBasic"] = objPREmployeeHR.ehBasic;
                dict["pHra"] = objPREmployeeHR.ehHra;
                dict["pConv"] = objPREmployeeHR.ehConv;
                dict["pDa"] = objPREmployeeHR.ehDa;
                dict["pTds"] = objPREmployeeHR.ehTds;
                dict["pEsi"] = objPREmployeeHR.ehEsi;
                dict["pCrBy"] = "ADMIN";
                dict["pCrDt"] = DateTime.Now;

                string insertQuery = $"INSERT INTO PR_EMPLOYEE_HR(EH_EMP_NO,EH_DESIGNATION,EH_GRADE,EH_BASIC,EH_HRA,EH_CONV,EH_DA,EH_TDS,EH_ESI,EH_CR_BY,EH_CR_DT)" +
                    $" VALUES(:pEmpNO,:pDesignation,:pGrade,:pBasic,:pHra,:pConv,:pDa,:pTds,:pEsi,:pCrBy,:pCrDt)";
                int rtval = DBConnection.ExecuteQuery(dict, insertQuery);
                if (rtval > 0)
                {
                    double earnings = Convert.ToDouble(objPREmployeeHR.ehBasic + objPREmployeeHR.ehHra + objPREmployeeHR.ehConv + objPREmployeeHR.ehDa);
                    double deductions = Convert.ToDouble(objPREmployeeHR.ehTds + objPREmployeeHR.ehEsi);
                    double salary = earnings - deductions;
                    Dictionary<string, object> dict1 = new Dictionary<string, object>();
                    dict1["empSalary"] = salary;
                    dict1["ehEmpNO"] = objPREmployeeHR.ehEmpNO;

                    string updateQuery = $"UPDATE PR_EMPLOYEE SET EMP_SALARY=:empSalary WHERE EMP_NO=:ehEmpNO";
                    int rtv = DBConnection.ExecuteQuery(dict1, updateQuery);
                    pENo = objPREmployeeHR.ehEmpNO;
                    return (rtval, salary);
                }
                pENo = null;
                return (rtval, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable LoadGridDetails(string empNo)
        {
            try
            {
                string str1 = $"SELECT EH_EMP_NO," +
                    $"DECODE(EH_DESIGNATION,EH_DESIGNATION,(SELECT CM_DESC FROM CODES_MASTER WHERE  CM_CODE = EH_DESIGNATION AND CM_TYPE='DESIGNATION'),NULL) EH_DESIGNATION," +
                    $"DECODE(EH_GRADE,EH_GRADE,(SELECT CM_DESC FROM CODES_MASTER WHERE  CM_CODE = EH_GRADE AND CM_TYPE='GRADE'),NULL) EH_GRADE," +
                    $"EH_BASIC,EH_HRA,EH_CONV,"+
                    $"EH_DA,EH_TDS,EH_ESI FROM PR_EMPLOYEE_HR WHERE EH_EMP_NO='{empNo}'";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LoadHistoryDetails()
        {
            try
            {
                string str1 = $"SELECT EH_EMP_NO," +
                    $"DECODE(EH_DESIGNATION,EH_DESIGNATION,(SELECT CM_DESC FROM CODES_MASTER WHERE  CM_CODE = EH_DESIGNATION AND CM_TYPE='DESIGNATION'),NULL) EH_DESIGNATION," +
                    $"DECODE(EH_GRADE,EH_GRADE,(SELECT CM_DESC FROM CODES_MASTER WHERE  CM_CODE = EH_GRADE AND CM_TYPE='GRADE'),NULL) EH_GRADE," +
                    $"EH_BASIC,EH_HRA,EH_CONV," +
                    $"EH_DA,EH_TDS,EH_ESI,DECODE(EH_ACTION_TYPE, 'D', 'Delete','I', 'Insert', 'U', 'Update',  EH_ACTION_TYPE) AS EH_ACTION_TYPE FROM PR_EMPLOYEE_HR_HIST";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public (int,double) UpdatePREmployeeHR(PREmployeeHR objPREmployeeHR,out string pENo)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["pEmpNO"] = objPREmployeeHR.ehEmpNO;
                dict["pDesg"] = objPREmployeeHR.ehDesignation;
                dict["pGrade"] = objPREmployeeHR.ehGrade;
                dict["pBasic"] = Convert.ToDouble(objPREmployeeHR.ehBasic);
                dict["pHRA"] = Convert.ToDouble(objPREmployeeHR.ehHra);
                dict["pCONV"] = Convert.ToDouble(objPREmployeeHR.ehConv);
                dict["pDA"] = Convert.ToDouble(objPREmployeeHR.ehDa);
                dict["pTDS"] = Convert.ToDouble(objPREmployeeHR.ehTds);
                dict["pESI"] = Convert.ToDouble(objPREmployeeHR.ehEsi);             
                dict["pUpBy"] = "ADMIN";
                dict["pUpDt"] = DateTime.Now;               
                string sql = "UPDATE PR_EMPLOYEE_HR SET EH_DESIGNATION=:pDesg," +
                    "EH_GRADE=:pGrade,EH_BASIC=:pBasic,EH_HRA=:pHRA,EH_CONV=:pCONV,EH_DA=:pDA," +
                    "EH_TDS=:pTDS,EH_ESI=:pESI,EH_UP_BY=:pUpBy,EH_UP_DT=:pUpDt WHERE EH_EMP_NO = :pEmpNO";
                int rtval = DBConnection.ExecuteQuery(dict, sql);
                if (rtval > 0)
                {
                    double earnings = Convert.ToDouble(objPREmployeeHR.ehBasic + objPREmployeeHR.ehHra + objPREmployeeHR.ehConv + objPREmployeeHR.ehDa);
                    double deductions = Convert.ToDouble(objPREmployeeHR.ehTds + objPREmployeeHR.ehEsi);
                    double salary = earnings - deductions;
                    Dictionary<string, object> dict1 = new Dictionary<string, object>();
                    dict1["empSalary"] = salary;
                    dict1["ehEmpNO"] = objPREmployeeHR.ehEmpNO;
                   
                    string insertQuery = $"UPDATE PR_EMPLOYEE SET EMP_SALARY=:empSalary WHERE EMP_NO=:ehEmpNO";
                    int rtv = DBConnection.ExecuteQuery(dict1, insertQuery);
                    pENo = objPREmployeeHR.ehEmpNO;
                    return (rtval, salary);
                }
                pENo = null;
                return (rtval, 0);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public PREmployeeHR FnGetEmployeeHR(string pEmpNo)
        {
            try
            {
                DataTable dt = new DataTable();
                PREmployeeHR objPREmployeeHR = new PREmployeeHR();
                Dictionary<string, object> objDict = new Dictionary<string, object>();
                objDict["pEmpNo"] = pEmpNo;
                string str1 = $"SELECT EH_EMP_NO,EH_DESIGNATION,EH_GRADE,EH_BASIC,EH_HRA,EH_CONV," +
                     $"EH_DA,EH_TDS,EH_ESI FROM PR_EMPLOYEE_HR WHERE EH_EMP_NO=:pEmpNo";
                dt = DBConnection.ExecuteQuerySelect(objDict, str1).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    objPREmployeeHR.ehEmpNO = !string.IsNullOrEmpty(row["EH_EMP_NO"].ToString()) ? row["EH_EMP_NO"].ToString() : string.Empty;
                    objPREmployeeHR.ehDesignation = !string.IsNullOrEmpty(row["EH_DESIGNATION"].ToString()) ? row["EH_DESIGNATION"].ToString() : string.Empty;
                    objPREmployeeHR.ehGrade = !string.IsNullOrEmpty(row["EH_GRADE"].ToString()) ? row["EH_GRADE"].ToString() : string.Empty;
                    objPREmployeeHR.ehBasic = !string.IsNullOrEmpty(row["EH_BASIC"].ToString()) ? Convert.ToDouble(row["EH_BASIC"].ToString()) : (double?)null;
                    objPREmployeeHR.ehHra = !string.IsNullOrEmpty(row["EH_HRA"].ToString()) ? Convert.ToDouble(row["EH_HRA"].ToString()) : (double?)null;
                    objPREmployeeHR.ehConv = !string.IsNullOrEmpty(row["EH_CONV"].ToString()) ? Convert.ToDouble(row["EH_CONV"].ToString()) : (double?)null;
                    objPREmployeeHR.ehDa = !string.IsNullOrEmpty(row["EH_DA"].ToString()) ? Convert.ToDouble(row["EH_DA"].ToString()) : (double?)null;
                    objPREmployeeHR.ehTds = !string.IsNullOrEmpty(row["EH_TDS"].ToString()) ? Convert.ToDouble(row["EH_TDS"].ToString()) : (double?)null;
                    objPREmployeeHR.ehEsi = !string.IsNullOrEmpty(row["EH_ESI"].ToString()) ? Convert.ToDouble(row["EH_ESI"].ToString()) : (double?)null;
                }
                return objPREmployeeHR;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the code master data.", ex);
            }
        }

        public int FnGetEmployeeHRCount(string pEmpNo)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                Dictionary<string, object> paramVal = new Dictionary<string, object>();
                string str1 = $"SELECT COUNT(*) COUNT FROM PR_EMPLOYEE_HR WHERE EH_EMP_NO='{pEmpNo}'";


                object obj = DBConnection.ExecuteScalar(str1);
                int status = !string.IsNullOrEmpty(obj.ToString()) ? Convert.ToInt32(obj) : 0;
                return status;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
