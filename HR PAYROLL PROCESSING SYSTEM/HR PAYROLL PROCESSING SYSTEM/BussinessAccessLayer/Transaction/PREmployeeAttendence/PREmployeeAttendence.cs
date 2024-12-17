using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Transaction.PrEmployeeAttendence
{
    public class PREmployeeAttendence
    {
        public string attEmpNO { get; set; }
        public string attYYYYMM { get; set; }
        public int attDaysPresent { get; set; }
        public int attDaysAbsent { get; set; }
        public string attCrby{ get; set; }
        public DateTime attCrdt { get; set; }
        public string attUpby { get; set; }
        public DateTime attUpdt { get; set; }

    }
}
