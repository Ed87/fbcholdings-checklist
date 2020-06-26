using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBChecklist.Services
{
    interface IBizTalkService<TEntity>
    {
        List<TEntity> GetBizTalkServicesStatistics();

        void SaveStatistics(TEntity entity);

    
    }
}
