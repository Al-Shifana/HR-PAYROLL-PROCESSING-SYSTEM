using BussinessAccessLayer;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessAccessLayer.Transaction.PREmployee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Transaction
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        PREmployeeManager objprEmployee = new PREmployeeManager();
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
                DataTable dt = objprEmployee.LoadGridDetails();
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int rowIndex = row.RowIndex;
            string empNo = grid1.DataKeys[rowIndex].Values["EMP_NO"].ToString();
            Response.Redirect("Employee.aspx?pEmpNo=" + empNo);


            //GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            //int rowIndex = row.RowIndex;
            //Session["eid"] = grid1.DataKeys[rowIndex].Value.ToString();
            //Response.Redirect("EditEmployee.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee.aspx");
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
                DataTable dt = objprEmployee.LoadGridDetails();
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