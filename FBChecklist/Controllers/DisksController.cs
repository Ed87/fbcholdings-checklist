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

            //Get DropDown selected value
            var selectedValue = Request.Form["ApplicationId"].ToString(); 
            var sv = Convert.ToInt32(selectedValue);
            Session["SelectedApp"] = sv;
            Session["ServerId"] = disksService.GetServerId(sv);

            Session["Authority"] = disksService.GetAuthority(Helpers.parameters.ActiveDirectory);
            Session["Username"] = disksService.GetSuperUsername(Helpers.parameters.ActiveDirectory);
            Session["Password"] = disksService.GetSuperUserPassword(Helpers.parameters.ActiveDirectory);
            Session["ServerIP"] = disksService.GetServerIp(sv);

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
