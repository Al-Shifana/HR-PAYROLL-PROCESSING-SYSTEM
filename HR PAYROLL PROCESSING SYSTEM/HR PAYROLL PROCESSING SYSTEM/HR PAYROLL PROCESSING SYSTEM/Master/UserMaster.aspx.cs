using BussinessAccessLayer.Master.UserMaster;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class UserMaster : System.Web.UI.Page
    {
        UserMasterManger objusermanager = new UserMasterManger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadgrid();
            }
        }
        public void loadgrid()
        {
            try
            {
                DataTable dt = objusermanager.LoadGridDetails();
                grid1.DataSource = dt;
                grid1.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserMasterEntity objusermaster = new UserMasterEntity();

            objusermaster.userId = txtUserId.Text;
            objusermaster.userName = txtUsername.Text;
            objusermaster.userPassword = txtPswd.Text;
            objusermaster.userType = txtType.Text;
            objusermaster.userActiveYN = chkActive.Checked ? "Y" : "N";

            try
            {
                if (objusermanager.IsUserMasterExist(txtUserId.Text) > 0)
                {
                    string script = "Swal.fire({title: 'ERROR', text: 'Code and type already exists!', icon: 'warning'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                    return;
                }
                else
                {
                    int result = objusermanager.InsertUserMasterDetails(objusermaster);
                    if (result > 0)
                    {
                        string script = "Swal.fire({title: 'Success', text: 'Yeah', icon: 'success'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                            loadgrid();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                string script = "Swal.fire({title: 'ERROR', text: 'OOPS!', icon: 'warning'});";
                ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
            }

        }

        protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grid1.EditIndex = e.NewEditIndex;
                loadgrid();
                //Response.Redirect("UserMasterUpdate.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = grid1.Rows[e.RowIndex];

                string userid = (row.FindControl("lblUserId") as Label).Text;
                string username = (row.FindControl("txtUsername") as TextBox).Text;
                string pswd = (row.FindControl("txtPswd") as TextBox).Text;
                string type = (row.FindControl("txtType") as TextBox).Text;
                string active = (row.FindControl("chkActive") as TextBox).Text;


                string s = objusermanager.UpdateUserOption(userid, username, pswd, type, active);

                int n = DBConnection.ExecuteQuery(s);
                if (n > 0)
                {
                    loadgrid();
                }
                else
                {
                    Console.WriteLine("connection failed");
                }
                grid1.EditIndex = -1;

                loadgrid();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        protected void grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grid1.EditIndex = -1;
                loadgrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = grid1.Rows[e.RowIndex];

                string userid = (row.FindControl("lblUserId") as Label).Text;

                string s = objusermanager.DeleteOption(userid);

                int n = DBConnection.ExecuteQuery(s);
                if (n > 0)
                {
                    loadgrid();
                }
                else
                {
                    Console.WriteLine("connection failed");
                }
                grid1.EditIndex = -1;

                loadgrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}