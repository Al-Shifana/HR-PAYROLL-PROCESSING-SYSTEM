using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using System.Data;
using BussinessAccessLayer.Transaction.PREmployeePayroll;

namespace HR_PAYROLL_PROCESSING_SYSTEM
{
    public partial class PaySlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string yymm = Request.QueryString["yymm"];
                    string eid = Request.QueryString["eid"];

                    PREmployeePayrollManager objPREmployeePayrollManager = new PREmployeePayrollManager();
                    DataTable dt = objPREmployeePayrollManager.LoadDetails(eid, yymm);

                    ReportDocument reportDocument = new ReportDocument();
                    reportDocument.Load(Server.MapPath("CrystalReport1.rpt"));
                    reportDocument.SetDataSource(dt);
                    ExportToPdf(reportDocument);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ExportToPdf(ReportDocument reportDocument)
        {
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "UserInformation" + "Admin");
            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }
        private void ExportToPdf(ReportDocument reportDocument, string puid)
        {
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "UserInformation" + $"{puid}");
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}