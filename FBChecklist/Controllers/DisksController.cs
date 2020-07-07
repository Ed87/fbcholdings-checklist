using FBChecklist.Common;
using FBChecklist.Exceptions;
using FBChecklist.Services;
using FBChecklist.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FBChecklist.Controllers
{
    public class DisksController : Controller
    {

        private AppEntities db = new AppEntities();
        private DisksService disksService;


        public DisksController(DisksService disksService)
        {
            this.disksService = disksService;

        }
        public DisksController() : this(new DisksService())
        {
            //the framework calls this
        }




        // GET: Disks
        public  ActionResult Index()
        {
            ViewBag.SunSystems = Helpers.parameters.Sunsys;
            ViewBag.BIP1 = Helpers.parameters.bipserver1;
            ViewBag.BIP2 = Helpers.parameters.bipserver2;
            ViewBag.GIS = Helpers.parameters.Gis;
            ViewBag.BLP1 = Helpers.parameters.billpayments1;
            ViewBag.sics = Helpers.parameters.sics;
            ViewBag.tds = Helpers.parameters.tds;
            ViewBag.sybrin = Helpers.parameters.sybrin;
            ViewBag.SunSystems = Helpers.parameters.Sunsys;
            ViewBag.VM1 = Helpers.parameters.Vm1;
            ViewBag.VM2 = Helpers.parameters.Vm2;
            ViewBag.VM3 = Helpers.parameters.Vm3;
            ViewBag.VM4 = Helpers.parameters.Vm4;
            ViewBag.VM5 = Helpers.parameters.Vm5;
            ViewBag.FCCm = Helpers.parameters.FCCm;
            ViewBag.RTGS = Helpers.parameters.RTGs;
            ViewBag.BLP2 = Helpers.parameters.billpayments2;
            ViewBag.BR = Helpers.parameters.bR;
            DateTime date = DateTime.Today;
            var disks = db.Disks.Include(d => d.Application).Include(d => d.Server)
                            .Where(d => EntityFunctions.TruncateTime(d.RunDate) == date);
                return View( disks.ToList());
           
        }


    

        // GET: Disks/QueryDiskInformation
        public ActionResult QueryDiskInformation()
        {
            var model = new DiskViewModel();
            disksService.GetApps(model);
            return View(model);
          
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QueryDiskInformation([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                disksService.GetApps(model);
                return View(model);
            }

           // Get DropDown selected value
            var selectedValue = Request.Form["ApplicationId"].ToString();
            var sv = Convert.ToInt32(selectedValue);
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);

            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
           // Session["ServerIP"] = disksService.GetServerIp(sv);

            var disk = new Disk();                  
            disksService.SaveEnvironmentInfo(disk);      
            return RedirectToAction("Index");
        }


        public ActionResult SICSNT([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.SICSNT;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }



        public ActionResult RTGS([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.RTGS;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }

        public ActionResult GIS([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.GIS;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }


        public ActionResult Sybrin([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.Sybrin;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }

        public ActionResult BillPayments2([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.BP2;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }


        public ActionResult BillPayments1([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.BizTalk;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }





        public ActionResult SunSystems([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.SunSystems;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }



        public ActionResult TDS([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.TDS;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }


        public ActionResult BIPServer2([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.BIPServer2;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }

        public ActionResult BIPServer1([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.BIPServer1;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }


        public ActionResult VM5([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.VM5;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }


        public ActionResult VM4([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.VM4;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }



        public ActionResult VM3([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.VM3;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }


        public ActionResult VM2([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.VM2;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
        }

        public ActionResult VM1([Bind(Include = "ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate")] DiskViewModel model)
        {

            var sv = Helpers.parameters.VM1;
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);
            Session["ServerIP"] = disksService.GetServerIp(sv);
            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            var disk = new Disk();
            disksService.SaveEnvironmentInfo(disk);
            return RedirectToAction("Index");
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
