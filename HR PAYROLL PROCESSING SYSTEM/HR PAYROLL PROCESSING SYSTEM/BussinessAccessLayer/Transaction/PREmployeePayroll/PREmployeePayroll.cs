using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PREmployeePayroll
{
    public class PREmployeePayroll
    {
        public string prYYYYMM { get; set; }
        public string prEmpNO { get; set; }
        public string prDestination { get; set; }
        public int prDaysPresent { get; set; }
        public int prDaysAbsent { get; set; }
        public int prBasic { get; set; }
        public int prHra { get; set; }
        public int prConv { get; set; }
        public int prDa { get; set; }
        public int prTds { get; set; }
        public int prEsi { get; set; }
        public int prTotEarnings { get; set; }
        public int prTotDedu { get; set; }
        public int prNetPayable { get; set; }
        public string prCrby { get; set; }
        public DateTime prCrdt { get; set; }
        public string prUpby { get; set; }
        public DateTime prUpdt { get; set; }

    }
}
