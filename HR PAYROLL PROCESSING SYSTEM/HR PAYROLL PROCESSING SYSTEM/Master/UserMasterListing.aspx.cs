using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Master.UserMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class UserMasterListing : System.Web.UI.Page
    {
        UserMasterManger objUserMasterManager = new UserMasterManger();
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
                DataTable dt = objUserMasterManager.LoadGridDetails();
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

        protected void grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = grid1.Rows[e.RowIndex];

                string userid = (row.FindControl("lblUserId") as Label).Text;

                int s = objUserMasterManager.DeleteOption(userid);
                if (s > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteSuccess", "Swal.fire('Deleted!', 'The record is deleted.', 'success');", true);
                    int dltEmp = objUserMasterManager.DeleteFromPREmployee(userid);
                    if (dltEmp > 0)
                    {

                        int dltEmpHR = objUserMasterManager.DeleteFromPREmployeeHR(userid);
                        if (dltEmpHR > 0)
                        {
                            loadgrid();
                        }
                        else
                        {
                            Console.WriteLine("connection failed");
                        }
                        //loadgrid();
                    }
                    else
                    {
                        Console.WriteLine("connection failed");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteFailed", "Swal.fire('Failed!', 'The record is active.', 'error');", true);
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
            Response.Redirect("UserMaster.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("UserMaster.aspx");
        }

        protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string userId = grid1.DataKeys[e.NewEditIndex].Values["USER_ID"].ToString();
                Response.Redirect("UserMaster.aspx?pId=" + userId);
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
                DataTable dt = objUserMasterManager.LoadGridDetails();
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