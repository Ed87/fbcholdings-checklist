using FBChecklist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    public class ServicesRepository : IServicesRepository<Service>
    {
        private AppEntities appEntities = new AppEntities();

        public void AddService(Service entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = "Admin";
            appEntities.Services.Add(entity);
            appEntities.SaveChanges();
        }

        public IEnumerable<Service> GetAll()
        {
            return appEntities.Services.ToList();
        }

        public IEnumerable<SelectListItem> PopulateServers()
        {
            using (var db = new AppEntities())
            {
                List<SelectListItem> servers = db.Servers.AsNoTracking()
                    .OrderBy(n => n.ServerIp)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.ServerId.ToString(),
                            Text = n.ServerIp
                        }).ToList();
                var apptip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Server ---"
                };
                servers.Insert(0, apptip);
                return new SelectList(servers, "Value", "Text");
            }
        }

        public void GetServers(ServicesViewModel model)
        {
            model.Servers= PopulateServers();
        }
    }
}