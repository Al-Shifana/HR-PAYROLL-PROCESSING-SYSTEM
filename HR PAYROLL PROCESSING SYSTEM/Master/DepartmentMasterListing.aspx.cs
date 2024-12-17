using BussinessAccessLayer.Master.DepartmentMaster;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class DepartmentMasterListing : System.Web.UI.Page
    {
        DepartmentMasterManager objDeptMasterMgr = new DepartmentMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                loadgrid();
            }
        }
        public void loadgrid()
        {
            DataTable dt = objDeptMasterMgr.LoadGridDetails();
            grid1.DataSource = dt;
            grid1.DataBind();
        }
        protected void grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DepartmentMasterEntity objDepartmentMaster = new DepartmentMasterEntity();

                GridViewRow row = grid1.Rows[e.RowIndex];
                objDepartmentMaster.deptNo = (row.FindControl("lblDeptNo") as Label).Text;

                int s = objDeptMasterMgr.DeleteOption(objDepartmentMaster);
                if (s > 0)
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
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        } 
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentMaster.aspx");
        }

        protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string DeptNo = grid1.DataKeys[e.NewEditIndex].Values["DEPT_NO"].ToString();
                Response.Redirect("DepartmentMaster.aspx?pDeptNo=" + DeptNo);
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

        protected void grid1_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortingDirection = string.Empty;
                if (sd == SortDirection.Ascending)
                {
                    sd = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    sd = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                DataTable dt = objDeptMasterMgr.LoadGridDetails();
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                grid1.DataSource = sortedView;
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

        public SortDirection sd
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }

        
    }
}