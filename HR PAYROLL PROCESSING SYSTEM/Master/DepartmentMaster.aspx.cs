using BussinessAccessLayer.Master.DepartmentMaster;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class DepartmentMaster : System.Web.UI.Page
    {
        DepartmentMasterManager objDeptMasterMgr = new DepartmentMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string DeptNo = Request.QueryString["pDeptNo"];

                if (!string.IsNullOrEmpty(DeptNo))
                {
                    txtDeptNo.ReadOnly = true;
                    Save.Text = "Update";
                    FnFillDeptMaster(DeptNo);
                }
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            DepartmentMasterEntity objDepartmentMaster = new DepartmentMasterEntity();
            objDepartmentMaster.deptNo = txtDeptNo.Text;
            objDepartmentMaster.deptName = txtDeptName.Text;
            try
            {
                if (Save.Text == "Save")
                {
                                    
                    if (objDeptMasterMgr.IsDepartmentMasterExist(objDepartmentMaster) > 0)
                    {
                        string script = "Swal.fire({title: 'ERROR', text: 'Department No already exists!', icon: 'warning'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        return;
                    }
                    else
                    {
                        string deptno;
                        int result = objDeptMasterMgr.InsertDepartmentMasterDetails(objDepartmentMaster, out deptno);
                        if (result > 0)
                        {
                            string script = $@"
                                 console.log('Executing Swal.fire');
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'Department Master Created Successfully.',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'DepartmentMaster.aspx?pDeptNo={deptno}';
                                }}
                                }});";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);
                            //Response.Redirect($"DepartmentMaster.aspx?pCode={codes}");
                            //string script = "Swal.fire({title: 'Success', text: 'Department Master Created Successfully', icon: 'success'});";
                            //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                    }
                }
                else if(Save.Text == "Update")
                {
                    string codes;
                    int result = objDeptMasterMgr.UpdateOption(objDepartmentMaster, out codes);
                    if (result > 0)
                    {
                        string script = $@"
                                 console.log('Executing Swal.fire');
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'Department Master Updated Successfully.',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'DepartmentMaster.aspx?pCode={codes}';
                                }}
                                }});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);
                        //Response.Redirect($"DepartmentMaster.aspx?pCode={codes}");
                        //string script = "Swal.fire({title: 'Success', text: 'CodeMaster Updated Successfully', icon: 'success'});";
                        //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
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


        protected void FnFillDeptMaster(string pCode)
        {
            DepartmentMasterEntity objDepartmentMaster = new DepartmentMasterEntity();
            objDepartmentMaster = objDeptMasterMgr.FnGetCodesMaster(pCode);
            txtDeptNo.Text = objDepartmentMaster.deptNo;
            txtDeptName.Text = objDepartmentMaster.deptName;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Master/DepartmentMasterListing.aspx");
        }
    }
}