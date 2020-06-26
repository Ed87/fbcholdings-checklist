using FBChecklist.Services;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FBChecklist.Controllers
{
    public class WebLogicController : Controller
    {
        private AppEntities db = new AppEntities();

        //// GET: WebLogic
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.WebLogics.ToListAsync());
        //}


        private WebLogicService webLogicService;
        private BizTalkService bizTalkService;


        public WebLogicController(WebLogicService webLogicService, BizTalkService bizTalkService)
        {

            this.webLogicService = webLogicService;
            this.bizTalkService = bizTalkService;
        }

        public WebLogicController() : this(new WebLogicService(), new BizTalkService())
        {
            //the framework calls this
        }



        // GET: WebLogics
        public async Task<ActionResult> Index()
        {
            //return View(await db.WebLogics.ToListAsync());
            return View(await webLogicService.GetServerStatistics());
        }

        public ActionResult BizTalk()
        {
            BizTalk model = new BizTalk();
            bizTalkService.SaveStatistics(model);
           
            return View();
        }

        // GET: WebLogic/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebLogic webLogic = await db.WebLogics.FindAsync(id);
            if (webLogic == null)
            {
                return HttpNotFound();
            }
            return View(webLogic);
        }

        // GET: WebLogic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebLogic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,name,heapFreeCurrent,heapSizeCurrent,state,health,usedPhysicalMemory,activeThreadCount,jvmProcessorLoad,RunDate")] WebLogic webLogic)
        {
            if (ModelState.IsValid)
            {
                db.WebLogics.Add(webLogic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(webLogic);
        }

        // GET: WebLogic/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebLogic webLogic = await db.WebLogics.FindAsync(id);
            if (webLogic == null)
            {
                return HttpNotFound();
            }
            return View(webLogic);
        }

        // POST: WebLogic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,name,heapFreeCurrent,heapSizeCurrent,state,health,usedPhysicalMemory,activeThreadCount,jvmProcessorLoad,RunDate")] WebLogic webLogic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webLogic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(webLogic);
        }

        // GET: WebLogic/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebLogic webLogic = await db.WebLogics.FindAsync(id);
            if (webLogic == null)
            {
                return HttpNotFound();
            }
            return View(webLogic);
        }

        // POST: WebLogic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WebLogic webLogic = await db.WebLogics.FindAsync(id);
            db.WebLogics.Remove(webLogic);
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
