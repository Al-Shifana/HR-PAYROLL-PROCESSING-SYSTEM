using BussinessAccessLayer.Master.UserMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessAccessLayer.Master.ErrorCodeMaster;

namespace HR_PAYROLL_PROCESSING_SYSTEM
{
    public partial class LoginPage : System.Web.UI.Page
    {
        UserMasterManger objusermanager = new UserMasterManger();
        protected void Page_Load(object sender, EventArgs e)
        {
                     
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string result = objusermanager.Loginfn(txtUsername.Text, txtPassword.Text);
                if (result == "Admin")
                {
                    Session["uid"] = txtUsername.Text;
                    Response.Redirect("Dashboard.aspx");
                    //string script = "Swal.fire({title: 'Success', text: 'Yeah', icon: 'success'});";
                    //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                }
                else if (result =="USR2")
                {
                    UserMasterEntity objUserMasterEntity = new UserMasterEntity();
                    string uid = txtUsername.Text;
                    string pswd = txtPassword.Text;

                    string rt = objusermanager.LoginId(txtUsername.Text, txtPassword.Text);
                    Session["uid"] = rt;
                    Response.Redirect($"~/Transaction/EmployeeDashboard.aspx?pEmpNo={rt}");
                }
                else
                {
                    lblMessage.Text = "Invalid username and password";
                }
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }

        }// string pEmpNo = Request.QueryString["pEmpNo"];
    }
}