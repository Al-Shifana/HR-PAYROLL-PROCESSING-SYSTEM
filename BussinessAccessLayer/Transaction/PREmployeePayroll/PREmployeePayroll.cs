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
        public double? prBasic { get; set; }
        public double? prHra { get; set; }
        public double? prConv { get; set; }
        public double? prDa { get; set; }
        public double? prTds { get; set; }
        public double? prEsi { get; set; }
        public double? prTotEarnings { get; set; }
        public double? prTotDedu { get; set; }
        public double? prNetPayable { get; set; }
        public string prCrby { get; set; }
        public DateTime? prCrdt { get; set; }
        public string prUpby { get; set; }
        public DateTime? prUpdt { get; set; }

    }
}
