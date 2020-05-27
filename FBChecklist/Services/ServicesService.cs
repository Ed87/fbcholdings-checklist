using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    public class ServicesService : IServicesService<Service>
    {
        private AppEntities db = new AppEntities();
        public void AddService(Service entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = "TshumaE";
            db.Services.Add(entity);
            db.SaveChanges();
        }

        public void EditService(Service dbentity, Service entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Service> GetAll()
        {
            return db.Services.ToList();
        }

       
        public Service GetServiceById(int id)
        {
            return db.Services
                 .FirstOrDefault(e => e.ServiceId == id);
        }
    }
}