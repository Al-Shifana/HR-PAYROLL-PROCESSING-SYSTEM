using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PREmployee
{
    public class PREmployee
    {
        public string empNO { get; set; }
        public string empPwd { get; set; }
        public string empName { get; set; }
        public DateTime? empDOB { get; set; }
        public DateTime? empJoinDate { get; set; }
        public double? empSalary { get; set; }
        public string empDeptNo { get; set; }
        public string empMgrNo { get; set; }
        public string empStatus { get; set; }
        public string empActiveYN { get; set; }
        public string empCrby { get; set; }
        public DateTime? empCrdt { get; set; }
        public string empUpby { get; set; }
        public DateTime? empUpdt { get; set; }

    }
}
