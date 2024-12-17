using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PREmployee
{
    public class PREmployeeManager
    {
        public DataTable LoadGridDetails()
        {
            try
            {
                string str1 = $"SELECT EMP_NO, EMP_NAME,EMP_PWD,TO_CHAR(EMP_DOB,'DD/MM/RRRR') EMP_DOB,TO_CHAR(EMP_JOIN_DATE,'DD/MM/RRRR') EMP_JOIN_DATE,EMP_SALARY,EMP_DEPTNO,EMP_MGRNO,EMP_STATUS,EMP_ACTIVE_YN,EMP_CR_BY,EMP_CR_DT FROM PR_EMPLOYEE";
                DataTable dt = DBConnection.ExecuteDataset(str1);
                return dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
