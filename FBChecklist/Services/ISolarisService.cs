using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBChecklist.Services
{
    public interface ISolarisService<TEntity>
    {
        List<TEntity> GetDiskStatistics();

        void SaveDiskInfo(TEntity entity);
    }
}