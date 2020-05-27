using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    interface IServicesService<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        void AddService(TEntity entity);

        void EditService(TEntity dbentity, TEntity entity);

        TEntity GetServiceById(int id);

       
    }
}
