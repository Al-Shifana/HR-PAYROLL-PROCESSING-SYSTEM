using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Master.CodeMaster
{
    public class CodeMasterEntity
    {
        public string cmCode { get; set; }
        public string cmType { get; set; }
        public string cmDesc { get; set; }
        public int cmValue { get; set; }
        public string cmCrBy { get; set; }
        public DateTime cmCrDt { get; set; }
        public string cmUpBy { get; set; }
        public DateTime cmUpDt { get; set; }
        public string cmActiveYN { get; set; }

    }
}
