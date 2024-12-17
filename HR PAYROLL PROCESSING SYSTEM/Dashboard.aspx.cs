using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pId = Request.QueryString["pId"];
            if (string.IsNullOrEmpty(pId))
            {
                Response.Redirect("LoginPage.aspx");
            }
            
        }
    }
}