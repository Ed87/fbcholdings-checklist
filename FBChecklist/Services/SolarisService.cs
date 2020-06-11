using FBChecklist.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBChecklist.Services
{
    public class SolarisService : ISolarisService<Solaris>
    {
        private AppEntities appEntities = new AppEntities();


        public string GetServerIp(int AppId)
        {

            var serverIp = (from c in appEntities.Servers
                            where c.ApplicationId == AppId
                            select c.ServerIp).FirstOrDefault();
            return serverIp.ToString();
        }


        public string GetSuperUserPassword(int AppId)
        {
            var password = (from c in appEntities.Credentials
                            where c.ApplicationId == AppId
                            select c.Password).FirstOrDefault();
            return password.ToString();
        }

        public string GetSuperUsername(int AppId)
        {
            var username = (from c in appEntities.Credentials
                            where c.ApplicationId == AppId
                            select c.Username).FirstOrDefault();
            return username.ToString();
        }       

        public void SaveDiskInfo(Solaris entity)
        {
            throw new NotImplementedException();
        }

        public List<Solaris> GetDiskStatistics()
        {
            throw new NotImplementedException();
        }
    }
}