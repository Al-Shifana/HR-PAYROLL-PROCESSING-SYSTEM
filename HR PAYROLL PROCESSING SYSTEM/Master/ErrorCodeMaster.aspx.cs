using System;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessLayer.Master.CodeMaster;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class ErrorCodeMaster : System.Web.UI.Page
    {
        ErrorCodeMasterManager objErrorCodeManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = Request.QueryString["pCode"];

                if (!string.IsNullOrEmpty(code))
                {
                    txtErrCode.ReadOnly = true;
                    btnSave.Text = "Update";
                    FnFillErrorCodeMaster(code);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {           
            try
            {
                ErrorCodeMasterEntity objErrorCodeMaster = new ErrorCodeMasterEntity();

                objErrorCodeMaster.errCode = txtErrCode.Text;
                objErrorCodeMaster.errType = txtErrType.Text;
                objErrorCodeMaster.errDesc = txtErrDesc.Text;
                if (btnSave.Text == "Save")
                {
                    if (objErrorCodeManager.IsErrorCodeMasterExist(objErrorCodeMaster) > 0)
                    {
                        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                        string errTitle = objErrorCodeMasterManager.FnFetchError("112");
                        string errmsg = "Code already exist";
                        string script = "Swal.fire({title:" + "'" + errTitle + "'" + ", text:" + "'" + errmsg + "'" + ", icon: 'warning'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                    }
                    else
                    {
                        string codes;
                        int result = objErrorCodeManager.InsertErrorCodeMasterDetails(objErrorCodeMaster,out codes);
                        if (result > 0)
                        {
                            string script = $@"
                           console.log('Executing Swal.fire');
                           Swal.fire({{
                           title: 'Success!',
                           text: 'Inserted successfully.',
                           icon: 'success',
                           confirmButtonText: 'OK'
                           }}).then((result) => {{
                           if (result.isConfirmed) {{
                          window.location.href = 'ErrorCodeMaster.aspx?pCode={codes}';
                          }}
                          }});";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);


                           //Response.Redirect($"ErrorCodeMaster.aspx?pCode={codes}");
                           //string script = "Swal.fire({title: 'Success', text: 'ErrorCode Created Successfully', icon: 'success'});";
                           //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    string codes;
                    int result = objErrorCodeManager.UpdateOption(objErrorCodeMaster, out codes);
                    if (result > 0)
                    {
                        string script = $@"
                           console.log('Executing Swal.fire');
                           Swal.fire({{
                           title: 'Success!',
                           text: 'ErrorCode Updated Successfully.',
                           icon: 'success',
                           confirmButtonText: 'OK'
                           }}).then((result) => {{
                           if (result.isConfirmed) {{
                          window.location.href = 'ErrorCodeMaster.aspx?pCode={codes}';
                          }}
                          }});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);


                        //string script = "Swal.fire({title: 'Success', text: 'ErrorCode Updated Successfully', icon: 'success'});";
                        //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        ////Response.Redirect($"ErrorCodeMaster.aspx?pCode={codes}");
                        
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
        protected void FnFillErrorCodeMaster(string pCode)
        {
            ErrorCodeMasterEntity objErrCodeMasterEntity = new ErrorCodeMasterEntity();
            objErrCodeMasterEntity = objErrorCodeManager.FnGetCodesMaster(pCode);
            txtErrCode.Text = objErrCodeMasterEntity.errCode;
            txtErrType.Text = objErrCodeMasterEntity.errType;
            txtErrDesc.Text = objErrCodeMasterEntity.errDesc;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Master/ErrorCodeMasterListing.aspx");
        }
    }
}