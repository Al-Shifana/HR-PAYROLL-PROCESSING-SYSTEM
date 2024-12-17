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
    public partial class CodesMasterListing : System.Web.UI.Page
    {
        CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        CodeMasterEntity objCodeMaster = new CodeMasterEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string pId = Request.QueryString["pId"];
            if (!string.IsNullOrEmpty(pId))
            {
                Response.Redirect("~/LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                loadgrid();
            }
        }
        public void loadgrid()
        {
            try
            {
                DataTable dt = objCodeMasterManager.LoadGridDetails();
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
                CodeMasterEntity objCodeMasterEntity = new CodeMasterEntity();
                CodeMasterManager objCodeMasterManager = new CodeMasterManager();
                string cmCode = grid1.DataKeys[e.RowIndex]["CM_CODE"].ToString();
                string cmType = grid1.DataKeys[e.RowIndex]["CM_TYPE"].ToString();

                objCodeMasterEntity.cmCode = cmCode;
                objCodeMasterEntity.cmType = cmType;

                if (objCodeMasterManager.DeleteOption(objCodeMasterEntity) > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteSuccess", "Swal.fire('Deleted!', 'The record is deleted.', 'success');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteFailed", "Swal.fire('Failed!', 'The record is active.', 'error');", true);                   
                }
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
            Response.Redirect("CodesMaster.aspx");
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

        protected void grid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowIndex == grid1.EditIndex) && (e.Row.RowIndex >= 0))
                {
                    grid1.DataBind();
                    //CodesMasterManager objCodesmasterManager = new CodesMasterManager();

                    //DropDownList ddlCmActiveYn = (e.Row.FindControl("ddlCmActiveYn") as DropDownList);
                    //ddlCmActiveYn.DataSource = objCodeMasterManager.GetCmActiveYn();
                    //ddlCmActiveYn.DataTextField = "CM_DESC";
                    //ddlCmActiveYn.DataValueField = "CM_CODE";
                    //ddlCmActiveYn.DataBind();
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
                CodeMasterManager objCodeMasterManager = new CodeMasterManager();
                DataTable dt = objCodeMasterManager.LoadGridDetails();
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

        protected void grid1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string code = grid1.DataKeys[e.NewEditIndex].Values["CM_CODE"].ToString();
                string type = grid1.DataKeys[e.NewEditIndex].Values["CM_TYPE"].ToString();
                Response.Redirect("CodesMaster.aspx?pCode=" + code + "&pType=" + type);
            }
            catch (Exception)
            {
                ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
                string errTitle = objErrorCodeMasterManager.FnFetchError("E003");
                string script = $"Swal.fire('Error',{"error occured"},'error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", script, true);
            }
        }

        
    }
}