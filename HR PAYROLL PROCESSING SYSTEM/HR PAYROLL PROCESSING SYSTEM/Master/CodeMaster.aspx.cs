using BussinessAccessLayer.Master.CodeMaster;
using BussinessLayer.Master.CodeMaster;
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
    public partial class CodeMaster : System.Web.UI.Page
    {
        CodeMasterManager objcodemastermanager = new CodeMasterManager();
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
                DataTable dt = objcodemastermanager.LoadGridDetails();
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
            CodeMasterEntity codemasterentity = new CodeMasterEntity();

            codemasterentity.cmCode = txtCmCode.Text;
            codemasterentity.cmType = txtCmType.Text;
            codemasterentity.cmDesc = txtCmDesc.Text;
            codemasterentity.cmValue = Convert.ToInt32(txtCmValue.Text);
            codemasterentity.cmActiveYN = chkActive.Checked ? "Y" : "N";

            try
            {
                if (objcodemastermanager.IsCodeMasterExist(txtCmCode.Text, txtCmType.Text) > 0)
                {
                    string script = "Swal.fire({title: 'ERROR', text: 'Code and type already exists!', icon: 'warning'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                    return;
                }
                else
                {
                    int result = objcodemastermanager.InsertCodeMasterDetails(codemasterentity);
                    if (result > 0)
                    {
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

                string cmcode = (row.FindControl("lblCmCode") as Label).Text;
                string cmtype = (row.FindControl("lblCmType") as Label).Text;
                string cmdesc = (row.FindControl("txtCmDesc") as TextBox).Text;
                string cmvalue = (row.FindControl("txtCmValue") as TextBox).Text;
                string cmactive = (row.FindControl("chkActive") as TextBox).Text;


                string s = objcodemastermanager.UpdateOption(cmcode, cmtype, cmdesc, cmvalue, cmactive);

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
                string cmcode = (row.FindControl("lblCmCode") as Label).Text;
                string cmtype = (row.FindControl("lblCmType") as Label).Text;

                string s = objcodemastermanager.DeleteOption(cmcode, cmtype);
                int n = DBConnection.ExecuteQuery(s);
                if (n > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteSuccess", "Swal.fire('Deleted!', 'The record has been deleted.', 'success');", true);
                    loadgrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteFailed", "Swal.fire('Failed!', 'The record could not be deleted.', 'error');", true);
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