using FBChecklist.Common;
using System.Linq;

namespace FBChecklist.Services
{
    public class DomainControllerService : IDomainControllerService<DomainController>
    {
        private AppEntities appEntities = new AppEntities();

        public string GetDirectoryEntry()
        {
            var ldap = (from c in appEntities.DomainControllers
                        select c.LDAP).FirstOrDefault();
            return ldap.ToString();
        }

        public string GetDomain()
        {
            var domain = (from c in appEntities.DomainControllers
                          select c.Domain).FirstOrDefault();
            return domain.ToString();

        }

        public string GetPassword()
        {
            var password = (from c in appEntities.DomainControllers
                            select c.Password).FirstOrDefault();
            return password.ToString();

        }

        public string GetUsername()
        {
            var username = (from c in appEntities.DomainControllers
                            select c.Username).FirstOrDefault();
            return username.ToString();

        }
    }
}