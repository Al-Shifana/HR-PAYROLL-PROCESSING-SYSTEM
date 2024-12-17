using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.Master.UserMaster
{
    public class UserMasterEntity
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string userType { get; set; }
        public string userCrBy { get; set; }
        public DateTime userCrDt { get; set; }
        public string userUpBy { get; set; }
        public DateTime userUpDt { get; set; }
        public string userActiveYN { get; set; }
    }
}
