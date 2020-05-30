using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FBChecklist.Services
{
   public interface IServersService<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        void AddServer(TEntity entity);

        void EditServer(TEntity dbentity, TEntity entity);

        TEntity GetServerById(int id);

        IEnumerable<SelectListItem> GetApplications();

        //void PopulateLookups(TEntity entity);
        
    }
}
