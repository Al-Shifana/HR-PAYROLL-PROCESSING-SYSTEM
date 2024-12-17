using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Master.UserMaster;
using BussinessLayer.Master.CodeMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class UserMaster : System.Web.UI.Page
    {
        UserMasterManger objUserMasterManager = new UserMasterManger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["pId"];

                if (!string.IsNullOrEmpty(id))
                {
                    txtUserId.ReadOnly = true;
                    btnSave.Text = "Update";
                    FnFillUserMaster(id);
                }
                DropDown();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserMasterEntity objusermaster = new UserMasterEntity();

            objusermaster.userId = txtUserId.Text;
            objusermaster.userName = txtUsername.Text;
            objusermaster.userPassword = txtPswd.Text;
            objusermaster.userType = ddlUserType.Text;
            objusermaster.userActiveYN = chkActive.Checked ? "Y" : "N";

            try
            {
                if (btnSave.Text == "Save")
                {
                    if (objUserMasterManager.IsUserMasterExist(txtUserId.Text) > 0)
                    {
                        string script = "Swal.fire({title: 'ERROR', text: 'Code and type already exists!', icon: 'warning'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        return;
                    }
                    else
                    {
                        string userId;
                        int result = objUserMasterManager.InsertUserMasterDetails(objusermaster, out userId);
                        if (result > 0)
                        {
                            string script = $@"
                                 console.log('Executing Swal.fire');
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'User Created Successfully.',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'UserMaster.aspx?pId={userId}';
                                }}
                                }});";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);

                            //string script = "Swal.fire({title: 'Success', text: 'User Created Successfully!', icon: 'success'});";
                            //ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    string userId;
                    int result = objUserMasterManager.UpdateOption(objusermaster, out userId);
                    if (result > 0)
                    {

                        string script = $@"
                                 console.log('Executing Swal.fire');
                                 Swal.fire({{
                                 title: 'Success!',
                                 text: 'User Master Updated Successfully.',
                                 icon: 'success',
                                 confirmButtonText: 'OK'
                                }}).then((result) => {{
                                 if (result.isConfirmed) {{
                                 window.location.href = 'UserMaster.aspx?pId={userId}';
                                }}
                                }});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SwalFireScript", script, true);
                        //Response.Redirect($"UserMaster.aspx?pId={userId}");
                        //string script = "Swal.fire({title: 'Success', text: 'User Master Updated Successfully', icon: 'success'});";
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
        public void DropDown()
        {
            try
            {
                CodeMasterManager objcodesmaster = new CodeMasterManager();

                ddlUserType.DataSource = objcodesmaster.FnDropDowns("USER TYPE");
                ddlUserType.DataTextField = "CM_DESC";
                ddlUserType.DataValueField = "CM_CODE";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("--Select--", "-1"));

            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }    
        protected void FnFillUserMaster(string pId)
        {
            UserMasterEntity objUserMasterEntity = new UserMasterEntity();
            objUserMasterEntity = objUserMasterManager.FnGetUserMaster(pId);
            txtUserId.Text = objUserMasterEntity.userId;
            txtUsername.Text = objUserMasterEntity.userName;
            txtPswd.Text = objUserMasterEntity.userPassword;
            ddlUserType.Text = objUserMasterEntity.userType;
            chkActive.Checked = objUserMasterEntity.userActiveYN == "Y" ? true : false;
        }
        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Master/UserMasterListing.aspx");
        }
    }
}
