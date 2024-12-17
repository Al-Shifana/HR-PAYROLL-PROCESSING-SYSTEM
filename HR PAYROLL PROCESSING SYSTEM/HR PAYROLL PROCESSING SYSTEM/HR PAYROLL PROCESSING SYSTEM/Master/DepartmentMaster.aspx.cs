using BussinessAccessLayer.Master.DepartmentMaster;
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
    public partial class DepartmentMaster : System.Web.UI.Page
    {
        DepartmentMasterManager objdepartmentMasterManager = new DepartmentMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadgrid();
            }
        }
        public void loadgrid()
        {
            DataTable dt = objdepartmentMasterManager.LoadGridDetails();
            grid1.DataSource = dt;
            grid1.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            DepartmentMasterEntity objDepartmentMaster = new DepartmentMasterEntity();

            objDepartmentMaster.deptNo = txtDeptNo.Text;
            objDepartmentMaster.deptName = txtDeptName.Text;

            try
            {
                if (objdepartmentMasterManager.IsDepartmentMasterExist(objDepartmentMaster.deptNo) > 0)
                {
                    string script = "Swal.fire({title: 'ERROR', text: 'Code and type already exists!', icon: 'warning'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", script, true);
                    return;
                }
                else
                {
                    int result = objdepartmentMasterManager.InsertDepartmentMasterDetails(objDepartmentMaster);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {

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

                string deptNo = (row.FindControl("lblDeptNo") as Label).Text;
                string deptName = (row.FindControl("txtDeptName") as TextBox).Text;

                string s = objdepartmentMasterManager.UpdateOption(deptNo, deptName);

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
                string deptNo = (row.FindControl("lblDeptNo") as Label).Text;

                string s = objdepartmentMasterManager.DeleteOption(deptNo);
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
    }
}