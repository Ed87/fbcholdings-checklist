using FBChecklist.Common;
using FBChecklist.Exceptions;
using FBChecklist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    public class ServicesRepository : IServicesRepository
    {
        private AppEntities appEntities = new AppEntities();

        public void AddService(Service entity)
        {
            entity.ServerId = Convert.ToInt32(HttpContext.Current.Session["ServerId"]);
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = "Admin";
            appEntities.Services.Add(entity);
            appEntities.SaveChanges();
        }

        public List<string> GetApplicationServices(int AppId)
        {
          
            //    dbContext.Configuration.UseDatabaseNullSemantics = true;
            var services = (from c in appEntities.Services
                              where c.ApplicationId == AppId
                              select c.ShortName).ToList();
            
            return services;
        }

        

        public string GetServerId(int AppId)
        {

            var serverId = (from c in appEntities.Servers
                            where c.ApplicationId == AppId
                            select c.ServerId).FirstOrDefault();
            return serverId.ToString();
        }

        public string GetServerIp(int AppId)
        {

            var serverIp = (from c in appEntities.Servers
                            where c.ApplicationId == AppId
                            select c.ServerIp).FirstOrDefault();
            return serverIp.ToString();
        }

        public string GetAuthority(int AppId)
        {
            var authority = (from c in appEntities.Credentials
                             where c.ApplicationId == AppId
                             select c.Reference).FirstOrDefault();
            return authority.ToString();
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



        public void AddServiceMonitor(ServiceMonitor entity)
        {
            var AppId = Convert.ToInt32(HttpContext.Current.Session["AppId"]);

            List<string> services = (from c in appEntities.Services
                                     where c.ApplicationId == AppId
                                     select c.ServiceName).ToList();

            foreach (var service in services)
            {
                
                if (Helpers.IsServiceRunning(service))
                {
                    entity.RunDate = DateTime.Now;
                    entity.CreatedBy = "Admin";
                    appEntities.ServiceMonitors.Add(entity);
                    appEntities.SaveChanges();

                }
            }
        }

        public IEnumerable<Service> GetAll()
        {
            return appEntities.Services.ToList();
        }

        public IEnumerable<SelectListItem> GetApplications()
        {
            using (var db = new AppEntities())
            {
                List<SelectListItem> applications = db.Applications.AsNoTracking()
                    .OrderBy(n => n.ApplicationName)
                      .Where(n => n.HasServices == Helpers.parameters.HasServices)
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

        internal void GetApps(ServiceMonitorViewModel model)
        {
            model.Applications = GetApplications();
        }

        public void GetApps(ServicesViewModel model)
        {
            model.Applications = GetApplications();
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

        public List<ServiceMonitor> GetServicesStatus()
        {

            List<ServiceMonitor> serviceinfo = new List<ServiceMonitor>();

            List<string> services = (List<string>)HttpContext.Current.Session["Services"];

            var serverIP = Convert.ToInt32(HttpContext.Current.Session["ServiceIP"]);
                      
            try
            {

                foreach (var service in services)
                {
                    var serviceIds = (from c in appEntities.Services
                                     where c.ShortName == service
                                     select c.ServiceId).ToList();

                    var svc = new ServiceMonitor();

                    svc.Status =Helpers.GetServiceState(service).ToString();
                    svc.ServiceName = service;  
                    
                    foreach (var svcId in serviceIds)
                    {
                        svc.ServiceId = svcId;
                    }
                  
                    serviceinfo.Add(svc);
                }

                }

            catch (Exception ex)
            {

                ExceptionLogger.SendErrorToText(ex);
            }

            
            return serviceinfo;
        }

        public void SaveServicesInfo(ServiceMonitor entity)
        {

            List<ServiceMonitor> ServiceInfo = new List<ServiceMonitor>();

            ServiceInfo = GetServicesStatus();

            foreach (var si in ServiceInfo)
            {
                entity.ServiceId = si.ServiceId;
                entity.Status = si.Status;
                entity.ServiceName = si.ServiceName;         
                entity.RunDate = DateTime.Now;
                entity.CreatedBy = "Admin";
                entity.ServerId = Convert.ToInt32(HttpContext.Current.Session["ServerId"]);
                entity.ApplicationId = Convert.ToInt32(HttpContext.Current.Session["SelectedApplication"]);
                appEntities.ServiceMonitors.Add(entity);
                appEntities.SaveChanges();
            }

        }


        public void GetServers(ServicesViewModel model)
        {
            model.Servers= PopulateServers();
        }

     
    }
}