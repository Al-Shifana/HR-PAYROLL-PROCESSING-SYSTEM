using BussinessAccessLayer;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Transaction.PREmployeePayroll;
using BussinessLayer.Master.CodeMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Transaction
{
    public partial class SlipGeneration : System.Web.UI.Page
    {
        PREmployeePayrollManager objPREmployeePayrollManager = new PREmployeePayrollManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string empId = Request.QueryString["pEmpNo"];
                Session["uid"] = empId;
                if (!string.IsNullOrEmpty(empId))
                {
                    loadgrid();
                }
                DropDown();
            }
        }
        public void DropDown()
        {
            try
            {
                CodeMasterManager objcodesmaster = new CodeMasterManager();

                ddlMonth.DataSource = objcodesmaster.FnDropDowns("MONTH");
                ddlMonth.DataTextField = "CM_DESC";
                ddlMonth.DataValueField = "CM_CODE";
                ddlMonth.DataBind();
                ddlMonth.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlYear.DataSource = objcodesmaster.FnDropDowns("YEAR");
                ddlYear.DataTextField = "CM_DESC";
                ddlYear.DataValueField = "CM_CODE";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("--Select--", "-1"));

               // SetDefaultMonthAndYear();
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadgrid();
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        public void loadgrid()
        {
            try
            {
                if (ddlMonth.SelectedValue != null && ddlYear.SelectedValue != null)
                {
                    grid1.DataSource = objPREmployeePayrollManager.LoadGridforSlip(ddlMonth.SelectedValue, ddlYear.SelectedValue,Convert.ToString(Session["uid"]));
                    grid1.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string selectedMonth = ddlMonth.SelectedValue;
            string selectedYear = ddlYear.SelectedValue;
            string yymm = selectedYear + selectedMonth;
            Session["yymm"] = grid1.DataKeys[e.NewEditIndex].Values["PR_YYYMM"].ToString();
            Session["eid"] = grid1.DataKeys[e.NewEditIndex].Values["PR_EMP_NO"].ToString();
            Response.Redirect("PaySlip.aspx");
        }
    }
}