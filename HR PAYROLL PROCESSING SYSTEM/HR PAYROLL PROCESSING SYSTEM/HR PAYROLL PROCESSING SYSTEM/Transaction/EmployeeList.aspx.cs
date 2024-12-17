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
            DataTable dt = objprEmployee.LoadGridDetails();
            grid1.DataSource = dt;
            grid1.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int rowIndex = row.RowIndex;

            // Store the selected row's primary key value in session
            Session["eid"] = grid1.DataKeys[rowIndex].Value.ToString();
            Response.Redirect("EditEmployee.aspx");
        }
    }
}