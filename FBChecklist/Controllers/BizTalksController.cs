using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FBChecklist;

namespace FBChecklist.Controllers
{
    public class BizTalksController : Controller
    {
        private AppEntities db = new AppEntities();

        // GET: BizTalks
        public ActionResult Index()
        {
            DateTime date = DateTime.Today;
            var services = db.BizTalk.Include(d => d.Application1).Include(d => d.Server)
                            .Where(d => EntityFunctions.TruncateTime(d.RunDate) == date);
            return View(services.ToList());
          
        }

        // GET: BizTalks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BizTalk bizTalk = db.BizTalk.Find(id);
            if (bizTalk == null)
            {
                return HttpNotFound();
            }
            return View(bizTalk);
        }

        // GET: BizTalks/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName");
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp");
            return View();
        }

        // POST: BizTalks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApplicationId,ServerId,Name,Status,Uri,TransportType,ReceivePort,ReceiveHandler,InstanceName,RunDate")] BizTalk bizTalk)
        {
            if (ModelState.IsValid)
            {
                db.BizTalk.Add(bizTalk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", bizTalk.ApplicationId);
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", bizTalk.ServerId);
            return View(bizTalk);
        }

        // GET: BizTalks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BizTalk bizTalk = db.BizTalk.Find(id);
            if (bizTalk == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", bizTalk.ApplicationId);
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", bizTalk.ServerId);
            return View(bizTalk);
        }

        // POST: BizTalks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ApplicationId,ServerId,Name,Status,Uri,TransportType,ReceivePort,ReceiveHandler,InstanceName,RunDate")] BizTalk bizTalk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bizTalk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", bizTalk.ApplicationId);
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", bizTalk.ServerId);
            return View(bizTalk);
        }

        // GET: BizTalks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BizTalk bizTalk = db.BizTalk.Find(id);
            if (bizTalk == null)
            {
                return HttpNotFound();
            }
            return View(bizTalk);
        }

        // POST: BizTalks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BizTalk bizTalk = db.BizTalk.Find(id);
            db.BizTalk.Remove(bizTalk);
            db.SaveChanges();
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
