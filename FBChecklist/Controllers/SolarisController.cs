using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FBChecklist;
using FBChecklist.Services;
using FBChecklist.ViewModels;

namespace FBChecklist.Controllers
{
    public class SolarisController : Controller
    {
        private AppEntities db = new AppEntities();
        private SolarisService solarisService;


        public SolarisController(SolarisService solarisService)
        {
            this.solarisService = solarisService;

        }
        public SolarisController() : this(new SolarisService())
        {
            //the framework calls this
        }


        // GET: Solaris
        public ActionResult Index()
        {
            var solaris = db.Solaris.Include(s => s.Application);
            return View(solaris.ToList());
        }

        // GET: Solaris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solaris solaris = db.Solaris.Find(id);
            if (solaris == null)
            {
                return HttpNotFound();
            }
            return View(solaris);
        }

        // GET: Solaris/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Filesystem,Blocks,Used,Available,Capacity,Mount,RunDate,ApplicationId")] Solaris solaris)
        {
            if (ModelState.IsValid)
            {
                db.Solaris.Add(solaris);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", solaris.ApplicationId);
            return View(solaris);
        }

        // GET: Solaris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solaris solaris = db.Solaris.Find(id);
            if (solaris == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", solaris.ApplicationId);
            return View(solaris);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Filesystem,Blocks,Used,Available,Capacity,Mount,RunDate,ApplicationId")] Solaris solaris)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solaris).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", solaris.ApplicationId);
            return View(solaris);
        }

        // GET: Solaris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solaris solaris = db.Solaris.Find(id);
            if (solaris == null)
            {
                return HttpNotFound();
            }
            return View(solaris);
        }

        // POST: Solaris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solaris solaris = db.Solaris.Find(id);
            db.Solaris.Remove(solaris);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Disks/QueryDiskInformation
        public ActionResult QueryDiskInformation()
        {
            var model = new SolarisViewModel();
            solarisService.GetApps(model);
            return View(model);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QueryDiskInformation([Bind(Include = "ApplicationId,Filesystem,Blocks,Mount,Used,Available,Capacity,RunDate")] SolarisViewModel model)
        {
            if (!ModelState.IsValid)
            {
                solarisService.GetApps(model);
                return View(model);
            }

            //Get DropDown selected value
            var selectedValue = Request.Form["ApplicationId"].ToString();
            var sv = Convert.ToInt32(selectedValue);
            Session["SelectedApp"] = sv;
          
            Session["Username"] = solarisService.GetSuperUsername(sv);
            Session["Password"] = solarisService.GetSuperUserPassword(sv);
            Session["ServerIP"] = solarisService.GetServerIp(sv);

            var disk = new Solaris();
            solarisService.SaveDiskInfo(disk);
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
