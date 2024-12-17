using BussinessAccessLayer.Master.UserMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserMasterEntity objusermasterentity = new UserMasterEntity();

            objusermasterentity.userName = txtUsername.Text;
            objusermasterentity.userPassword = txtPassword.Text;

            string usrname = objusermasterentity.userName;
            string pswd = objusermasterentity.userPassword;

            string str = $"SELECT USER_TYPE FROM USER_MASTER WHERE USER_NAME='{usrname}' AND USER_PASSWORD='{pswd}'";
            object rt = DBConnection.ExecuteScalar(str);
            string st = Convert.ToString(rt);
            if (st=="ADMIN")
            {
                Response.Redirect("Master/CodeMaster.aspx");
            }
        }
    }
}