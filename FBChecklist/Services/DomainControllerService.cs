using FBChecklist.Common;
using System.Linq;

namespace FBChecklist.Services
{
    public class DomainControllerService : IDomainControllerService<DomainController>
    {
        private AppEntities appEntities = new AppEntities();

        public string GetDirectoryEntry()
        {
         var dentry= (from j in appEntities.DomainControllers                            
                             select new
                             {
                                j.LDAP
                             });
            return dentry.ToString();
        }

        public string GetDomain()
        {
            var domain = (from j in appEntities.DomainControllers
                          select new
                          {
                              j.Domain
                          });
            return domain.ToString();
        }

        public string GetPassword()
        {
            var password = (from j in appEntities.DomainControllers
                          select new
                          {
                              j.Password
                          });
            return password.ToString();
        }

        public string GetUsername()
        {
            var username = (from j in appEntities.DomainControllers
                            select new
                            {
                                j.Username
                            });
            return username.ToString();
        }
    }
}