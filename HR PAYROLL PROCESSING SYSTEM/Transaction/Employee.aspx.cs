using BussinessAccessLayer;
using BussinessAccessLayer.Master.DepartmentMaster;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Master.UserMaster;
using BussinessAccessLayer.Transaction.PREmployee;
using BussinessAccessLayer.Transaction.PrEmployeeAttendence;
using BussinessAccessLayer.Transaction.PrEmployeeHR;
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
    public partial class Employee : System.Web.UI.Page
    {
        UserMasterManger objusermanager = new UserMasterManger();
        protected void Page_Load(object sender, EventArgs e)
        {
           

            txtDOB.Attributes.Add("readonly", "readonly");
            txtJoinDate.Attributes.Add("readonly", "readonly");
            container2.Visible = false;
            if (!IsPostBack)
            {
                DropDown();
                string pEmpNo = Request.QueryString["pEmpNo"];
                if (!string.IsNullOrEmpty(pEmpNo))
                {
                    PREmployeeHRManager objobjPREmpHRMgr = new PREmployeeHRManager();
                    int count = objobjPREmpHRMgr.FnGetEmployeeHRCount(pEmpNo);
                    if (count == 1)
                    {
                        Div2.Visible = true;
                        btnAllowance.Visible = false;
                    }
                    else
                    {
                        Div2.Visible = false;
                        btnAllowance.Visible = true;

                    }

                    btnSave.Text = "Update";
                    FnFillEmployee(pEmpNo);

                    btnSave.Visible = true;
                    loadgrid();

                    container2.Visible = false;
                    FnFillEmployeeHR(pEmpNo);
                    btnSubmit.Text = "Update";
                }

                loadgrid();
            }
        }
        public void loadgrid()
        {
            try
            {
                PREmployeeHRManager objPREmpHrManager = new PREmployeeHRManager();
                DataTable dt = objPREmpHrManager.LoadGridDetails(txtENo.Text);
                grid1.DataSource = dt;
                grid1.DataBind();
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PREmployee objPREmployee = new PREmployee();
                PREmployeeManager objPREmployeeManager = new PREmployeeManager();
                PREmployeeAttendenceManager objPREmployeeAttendanceMgr = new PREmployeeAttendenceManager();
                PREmployeeAttendence objPrEmployeeAttendence = new PREmployeeAttendence();

                try
                {
                    if (btnSave.Text == "Save")
                    {
                        objPREmployee.empNO = objPREmployeeManager.Funct();
                        objPREmployee.empName = txtEmpName.Text;
                        objPREmployee.empDOB = Convert.ToDateTime(txtDOB.Text);
                        objPREmployee.empJoinDate = Convert.ToDateTime(txtJoinDate.Text);
                        //objPREmployee.empSalary = Convert.ToDouble(txtSalary.Text);
                        objPREmployee.empDeptNo = ddlDept.SelectedValue;
                        objPREmployee.empMgrNo = ddlMgr.SelectedValue;
                        objPREmployee.empStatus = ddlStatus.SelectedValue;
                        objPREmployee.empActiveYN = chkActive.Checked ? "Y" : "N";



                        int result = objPREmployeeManager.InsertEmployeeDetails(objPREmployee);
                        if (result > 0)
                        {
                            txtENo.Text = objPREmployee.empNO.ToString();
                            UserMasterEntity objusermaster = new UserMasterEntity();
                            objusermaster.userId = txtENo.Text;
                            objusermaster.userName = txtEmpName.Text;
                            objusermaster.userType = "USR2";
                            objusermaster.userActiveYN = chkActive.Checked ? "Y" : "N";

                            int rslt = objusermanager.InsertUserMasterEmployeeDetails(objusermaster);
                            if (rslt > 0)
                            {
                                //string script = "Swal.fire({title: 'Success', text: 'Yeah', icon: 'success'});";
                                //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                            }
                            DateTime empJoinDate = Convert.ToDateTime(txtJoinDate.Text);

                            string selectedYear = Convert.ToString(empJoinDate.Year);
                            string selectedMonth = Convert.ToString(empJoinDate.Month);
                            string selectedDay = Convert.ToString(empJoinDate.Day);


                            int year = int.Parse(selectedYear);
                            int month = int.Parse(selectedMonth);
                            int day = int.Parse(selectedDay);

                            int daysInMonth = GetDaysInMonth(year, month);
                            // Session["daysInMonth"] = daysInMonth;
                            int prsntdays = daysInMonth - day;

                            int processedCount = objPREmployeeAttendanceMgr.FnAttendanceProcessedOrNot(selectedYear, selectedMonth);
                            if (processedCount > 0)
                            {
                                objPrEmployeeAttendence.attEmpNO = objPREmployee.empNO;
                                objPrEmployeeAttendence.attYYYYMM = selectedYear + selectedMonth;
                                objPrEmployeeAttendence.attDaysPresent = prsntdays;
                                objPrEmployeeAttendence.attDaysAbsent = day;
                                int st = objPREmployeeAttendanceMgr.InsertAttendanceNewEmp(objPrEmployeeAttendence);
                                if (st > 0)
                                {
                                    string script1 = $@"
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'Employee Created Successfully.',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'Employee.aspx?pEmpNo={objPREmployee.empNO}';
                                }}
                                }});";
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script1, true);
                                }

                            }
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        objPREmployee.empNO = txtENo.Text;
                        objPREmployee.empName = txtEmpName.Text;
                        objPREmployee.empDOB = Convert.ToDateTime(txtDOB.Text);
                        objPREmployee.empJoinDate = Convert.ToDateTime(txtJoinDate.Text);
                        objPREmployee.empSalary = !string.IsNullOrEmpty(txtSalary.Text) ? Convert.ToDouble(txtSalary.Text) : (double?)null;
                        objPREmployee.empDeptNo = ddlDept.SelectedValue;
                        objPREmployee.empMgrNo = ddlMgr.SelectedValue;
                        objPREmployee.empStatus = ddlStatus.SelectedValue;
                        objPREmployee.empActiveYN = chkActive.Checked ? "Y" : "N";

                        string empNo;
                        int st = objPREmployeeManager.UpdateOption(objPREmployee, out empNo);
                        if (st > 0)
                        {
                            string script = $@"
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'Employee Updated Successfully',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'Employee.aspx?pEmpNo={empNo}';
                                }}
                                }});";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);
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
        protected void btnAllowance_Click(object sender, EventArgs e)
        {
            container2.Visible = true;
            //txtEmpNo.Text = txtENo.Text;
            btnAllowance.Visible = false;
            btnSubmit.Visible = true;
            btnSubmit.Text = "Submit";
            Div2.Visible = false;

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

                ddlMgr.DataSource = objcodesmaster.FnDropDownManager();
                ddlMgr.DataTextField = "EMP_NAME";
                ddlMgr.DataValueField = "EMP_NO";
                ddlMgr.DataBind();
                ddlMgr.Items.Insert(0, new ListItem("--Select--", "-1"));

                DepartmentMasterManager objDeptMaster = new DepartmentMasterManager();

                ddlDept.DataSource = objDeptMaster.DeptDropDowns();
                ddlDept.DataTextField = "DEPT_NAME";
                ddlDept.DataValueField = "DEPT_NO";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlDesignation.DataSource = objcodesmaster.FnDropDowns("DESIGNATION");
                ddlDesignation.DataTextField = "CM_DESC";
                ddlDesignation.DataValueField = "CM_CODE";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlGrade.DataSource = objcodesmaster.FnDropDowns("GRADE");
                ddlGrade.DataTextField = "CM_DESC";
                ddlGrade.DataValueField = "CM_CODE";
                ddlGrade.DataBind();
                ddlGrade.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            PREmployeeHR objPREmpHr = new PREmployeeHR();
            PREmployeeHRManager objPREmployeeHRManager = new PREmployeeHRManager();
            try
            {

                if (btnSubmit.Text == "Submit")
                {
                    // Div2.Visible = true;
                    objPREmpHr.ehEmpNO = txtENo.Text;
                    objPREmpHr.ehDesignation = ddlDesignation.Text;
                    objPREmpHr.ehGrade = ddlGrade.Text;
                    objPREmpHr.ehBasic = !string.IsNullOrEmpty(txtBasic.Text.ToString()) ? Convert.ToDouble(txtBasic.Text.ToString()) : (double?)null;
                    objPREmpHr.ehHra = !string.IsNullOrEmpty(txtHRA.Text.ToString()) ? Convert.ToDouble(txtHRA.Text.ToString()) : (double?)null;
                    objPREmpHr.ehConv = !string.IsNullOrEmpty(txtCONV.Text.ToString()) ? Convert.ToDouble(txtCONV.Text.ToString()) : (double?)null;
                    objPREmpHr.ehDa = !string.IsNullOrEmpty(txtDA.Text.ToString()) ? Convert.ToDouble(txtDA.Text.ToString()) : (double?)null;
                    objPREmpHr.ehTds = !string.IsNullOrEmpty(txtTDS.Text.ToString()) ? Convert.ToDouble(txtTDS.Text.ToString()) : (double?)null;
                    objPREmpHr.ehEsi = !string.IsNullOrEmpty(txtESI.Text.ToString()) ? Convert.ToDouble(txtESI.Text.ToString()) : (double?)null;
                    objPREmpHr.ehCrBy = "ADMIN";
                    objPREmpHr.ehCrDt = !string.IsNullOrEmpty(DateTime.Now.ToString()) ? Convert.ToDateTime(DateTime.Now.ToString()) : (DateTime?)null;

                    PREmployeeHRManager objPREmpHrManager = new PREmployeeHRManager();
                    PREmployee objPREmployee = new PREmployee();
                    objPREmployee.empNO = txtENo.Text;
                    string pEmpNo = objPREmployee.empNO;
                    (int updateStatus, double salary) = objPREmpHrManager.InsertPrEmpHR(objPREmpHr, out pEmpNo);
                    if (updateStatus > 0)
                    {
                        txtSalary.Text = salary.ToString();
                        grid1.Visible = true;
                        string script = $@"
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'Allowance added Successfully',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'Employee.aspx?pEmpNo={pEmpNo}';
                                }}
                                }});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);
                    }


                }
                else if (btnSubmit.Text == "Update")
                {
                    btnAllowance.Visible = false;
                    container2.Visible = false;
                    objPREmpHr.ehEmpNO = txtENo.Text;
                    objPREmpHr.ehDesignation = ddlDesignation.Text;
                    objPREmpHr.ehGrade = ddlGrade.Text;
                    objPREmpHr.ehBasic = !string.IsNullOrEmpty(txtBasic.Text.ToString()) ? Convert.ToDouble(txtBasic.Text.ToString()) : (double?)null;
                    objPREmpHr.ehHra = !string.IsNullOrEmpty(txtHRA.Text.ToString()) ? Convert.ToDouble(txtHRA.Text.ToString()) : (double?)null;
                    objPREmpHr.ehConv = !string.IsNullOrEmpty(txtCONV.Text.ToString()) ? Convert.ToDouble(txtCONV.Text.ToString()) : (double?)null;
                    objPREmpHr.ehDa = !string.IsNullOrEmpty(txtDA.Text.ToString()) ? Convert.ToDouble(txtDA.Text.ToString()) : (double?)null;
                    objPREmpHr.ehTds = !string.IsNullOrEmpty(txtTDS.Text.ToString()) ? Convert.ToDouble(txtTDS.Text.ToString()) : (double?)null;
                    objPREmpHr.ehEsi = !string.IsNullOrEmpty(txtESI.Text.ToString()) ? Convert.ToDouble(txtESI.Text.ToString()) : (double?)null;
                    objPREmpHr.ehUpBy = "ADMIN";
                    objPREmpHr.ehUpDt = !string.IsNullOrEmpty(DateTime.Now.ToString()) ? Convert.ToDateTime(DateTime.Now.ToString()) : (DateTime?)null;

                    container2.Visible = true;
                    PREmployee objPREmployee = new PREmployee();
                    objPREmployee.empNO = txtENo.Text;
                    string pEmpNo = objPREmployee.empNO;
                    (int updateStatus, double salary) = objPREmployeeHRManager.UpdatePREmployeeHR(objPREmpHr, out pEmpNo);
                    if (updateStatus > 0)
                    {
                        txtSalary.Text = salary.ToString();

                        string script = $@"
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'Allowance updated Successfully',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'Employee.aspx?pEmpNo={pEmpNo}';
                                }}
                                }});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Update";
            grid1.Visible = false;
            Div2.Visible = false;
            container2.Visible = true;
        }
        protected void FnFillEmployee(string pEmpNo)
        {
            PREmployee objPREmployee = new PREmployee();
            PREmployeeManager objobjPREmployeeManager = new PREmployeeManager();
            objPREmployee = objobjPREmployeeManager.FnGetEmployee(pEmpNo);
            txtENo.Text = objPREmployee.empNO;
            txtEmpName.Text = objPREmployee.empName;
            txtDOB.Text = objPREmployee.empDOB.Value.ToString("dd-MM-yyyy");
            txtJoinDate.Text = objPREmployee.empJoinDate.Value.ToString("dd-MM-yyyy");
            txtSalary.Text = Convert.ToString(objPREmployee.empSalary);
            ddlDept.SelectedValue = objPREmployee.empDeptNo;
            ddlMgr.SelectedValue = objPREmployee.empMgrNo;
            ddlStatus.SelectedValue = objPREmployee.empStatus;
            chkActive.Checked = objPREmployee.empActiveYN == "Y" ? true : false;
        }
        protected void FnFillEmployeeHR(string pEmpNo)
        {
            PREmployee objPREmployee = new PREmployee();
            PREmployeeHR objPREmployeeHr = new PREmployeeHR();
            PREmployeeHRManager objobjPREmpHRMgr = new PREmployeeHRManager();
            objPREmployeeHr = objobjPREmpHRMgr.FnGetEmployeeHR(pEmpNo);
            //txtEmpNo.Text = objPREmployeeHr.ehEmpNO;
            ddlDesignation.Text = objPREmployeeHr.ehDesignation;
            ddlGrade.Text = objPREmployeeHr.ehGrade;
            txtBasic.Text = Convert.ToString(objPREmployeeHr.ehBasic);
            txtHRA.Text = Convert.ToString(objPREmployeeHr.ehHra);
            txtCONV.Text = Convert.ToString(objPREmployeeHr.ehConv);
            txtDA.Text = Convert.ToString(objPREmployeeHr.ehDa);
            txtTDS.Text = Convert.ToString(objPREmployeeHr.ehTds);
            txtESI.Text = Convert.ToString(objPREmployeeHr.ehEsi);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/EmployeeList.aspx");
        }
    }
}