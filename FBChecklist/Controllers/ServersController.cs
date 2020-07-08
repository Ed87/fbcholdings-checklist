using FBChecklist.Services;
using FBChecklist.ViewModels;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace FBChecklist.Controllers
{
    public class ServersController : Controller
    {

        private AppEntities db = new AppEntities();
        private ServersService serversService;
      
      
        public ServersController(ServersService serversService)
        {
            this.serversService = serversService;
           
        }
        public ServersController() : this(new ServersService())
        {
            //the framework calls this
        }

        // GET: Servers
        public ActionResult Index()
        {

            return View(serversService.GetAll());
        }

        public ActionResult Email()
        {
            using (var message = new MailMessage("edmore.tshuma@fbc.co.zw", "tinotenda.musa@fbc.co.zw"))
            {
                message.Subject = "CHECKLIST REPORT TEST";
                message.Body = "Daily checklist completed at " + DateTime.Now + "Please find attached report for your review";
                message.CC.Add("farai.muusha@fbc.co.zw");
                message.CC.Add("edmore.tshuma@fbc.co.zw");

                SmtpClient client = new SmtpClient();
                client.EnableSsl = false;
                client.Host = "10.170.8.22";
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("fbc.corp\tshumae", "@Ert2020");

                {
                    client.Send(message);
                }
            }
            return View();
        }

        private void PopulateLookups(ServerViewModel model)
        {
            model.Applications = serversService.GetApplications();
        }

        // GET: Servers/Create
        public ActionResult Create()
        {
            var model = new ServerViewModel();          
            serversService.GetApps(model);
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,ServerIp,ServerName,CreatedBy,CreatedOn")] ServerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                serversService.GetApps(model);            
                return View(model);               
            }

            var server = new Server();
            model.UpdateModel(server);
            serversService.AddServer(server);

            return RedirectToAction("Index");
        }

        //// GET: Servers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Server server = db.Servers.Find(id);
        //    if (server == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(server);
        //}

        [HttpGet]
        public ActionResult Edit(int serverId)
        {
            var server = serversService.GetServerById(serverId);
            if (server == null) return RedirectToAction("Index");

            var model = new ServerViewModel(server);
            PopulateLookups(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Server server,[Bind(Include = "ServerIp,ServerName,IsActive,ModifiedBy,ModifiedOn")] ServerViewModel model)
        {

            Server serverToUpdate = serversService.GetServerById(model.ServerId);
            if (server == null) return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                PopulateLookups(model);
                serversService.EditServer(serverToUpdate, server);
                return View(model);
            }
           
            return View(server);
        }

        //// GET: Servers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Server server = db.Servers.Find(id);
        //    if (server == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(server);
        //}

        //// POST: Servers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Server server = db.Servers.Find(id);
        //    db.Servers.Remove(server);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
