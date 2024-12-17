using BussinessAccessLayer.Master.CodeMaster;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessLayer.Master.CodeMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class CodesMaster : System.Web.UI.Page
    {
        CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = Request.QueryString["pCode"];
                string type = Request.QueryString["pType"];

                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(type))
                {
                    btnSave.Text = "Update";
                    txtCmCode.ReadOnly = true;
                    FnFillCodesMaster(code, type);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CodeMasterEntity codemasterentity = new CodeMasterEntity();

                codemasterentity.cmCode = txtCmCode.Text;
                codemasterentity.cmType = txtCmType.Text;
                codemasterentity.cmDesc = txtCmDesc.Text;
                codemasterentity.cmValue = string.IsNullOrEmpty(txtCmValue.Text) ? 0 : Convert.ToInt32(txtCmValue.Text);
                codemasterentity.cmActiveYN = chkActive.Checked ? "Y" : "N";

                try
                {
                    if (btnSave.Text == "Save")
                    {
                        if (objCodeMasterManager.IsCodeMasterExist(codemasterentity) > 0)
                        {
                            ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                            string errTitle = objErrorCodeMasterManager.FnFetchError("112");
                            string errmsg= "Code already exist";
                            string script = "Swal.fire({title:" + "'" + errTitle + "'" + ", text:" + "'" + errmsg + "'" + ", icon: 'warning'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                        else
                        {
                            string codes, type;
                            int result = objCodeMasterManager.InsertCodeMasterDetails(codemasterentity, out codes, out type);
                            if (result > 0)
                            {
                                string script = $@"
                                 console.log('Executing Swal.fire');
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'CodeMaster Created successfully.',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'CodesMaster.aspx?pCode={codes}&pType={type}';
                                }}
                                }});";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);

                                //Response.Redirect($"CodesMaster.aspx?pCode={codes}&pType={type}");
                                //ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                                //string errTitle = objErrorCodeMasterManager.FnFetchError("113");
                                //string script = "Swal.fire({title:" + "'" + errTitle + "'" + ", text:" + "'" + errTitle + "'" + ", icon: 'success'});";
                                //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                            }
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        string codes, type;
                        int result = objCodeMasterManager.UpdateOption(codemasterentity, out codes, out type);
                        if (result > 0)
                        {
                            string script = $@"
                           console.log('Executing Swal.fire');
                           Swal.fire({{
                           title: 'Success!',
                           text: 'Updated successfully.',
                           icon: 'success',
                           confirmButtonText: 'OK'
                           }}).then((result) => {{
                           if (result.isConfirmed) {{
                          window.location.href = 'CodesMaster.aspx?pCode={codes}&pType={type}';
                          }}
                          }});";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);



                            
                            //Response.Redirect($"CodesMaster.aspx?pCode={codes}&pType={type}");
                            //ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                            //string errTitle = objErrorCodeMasterManager.FnFetchError("114");
                            //string script = "Swal.fire({title:" + "'" + errTitle + "'" + ", text:" + "'" + errTitle + "'" + ", icon: 'success'});";
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
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void FnFillCodesMaster(string pCode, string pType)
        {
            CodeMasterEntity objCodeMasterEntity = new CodeMasterEntity();
            objCodeMasterEntity = objCodeMasterManager.FnGetCodesMaster(pCode, pType);
            txtCmCode.Text = objCodeMasterEntity.cmCode;
            txtCmType.Text = objCodeMasterEntity.cmType;
            txtCmValue.Text = !string.IsNullOrEmpty(objCodeMasterEntity.cmValue.ToString()) ? objCodeMasterEntity.cmValue.ToString() : string.Empty;
            txtCmDesc.Text = objCodeMasterEntity.cmDesc;
            chkActive.Checked = objCodeMasterEntity.cmActiveYN == "Y" ? true : false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Master/CodesMasterListing.aspx");
        }
    }
}