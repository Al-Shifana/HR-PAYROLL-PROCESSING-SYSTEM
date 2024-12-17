using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PrEmployeeHR
{
    public class PREmployeeHR
    {
        public string ehEmpNO { get; set; }
        public string ehDestination { get; set; }
        public string ehGrade { get; set; }
        public int ehBasic { get; set; }
        public int ehHra { get; set; }
        public int ehConv { get; set; }
        public int ehDa { get; set; }
        public int ehTds { get; set; }
        public int ehEsi { get; set; }
        public string ehCrby { get; set; }
        public DateTime ehCrdt { get; set; }
        public string ehUpby { get; set; }
        public DateTime ehUpdt { get; set; }

    }
}
