using BussinessAccessLayer.Master.CodeMaster;
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
    public partial class CodesMasterListingTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCodesMasterListing();
            }
        }

        public void LoadCodesMasterListing()
        {
            try
            {
                CodeMasterManager objCodes = new CodeMasterManager();
                gvCodesMaster.DataSource = objCodes.LoadGridDetails();
                gvCodesMaster.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvCodesMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string cmcode = gvCodesMaster.DataKeys[e.NewEditIndex]["CM_CODE"].ToString();
                string cmtype = gvCodesMaster.DataKeys[e.NewEditIndex]["CM_TYPE"].ToString();
                Response.Redirect($"/Master/CodesMaster.aspx?pCode={cmcode}&pType={cmtype}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvCodesMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                CodeMasterEntity objCodeMasterEntity = new CodeMasterEntity();
                CodeMasterManager objCodeMasterManager = new CodeMasterManager();
                string cmCode = gvCodesMaster.DataKeys[e.RowIndex]["CM_CODE"].ToString();
                string cmType = gvCodesMaster.DataKeys[e.RowIndex]["CM_TYPE"].ToString();

                objCodeMasterEntity.cmCode = cmCode;
                objCodeMasterEntity.cmType = cmType;

                objCodeMasterManager.DeleteOption(objCodeMasterEntity);
                LoadCodesMasterListing();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvCodesMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void gvCodesMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void gvCodesMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCodesMaster.PageIndex = e.NewPageIndex;
                this.LoadCodesMasterListing();
            }
            catch (Exception ex)
            {
                throw ex;
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
        protected void gvCodesMaster_Sorting(object sender, GridViewSortEventArgs e)
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
                gvCodesMaster.DataSource = sortedView;
                gvCodesMaster.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}