using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Transaction
{
    public partial class EditEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // LoadDetails();
            }
        }
        //private void LoadDetails()
        //{
        //    UserManager usermanager = new UserManager();
        //    DataTable dt = usermanager.LoadUserDetails(Convert.ToString(Session["eid"]));

        //    DataRow row = dt.Rows[0];
        //    txtUserId.Text = row["USR_ID"].ToString();
        //    txtFirstName.Text = row["USR_FIRST_NAME"].ToString();
        //    txtMiddleName.Text = row["USR_MIDDLE_NAME"].ToString();
        //    txtLastName.Text = row["USR_LAST_NAME"].ToString();
        //    ddUserType.Text = row["USR_TYPE"].ToString();
        //    if (row["USR_DOB"] != DBNull.Value)
        //    {
        //        txtDOB.Text = Convert.ToDateTime(row["USR_DOB"]).ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        txtDOB.Text = string.Empty;
        //    }
        //    //txtDOB.Text = row["USR_DOB"].ToString();
        //    rdGender.Text = row["USR_GENDER"].ToString();
        //    //txtpswd.Text = row["USR_PASSWORD"].ToString();
        //    txtAddress.Text = row["USR_ADDRESS"].ToString();
        //    txtCity.Text = row["USR_CITY"].ToString();
        //    ddNation.Text = row["USR_NATION"].ToString();
        //    txtMobile.Text = row["USR_MOBILE"].ToString();
        //    txtEmail.Text = row["USR_EMAIL"].ToString();
        //    rdUserActive.Text = row["USR_ACTIVE_YN"].ToString();

        //}
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

    }
}