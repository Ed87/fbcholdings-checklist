using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FBChecklist.Common;
using FBChecklist.Exceptions;

namespace FBChecklist.Controllers
{
    public class DashboardsController : Controller
    {
        private AppEntities db = new AppEntities();

        // GET: Dashboards
        public ActionResult Index()
        {
            var dashboard = db.Dashboard.Include(d => d.Application).Include(d => d.Server);
            return View(dashboard.ToList());
        }

        // GET: Dashboards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dashboard dashboard = db.Dashboard.Find(id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }
            return View(dashboard);
        }

        // GET: Dashboards/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName");
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp");
            return View();
        }

        public ActionResult QueryService()
        {
            return RedirectToAction("QueryService", "Services");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApplicationId,TaskId,Status,RunDate,CheckedBy,Percentage,ServerId")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                db.Dashboard.Add(dashboard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", dashboard.ApplicationId);
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", dashboard.ServerId);
            return View(dashboard);
        }

        public ActionResult Tasks()
        {

            return View();
        }

        //AppServices

        public ActionResult GetGISServiceStatistics()
        {
            return RedirectToAction("GISServices", "Services");
        }


        public ActionResult GetSunSystemsServiceStatistics()
        {
            return RedirectToAction("SunSystemsServices", "Services");
        }

        public ActionResult GetBillPayments2ServiceStatistics()
        {
            return RedirectToAction("BillPayments2Services", "Services");
        }

        public ActionResult GetBillPayments1ServiceStatistics()
        {
            return RedirectToAction("BillPayments1Services", "Services");
        }


        public ActionResult GetSICSNTServiceStatistics()
        {
            return RedirectToAction("SICSNTServices", "Services");
        }

        public ActionResult GetTDSServiceStatistics()
        {
            return RedirectToAction("TDSServices", "Services");
        }

        public ActionResult GetSybrinServiceStatistics()
        {
            return RedirectToAction("SybrinServices", "Services");
        }

        public ActionResult IsZIMRAVPNReachable()
        {
            return RedirectToAction("IsZIMRAVPNReachable", "Webrequests");
        }
        public ActionResult IsFCCMReachable()
        {
            return RedirectToAction("IsFCCMReachable", "Webrequests");
        }

        public ActionResult IsBIP12cReachable()
        {
            return RedirectToAction("IsBIP12cReachable", "Webrequests");
        }

        public ActionResult GetFlexcubeTimeLevelStatistics()
        {
            return RedirectToAction("CheckTimeLevel", "Flexcube");
        }


        public ActionResult GetZimraSTPBizTalkServices()
        {
            return RedirectToAction("Biztalk", "Weblogic");
        }

        public ActionResult GetWeblogicApplicationServerServices()
        {
            return RedirectToAction("Index", "Weblogic");
        }

        public ActionResult GetSICSNTPerfomanceStatistics()
        {
            return RedirectToAction("SICSNT", "Disks");
        }


        public ActionResult GetVM1PerfomanceStatistics()
        {
            return RedirectToAction("VM1", "Disks");
        }

        public ActionResult GetVM2PerfomanceStatistics()
        {
            return RedirectToAction("VM2", "Disks");
        }
        public ActionResult GetVM3PerfomanceStatistics()
        {
            return RedirectToAction("VM3", "Disks");
        }

        public ActionResult GetVM4PerfomanceStatistics()
        {
            return RedirectToAction("VM4", "Disks");
        }

        public ActionResult GetVM5PerfomanceStatistics()
        {
            return RedirectToAction("VM5", "Disks");
        }

        public ActionResult GetSunSystemsPerfomanceStatistics()
        {
            return RedirectToAction("SunSystems", "Disks");
        }



        public ActionResult GetBillPayments1PerfomanceStatistics()
        {
            return RedirectToAction("BillPayments1", "Disks");
        }


        public ActionResult GetBillPayments2PerfomanceStatistics()
        {
            return RedirectToAction("BillPayments2", "Disks");
        }





        public ActionResult GetSybrinPerfomanceStatistics()
        {
            return RedirectToAction("Sybrin", "Disks");
        }


        public ActionResult GetTDSPerfomanceStatistics()
        {
            return RedirectToAction("TDS", "Disks");
        }


        public ActionResult GetBIPServer1PerfomanceStatistics()
        {
            return RedirectToAction("BIPServer1", "Disks");
        }

        public ActionResult GetBIPServer2PerfomanceStatistics()
        {
            return RedirectToAction("BIPServer2", "Disks");
        }

        public ActionResult GenerateMemo()
        {

            SqlConnection con = new SqlConnection(Helpers.DatabaseConnect);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Description,CurrencySymbol,ActualAmount,CurrencyAbbr FROM vw_PurchasesByCriteria WHERE Reference='" + Convert.ToString(Session["Reference"]) + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

            }
            catch (Exception ex)
            {
                ExceptionLogger.SendErrorToText(ex);
                //ViewBag.ErrorMessage = Messages.GENERAL_ERROR;
                return RedirectToAction("ReportError");

            }
            var today = DateTime.Now.ToShortDateString();
            var TaskReport = "Memo";
            string OutputFileName = TaskReport.ToString() + "_" + today + ".pdf";

            ReportClass rptH = new ReportClass();
            rptH.FileName = Server.MapPath(("~/Reports/") + "PurchaseMemo.rpt");

            rptH.Load();
            rptH.SetDataSource(dt);


            string filePath = Server.MapPath("~/TaskReports/");
            string destPath = Path.Combine(filePath, Helpers.ToSafeFileName(OutputFileName));

            rptH.ExportToDisk(ExportFormatType.PortableDocFormat, destPath);
            Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        public ActionResult GenerateWordMemo()
        {

            SqlConnection con = new SqlConnection(Helpers.DatabaseConnect);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Description,CurrencySymbol,ActualAmount,CurrencyAbbr FROM vw_PurchasesByCriteria WHERE Reference='" + Convert.ToString(Session["Reference"]) + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                //ExceptionLogging.SendErrorToText(ex);
               // ViewBag.ErrorMessage = Messages.GENERAL_ERROR;
                return RedirectToAction("ReportError");
            }
            var today = DateTime.Now.ToShortDateString();
            var TaskReport = "Memo";
            string OutputFileName = TaskReport.ToString() + "_" + today + ".pdf";

            ReportClass rptH = new ReportClass();
            rptH.FileName = Server.MapPath(("~/Reports/") + "PurchaseMemo.rpt");

            rptH.Load();
            rptH.SetDataSource(dt);


            string filePath = Server.MapPath("~/TaskReports/");
            string destPath = Path.Combine(filePath, Helpers.ToSafeFileName(OutputFileName));

            rptH.ExportToDisk(ExportFormatType.PortableDocFormat, destPath);

            Stream stream = rptH.ExportToStream(ExportFormatType.WordForWindows);
            return File(stream, " application/msword");
        }

        public ActionResult GetOracleDbServerPerfomanceStatistics()
        {
            return RedirectToAction("OracleDbServer", "Solaris");
        }

        public ActionResult GetWeblogicAppServerPerfomanceStatistics()
        {
            return RedirectToAction("WeblogicAppServer", "Solaris");
        }
                          
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
