using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Transaction.PrEmployeeAttendence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Transaction
{
    public partial class EmployeeAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTotal.Attributes.Add("readonly", "readonly");
            txtPresentDays.Attributes.Add("readonly", "readonly");
            txtEmpId.Text = Request.QueryString["pEmpNo"];
            int daysInMonth = (int)Session["daysInMonth"];
            txtTotal.Text = daysInMonth.ToString();
            if (!IsPostBack)
            {
                string empNo = Request.QueryString["pEmpNo"];
                string empYYYYMM = Request.QueryString["pYYYYMM"];
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                PREmployeeAttendence objPrEmployeeAttendence = new PREmployeeAttendence();
                PREmployeeAttendenceManager objPREmployeeAttendenceManager = new PREmployeeAttendenceManager();

                objPrEmployeeAttendence.attEmpNO = Request.QueryString["pEmpNo"];
                objPrEmployeeAttendence.attDaysPresent = !string.IsNullOrEmpty(txtPresentDays.Text) ? Convert.ToInt32(txtPresentDays.Text) : (int?)null;
                objPrEmployeeAttendence.attDaysAbsent = !string.IsNullOrEmpty(txtAbsentDays.Text) ? Convert.ToInt32(txtAbsentDays.Text) : (int?)null;
                objPrEmployeeAttendence.attYYYYMM = Request.QueryString["pYYYYMM"];

                DateTime now = DateTime.Now;
                string yearmonth = Convert.ToString(now.Year) + Convert.ToString(now.Month);

                if (objPrEmployeeAttendence.attYYYYMM == yearmonth)
                {
                    string script = "Swal.fire({title: 'Warning', text: 'Attendance Cannot updated for current month', icon: 'warning'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                }
                else
                {
                    bool attpresent = objPREmployeeAttendenceManager.IsAttendanceFound(objPrEmployeeAttendence);
                    if (attpresent == true)
                    {
                        string empId, yyyymm;
                        int result = objPREmployeeAttendenceManager.UpdateAttendanceDetails(objPrEmployeeAttendence, out empId, out yyyymm);
                        if (result > 0)
                        {
                            string script = "Swal.fire({title: 'Success', text: 'Attendance Updated Successfully', icon: 'success'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                    }
                    else
                    {
                        string empId, yyyymm;
                        int result = objPREmployeeAttendenceManager.InsertAttendanceDetails(objPrEmployeeAttendence, out empId, out yyyymm);
                        if (result > 0)
                        {
                            //Response.Redirect($"EmployeeAttendance.aspx?pEmpNo={empId}&pYYYYMM={yyyymm}");
                            string script = "Swal.fire({title: 'Success', text: 'Attendance Created Successfully', icon: 'success'});";
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
        protected void txtAbsentDays_TextChanged(object sender, EventArgs e)
        {
            int daysInMonth = (int)Session["daysInMonth"];
            int absentDays = Convert.ToInt32(txtAbsentDays.Text);
            txtPresentDays.Text = (daysInMonth - absentDays).ToString();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/AttendenceDetails.aspx");
        }
    }
}