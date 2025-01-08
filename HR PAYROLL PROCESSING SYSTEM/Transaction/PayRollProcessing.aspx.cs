using BussinessAccessLayer;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Transaction.PrEmployeeAttendence;
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
    public partial class PayRollProcessing : System.Web.UI.Page
    {
        PREmployeePayrollManager objPREmployeePayrollManager = new PREmployeePayrollManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DropDown();
                    loadgrid();
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
        public void loadgrid()
        {
            try
            {
                if (ddlMonth.SelectedValue != null && ddlYear.SelectedValue != null)
                {
                    grid1.DataSource = objPREmployeePayrollManager.LoadGridDetails(ddlMonth.SelectedValue, ddlYear.SelectedValue);
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

                SetDefaultMonthAndYear();
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        private void SetDefaultMonthAndYear()
        {

            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);

            //ListItem monthItem = ddlMonth.Items.FindByValue(currentMonth.ToString());
            //if (monthItem != null)
            //{
            //    ddlMonth.ClearSelection();
            //    monthItem.Selected = true;
            //}

            //ListItem yearItem = ddlYear.Items.FindByValue(currentYear.ToString());
            //if (yearItem != null)
            //{
            //    ddlYear.ClearSelection();
            //    yearItem.Selected = true;
            //}
        }
        protected void btnPayRollProcessing_Click(object sender, EventArgs e)
        {
            try
            {
                PREmployeePayroll objPREmployeePayroll = new PREmployeePayroll();
                PREmployeePayrollManager objPREmployeePayrollMgr = new PREmployeePayrollManager();
                PREmployeeManager objPrEmployeeMgr = new PREmployeeManager();
                PREmployeeAttendenceManager objPREmployeeAttendanceMgr = new PREmployeeAttendenceManager();

                int? year;
                int? month;
                string selectedMonth = ddlMonth.SelectedValue;
                string selectedYear = ddlYear.SelectedValue;
                string yymm = selectedYear + selectedMonth;
                string previousMonth;

                int? prevMonth = Convert.ToInt32(selectedMonth) - 1;
                int? yr = Convert.ToInt32(selectedYear);
                if (prevMonth == 0)
                {
                    prevMonth = 12;
                    yr = Convert.ToInt32(selectedYear) - 1;
                }

                if (selectedMonth != "10" || selectedMonth != "11" || selectedMonth != "12")
                {
                    previousMonth = "0" + Convert.ToString(prevMonth);
                }
                else
                {
                    previousMonth = Convert.ToString(prevMonth);
                }

                DateTime now = DateTime.Now;
                string yearmonth = Convert.ToString(now.Year) + Convert.ToString(now.Month);


                DataTable dt = objPrEmployeeMgr.CheckJoinDate();
                if (dt.Rows.Count > 0)
                {
                    year = !string.IsNullOrEmpty(dt.Rows[0]["EMP_JOIN_YEAR"].ToString()) ? Convert.ToInt32(dt.Rows[0]["EMP_JOIN_YEAR"]) : (int?)null;
                    month = !string.IsNullOrEmpty(dt.Rows[0]["EMP_JOIN_MONTH"].ToString()) ? Convert.ToInt32(dt.Rows[0]["EMP_JOIN_MONTH"]) : (int?)null;

                    if (Convert.ToInt32(ddlMonth.SelectedValue) == month && Convert.ToInt32(ddlYear.SelectedValue) == year)
                    {
                        int attendanceProcess = objPREmployeeAttendanceMgr.FnAttendanceProcessedForCurrMonth(Convert.ToString(yr), selectedMonth);

                        if (attendanceProcess > 0)
                        {
                            int result = objPREmployeePayrollMgr.FnProcessPayroll(yymm, "ADMIN");
                            if (result > 0)
                            {
                                loadgrid();
                                string script = "Swal.fire({title: 'Success', text: 'Salary Processed Successfully', icon: 'success'});";
                                ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                            }
                        }
                        else
                        {
                            string script = "Swal.fire({title: 'Warning', text: 'Attendance not processed for current month', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }


                    }
                    else if (Convert.ToInt32(ddlMonth.SelectedValue) > month && Convert.ToInt32(ddlYear.SelectedValue) >= year)
                    {
                        int attendanceProcess = objPREmployeeAttendanceMgr.FnAttendanceProcessedForCurrMonth(Convert.ToString(yr), selectedMonth);

                        if (attendanceProcess > 0)
                        {


                            int payProcess = objPREmployeePayrollMgr.FnIsPayrollProcessedOrNotForPrevMonth(Convert.ToString(yr) + previousMonth);

                            if (payProcess > 0)
                            {

                                int result = objPREmployeePayrollMgr.FnProcessPayroll(yymm, "ADMIN");
                                if (result > 0)
                                {
                                    loadgrid();
                                    string script = "Swal.fire({title: 'Success', text: 'Salary Processed Successfully', icon: 'success'});";
                                    ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                                }
                            }
                            else
                            {
                                string script = "Swal.fire({title: 'Warning', text: 'Salary not processed for previous month.', icon: 'warning'});";
                                ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                            }
                        }
                        else
                        {
                            string script = "Swal.fire({title: 'Warning', text: 'Attendance not processed for current month', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                    }

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


        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int? year;
                int? month;


                PREmployeeManager objPrEmployeeMgr = new PREmployeeManager();
                if (ddlMonth.SelectedValue != null && ddlYear.SelectedValue != null)
                {
                    DataTable dt = objPrEmployeeMgr.CheckJoinDate();
                    if (dt.Rows.Count > 0)
                    {
                        year = !string.IsNullOrEmpty(dt.Rows[0]["EMP_JOIN_YEAR"].ToString()) ? Convert.ToInt32(dt.Rows[0]["EMP_JOIN_YEAR"]) : (int?)null;
                        month = !string.IsNullOrEmpty(dt.Rows[0]["EMP_JOIN_MONTH"].ToString()) ? Convert.ToInt32(dt.Rows[0]["EMP_JOIN_MONTH"]) : (int?)null;

                        if (Convert.ToInt32(ddlMonth.SelectedValue) < month && Convert.ToInt32(ddlYear.SelectedValue) <= year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'No Employee joined in the selected month and year', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);

                        }
                        else if (Convert.ToInt32(ddlMonth.SelectedValue) > DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedValue) >= DateTime.Now.Year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'Future Payroll processing is not allowed', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        else if (Convert.ToInt32(ddlMonth.SelectedValue) >= DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedValue) > DateTime.Now.Year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'Future Payroll processing is not allowed', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        else if (Convert.ToInt32(ddlMonth.SelectedValue) < DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedValue) > DateTime.Now.Year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'Future Payroll processing is not allowed', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        else if (Convert.ToInt32(ddlMonth.SelectedValue) >= DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedValue) < DateTime.Now.Year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'No Employee joined in the selected month and year', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        else if (Convert.ToInt32(ddlMonth.SelectedValue) < DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedValue) < DateTime.Now.Year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'No Employee joined in the selected month and year', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        else if (Convert.ToInt32(ddlMonth.SelectedValue) == DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedValue) == DateTime.Now.Year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'Payroll processing is not allowed for current month', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        loadgrid();
                    }
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

        protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string selectedMonth = ddlMonth.SelectedValue;
            string selectedYear = ddlYear.SelectedValue;
            string eid = grid1.DataKeys[e.NewEditIndex].Values["PR_EMP_NO"].ToString();
            string yymm = selectedYear + selectedMonth;
            string url = "PaySlip.aspx?yymm=" + yymm + "&eid=" + eid;
            Response.Redirect(url);
        }
        protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grid1.PageIndex = e.NewPageIndex;
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

    }
}