using FBChecklist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    public class ServersService : IServersService<Server>
    {
       
        private AppEntities db = new AppEntities();

        public void AddServer(Server entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = "TshumaE";
            db.Servers.Add(entity);
            db.SaveChanges();
        }


        public void EditServer(Server dbentity, Server entity)
        {
            entity.ModifiedOn = DateTime.Now;
            entity.ModifiedBy = "TshumaE";
            dbentity.ModifiedOn = entity.ModifiedOn;
            dbentity.ModifiedBy = entity.ModifiedBy;
            dbentity.ServerIp =  entity.ServerIp;
            dbentity.ServerName = entity.ServerName;
            dbentity.IsActive = entity.IsActive;
            db.SaveChanges();
        }

        public IEnumerable<Server> GetAll()
        {
            return db.Servers.ToList();
        }

        public IEnumerable<SelectListItem> GetApplications()
        {
            using (var db = new AppEntities())
            {
                List<SelectListItem> applications = db.Applications.AsNoTracking()
                    .OrderBy(n => n.ApplicationName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.ApplicationId.ToString(),
                            Text = n.ApplicationName
                        }).ToList();
                var apptip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select application ---"
                };
                applications.Insert(0, apptip);
                return new SelectList(applications, "Value", "Text");
            }
        }

        public Server GetServerById(int id)
        {
            return db.Servers
                  .FirstOrDefault(e => e.ServerId == id);
        }

       public void GetApps(ServerViewModel model)
        {
            model.Applications = GetApplications();
        }
       
        //public void AddServer(Server server)
        //{         
        //    server.CreatedOn=DateTime.Now;
        //    server.CreatedBy = "TshumaE";
        //    db.Servers.Add(server);
        //    db.SaveChanges();
        //}

        //public IEnumerable<Server> GetAll()
        //{
        //    return db.Servers.ToList();
        //}
    }
}