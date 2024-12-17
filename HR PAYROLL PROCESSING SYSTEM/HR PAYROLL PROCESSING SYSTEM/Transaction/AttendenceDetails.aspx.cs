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
    public partial class AttendenceDetails : System.Web.UI.Page
    {
        PREmployeeManager objPrEmployee = new PREmployeeManager();
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
                    grid1.DataSource = objPrEmployee.GetAttendance(ddlMonth.SelectedValue, ddlYear.SelectedValue);
                    grid1.DataBind();
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

                SetCurrentMonthAndYear();
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        private void SetCurrentMonthAndYear()
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            ListItem monthItem = ddlMonth.Items.FindByValue(currentMonth.ToString());
            if (monthItem != null)
            {
                ddlMonth.ClearSelection();
                monthItem.Selected = true;
            }

            ListItem yearItem = ddlYear.Items.FindByValue(currentYear.ToString());
            if (yearItem != null)
            {
                ddlYear.ClearSelection();
                yearItem.Selected = true;
            }
        }
        protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string empNo = grid1.DataKeys[e.NewEditIndex].Values["EMP_NO"].ToString();
                string selectedMonth = ddlMonth.SelectedValue;
                string selectedYear = ddlYear.SelectedValue;
                string yymm = selectedYear + selectedMonth;
                int year = int.Parse(selectedYear);
                int month = int.Parse(selectedMonth);

                int daysInMonth = GetDaysInMonth(year, month);
                Session["daysInMonth"] = daysInMonth;

                Response.Redirect($"EmployeeAttendance.aspx?pEmpNo={empNo}&pYYYYMM={yymm}");
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        public int GetDaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }


        protected void btnPayRollProcessing_Click(object sender, EventArgs e)
        {
            try
            {
                PREmployeeAttendence objPREmployeeAttendence = new PREmployeeAttendence();
                PREmployeeAttendenceManager objPREmployeeAttendanceMgr = new PREmployeeAttendenceManager();
                PREmployeeManager objPrEmployeeMgr = new PREmployeeManager();
                PREmployeePayrollManager objPREmployeePayrollManager = new PREmployeePayrollManager();

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

                if (yymm == yearmonth)
                {
                    string script = "Swal.fire({title: 'Warning', text: 'Attendance cannot updated for current month', icon: 'warning'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                }
                else
                {
                    if (ddlMonth.SelectedValue != null && ddlYear.SelectedValue != null)
                    {


                        bool payrollProcess = objPREmployeePayrollManager.IsPayrollProcessed(selectedYear + selectedMonth);

                        if (payrollProcess == true)
                        {
                            string script = "Swal.fire({title: 'Warning', text: 'Cant process the attendance, Attendance already processed for the month.', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        else
                        {
                            DataTable dt = objPrEmployeeMgr.CheckJoinDate();
                            if (dt.Rows.Count > 0)
                            {
                                year = !string.IsNullOrEmpty(dt.Rows[0]["EMP_JOIN_YEAR"].ToString()) ? Convert.ToInt32(dt.Rows[0]["EMP_JOIN_YEAR"]) : (int?)null;
                                month = !string.IsNullOrEmpty(dt.Rows[0]["EMP_JOIN_MONTH"].ToString()) ? Convert.ToInt32(dt.Rows[0]["EMP_JOIN_MONTH"]) : (int?)null;

                                if (Convert.ToInt32(ddlMonth.SelectedValue) >= month && Convert.ToInt32(ddlYear.SelectedValue) >= year)
                                {

                                    int processedCount = objPREmployeeAttendanceMgr.FnAttendanceProcessedOrNot(Convert.ToString(yr), previousMonth);

                                    if (processedCount == 0)
                                    {
                                        if (yr == year && Convert.ToInt32(selectedMonth) == month)
                                        {
                                            int result = objPREmployeeAttendanceMgr.FnInsertAttendance(yymm, "ADMIN");
                                            if (result > 0)
                                            {
                                                loadgrid();
                                                string script = "Swal.fire({title: 'Success', text: 'Attendance of all Presentees Created Successfully', icon: 'success'});";
                                                ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                                            }
                                        }
                                        else if (yr > year)
                                        {
                                            string script = "Swal.fire({title: 'Warning', text: 'Attendance for the future year is not processed.', icon: 'warning'});";
                                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                                        }
                                        else
                                        {
                                            string script = "Swal.fire({title: 'Warning', text: 'Attendance for the previous month is not processed.', icon: 'warning'});";
                                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                                        }
                                    }
                                    else
                                    {                
                                        
                                        int result = objPREmployeeAttendanceMgr.FnInsertAttendance(yymm, "ADMIN");
                                        if (result > 0)
                                        {
                                            loadgrid();
                                            string script = "Swal.fire({title: 'Success', text: 'Attendance of all Presentees Updated Successfully', icon: 'success'});";
                                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                                        }
                                    }
                                }
                            }
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
        protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
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
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'No Employee joined in the selected month and year', icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);

                        }
                        else if (Convert.ToInt32(ddlMonth.SelectedValue) > DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedValue) >= DateTime.Now.Year)
                        {
                            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
                            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                            string script = "Swal.fire({title: 'Warning', text: 'Future attendance processing is not allowed', icon: 'warning'});";
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