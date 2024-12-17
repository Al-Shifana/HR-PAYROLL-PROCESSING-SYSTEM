using BussinessAccessLayer;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Transaction.PREmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Transaction
{
    public partial class EmployeeDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string empId = Request.QueryString["pEmpNo"];

                    if (!string.IsNullOrEmpty(empId))
                    {
                        FnFillEmployee(empId);
                    }
                    Session["empId"] = empId;
                    //DropDown();
                }

            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        protected void FnFillEmployee(string pEmpNo)
        {
            try
            {
                PREmployee objPREmployee = new PREmployee();
                PREmployeeManager objPREmployeeManager = new PREmployeeManager();

                objPREmployee = objPREmployeeManager.LoadEmpDetails(pEmpNo);

                txtEmpNo.Text = objPREmployee.empNO;
                txtEmpName.Text = objPREmployee.empName;
                txtDOB.Text = objPREmployee.empDOB.Value.ToString("dd-MM-yyyy");
                txtJoinDate.Text = objPREmployee.empJoinDate.Value.ToString("dd-MM-yyyy");
                ddlDept.Text = objPREmployee.empDeptNo;
                ddlMgr.Text = objPREmployee.empMgrNo;
                ddlStatus.Text = objPREmployee.empStatus;
                chkActive.Checked = objPREmployee.empActiveYN == "Y" ? true : false;
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                PREmployeeManager objPREmployeeManager = new PREmployeeManager();
                PREmployee objPREmployee = new PREmployee();

                string empId=txtEmpNo.Text;
                string st = objPREmployeeManager.FnCheckpassword(empId);
                if (st == txtCurrentPswd.Text)
                {
                    string newPswd = txtNewPswd.Text;
                    int up=objPREmployeeManager.UpdatePassword(empId, newPswd);
                    if (up > 0)
                    {
                        string script = "Swal.fire({title: 'Success', text: 'Password Changed Successfully', icon: 'success'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                    }
                }
                else
                {
                    string script = "Swal.fire({title: 'Warning', text: 'Password Incorrect', icon: 'warning'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                }
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }

        
    }
}