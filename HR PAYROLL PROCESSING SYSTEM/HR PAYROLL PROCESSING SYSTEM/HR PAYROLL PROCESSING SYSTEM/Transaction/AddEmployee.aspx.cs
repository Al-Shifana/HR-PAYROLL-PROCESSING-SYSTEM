using BussinessAccessLayer.Master.CodeMaster;
using BussinessAccessLayer.Master.DepartmentMaster;
using BussinessLayer.Master.CodeMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Transaction
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDown();
            DropDownDept();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnAllowance.Visible = true;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void btnAllowance_Click(object sender, EventArgs e)
        {

        }
        public void DropDown()
        {
            try
            {
               CodeMasterManager objcodesmaster = new CodeMasterManager();

                ddlStatus.DataSource = objcodesmaster.FnDropDowns("STATUS");
                ddlStatus.DataTextField = "CM_DESC";
                ddlStatus.DataValueField = "CM_CODE";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlMgr.DataSource = objcodesmaster.FnDropDowns("MANAGER");
                ddlMgr.DataTextField = "CM_DESC";
                ddlMgr.DataValueField = "CM_CODE";
                ddlMgr.DataBind();
                ddlMgr.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;

                //ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                //string errTitle = objErrorCodeMasterManager.FnFetchError("112");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('" + errTitle + "','" + ex.Message + "' ,'error')", true);

            }
        }
        public void DropDownDept()
        {
            try
            {
                DepartmentMasterManager objDeptMaster = new DepartmentMasterManager();

                ddlDept.DataSource = objDeptMaster.DeptDropDowns();
                ddlDept.DataTextField = "DEPT_NAME";
                ddlDept.DataValueField = "DEPT_NO";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;

                //ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                //string errTitle = objErrorCodeMasterManager.FnFetchError("112");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('" + errTitle + "','" + ex.Message + "' ,'error')", true);

            }
        }
    }
}