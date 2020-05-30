using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FBChecklist.Controllers
{
    public class DisksController : Controller
    {
        private AppEntities db = new AppEntities();

        // GET: Disks
        public async Task<ActionResult> Index()
        {
            var disks = db.Disks.Include(d => d.Application).Include(d => d.Server);
            return View(await disks.ToListAsync());
        }

        // GET: Disks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disk disk = await db.Disks.FindAsync(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            return View(disk);
        }

        // GET: Disks/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName");
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp");
            return View();
        }

        // POST: Disks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DiskId,ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate,Memory,CPU")] Disk disk)
        {
            if (ModelState.IsValid)
            {
                db.Disks.Add(disk);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", disk.ApplicationId);
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", disk.ServerId);
            return View(disk);
        }

        // GET: Disks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disk disk = await db.Disks.FindAsync(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", disk.ApplicationId);
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", disk.ServerId);
            return View(disk);
        }

        // POST: Disks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DiskId,ApplicationId,ServerId,FreeSpace,UsedSpace,TotalSpace,DiskName,RunDate,Memory,CPU")] Disk disk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disk).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", disk.ApplicationId);
            ViewBag.ServerId = new SelectList(db.Servers, "ServerId", "ServerIp", disk.ServerId);
            return View(disk);
        }

        // GET: Disks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disk disk = await db.Disks.FindAsync(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            return View(disk);
        }

        // POST: Disks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Disk disk = await db.Disks.FindAsync(id);
            db.Disks.Remove(disk);
            await db.SaveChangesAsync();
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
