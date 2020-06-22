using FBChecklist.Services;
using FBChecklist.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FBChecklist.Controllers
{
    public class ServicesController : Controller
    {
        private AppEntities db = new AppEntities();
        private ServicesRepository servicesRepository;


        public ServicesController(ServicesRepository servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public ServicesController() : this(new ServicesRepository())
        {
            //the framework calls this
        }



        // GET: Services
        public ActionResult Index()
        {
            //return View(db.Services.ToList());
            var services = db.Services.Include(s => s.Server).Include(s => s.Application);
            return View(services.ToList());
        }

      

        // GET: ServicesStatus
        public ActionResult ServiceStatus()
        {
            DateTime date = DateTime.Today;
            var services = db.ServiceMonitors.Include(d => d.Application).Include(d => d.Server).Include(d =>d.Service)
                            .Where(d => EntityFunctions.TruncateTime(d.RunDate) == date);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            var model = new ServicesViewModel();
            // servicesRepository.GetServers(model);
            servicesRepository.GetApps(model);
            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,ServiceName,CreatedBy,CreatedOn,ServerId,ShortName,ApplicationId")] ServicesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // servicesRepository.GetServers(model);
                servicesRepository.GetApps(model);
                return View(model);
            }

            var selectedValue = Request.Form["ApplicationId"].ToString();
            var sv = Convert.ToInt32(selectedValue);
            Session["SelectedApp"] = sv;
            Session["ServerId"] = servicesRepository.GetServerId(sv);

            var service = new Service();
            model.UpdateModel(service);
            servicesRepository.AddService(service);
            return RedirectToAction("Index");
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", service.ServerId);
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", service.ApplicationId);
            return View(service);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,ServiceName,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,ServerId,ShortName,ApplicationId")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", service.ServerId);
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", service.ApplicationId);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult QueryService()
        {
            var model = new ServiceMonitorViewModel();
            servicesRepository.GetApps(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QueryService([Bind(Include = "ServiceId,ServiceName,CreatedBy,RunDate,ServerId,Status,ApplicationId")] ServiceMonitorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                servicesRepository.GetApps(model);
                return View(model);
            }

            var selectedValue = Request.Form["ApplicationId"].ToString();
            var sv = Convert.ToInt32(selectedValue);
            Session["SelectedApplication"] = sv;

            Session["ServerId"] = servicesRepository.GetServerId(sv);
            Session["ServerIP"] = servicesRepository.GetServerIp(sv);
            Session["Services"] = servicesRepository.GetApplicationServices(sv);
            Session["Authority"] = servicesRepository.GetAuthority(sv);
            Session["Username"] = servicesRepository.GetSuperUsername(sv);
            Session["Password"] = servicesRepository.GetSuperUserPassword(sv);

            var servicemonitor = new ServiceMonitor();
            servicesRepository.SaveServicesInfo(servicemonitor);
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
