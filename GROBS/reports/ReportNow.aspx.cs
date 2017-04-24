using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using GROBS.EFModels;
using GROBS.IBSL;
using GROBS.BSL;
namespace GROBS.reports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int _userid = (int)Session["user_id"];
                string name = "";
                int i = 0;
                ReportDataSet _rds = new ReportDataSet();
                //DataTable _dt;                
                if (Request.QueryString["pid"] != null)
                {
                    name = Request.QueryString["pid"];
                    switch (name)
                    {
                        case "personrights":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptQuanxian.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtqx = _rds.Tables["Quanxian"];
                            var personrights = ServiceFactory.auth_quanxianservice.GetPersonRightsFirst(_userid);
                            i = 0;
                            DataRow drqx;
                            foreach (auth_personrights _pr in personrights)
                            {
                                i++;
                                drqx = dtqx.NewRow();
                                drqx["id"] = i;
                                drqx["rolename"] = _pr.rolename;
                                drqx["username"] = _pr.fullname;
                                drqx["module"] = _pr.module;
                                drqx["name"] = _pr.name;
                                drqx["control"] = _pr.controller;
                                drqx["function"] = _pr.function;
                                dtqx.Rows.Add(drqx);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["Quanxian"]));

                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}