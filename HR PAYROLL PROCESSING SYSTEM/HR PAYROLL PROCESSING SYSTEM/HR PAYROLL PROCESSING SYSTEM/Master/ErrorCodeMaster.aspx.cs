using System;
using BussinessAccessLayer.Master.ErrorCodeMaster;
using BussinessLayer.Master.CodeMaster;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HR_PAYROLL_PROCESSING_SYSTEM.Master
{
    public partial class ErrorCodeMaster : System.Web.UI.Page
    {
        ErrorCodeMasterManager objErrorCodeManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            loadgrid();
        }
        public void loadgrid()
        {
            try
            {
                DataTable dt = objErrorCodeManager.LoadGridDetails();
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
             ErrorCodeMasterEntity objErrorCodeMaster = new ErrorCodeMasterEntity();

            objErrorCodeMaster.errCode = txtErrCode.Text;
            objErrorCodeMaster.errType = txtErrType.Text;
            objErrorCodeMaster.errDesc = txtErrDesc.Text;
            try
            {
                if (objErrorCodeManager.IsErrorCodeMasterExist(objErrorCodeMaster.errCode, objErrorCodeMaster.errType) > 0)
                {
                    string script = "Swal.fire({title: 'ERROR', text: 'Code and type already exists!', icon: 'warning'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "registrationfailed", script, true);
                    return;
                }
                else
                {
                    int result = objErrorCodeManager.InsertErrorCodeMasterDetails(objErrorCodeMaster);
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
    }
}