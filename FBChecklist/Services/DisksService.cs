using FBChecklist.Common;
using FBChecklist.Exceptions;
using FBChecklist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    public class DisksService : IDisksService<Disk>
    {
        private AppEntities appEntities = new AppEntities();



        public string GetServerId(int AppId)
        {
           
            var serverId = (from c in appEntities.Servers
                            where c.ApplicationId==AppId
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


        public string GetApplicationName(int AppId)
        {
           
            var appname = (from c in appEntities.Applications
                            where c.ApplicationId == AppId
                            select c.ApplicationName).FirstOrDefault();
            return appname.ToString();
        }

        public int CheckForClusteredDisks(string serverIp)
        {
            var cluster = (from c in appEntities.Servers
                           where c.ServerIp == serverIp
                           select c.HasClusteredDisks).FirstOrDefault();
            int isClustered = Convert.ToInt32(cluster);
            return isClustered ;
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



        public void GetApps(DiskViewModel model)
        {
            model.Applications = GetApplications();
        }

        public IEnumerable<SelectListItem> GetApplications()
        {
            using (var db = new AppEntities())
            {
                List<SelectListItem> applications = db.Applications.AsNoTracking()
                    .OrderBy(n => n.ApplicationName)
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


        public void SaveEnvironmentInfo(Disk entity)
        {
          
            List<Disk> DiskInfo = new List<Disk>();
            DiskInfo = GetEnvironmentStatistics(); 
            foreach(var di in DiskInfo)
            {
                 entity.DiskName =di.DiskName ;
                 entity.FreeSpace = di.FreeSpace;
                 entity.TotalSpace = di.TotalSpace;
                 entity.UsedSpace = di.UsedSpace;
                 entity.PercentageUsed = di.PercentageUsed;
                 entity.RunDate = DateTime.Now;
                 //entity.CPU = di.CPU;                
                 entity.ServerId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ServerId"]);
                 entity.ApplicationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedApp"]);
                 appEntities.Disks.Add(entity);
                 appEntities.SaveChanges();
            }
           
        }

        public List<Disk> GetEnvironmentStatistics()
        {

            List<Disk> diskinfo = new List<Disk>();

            var serverIP = Convert.ToString(System.Web.HttpContext.Current.Session["ServerIP"]);
            var clusterDisksStatus = CheckForClusteredDisks(serverIP);

            if (Helpers.HasClusteredDisks(clusterDisksStatus))
            {
                string getClusterSharedVolumesStatistics = "get-WmiObject win32_logicaldisk -Computername " + serverIP + "";
                PowerShell ps = PowerShell.Create();              
                ps.AddScript(getClusterSharedVolumesStatistics);
                var results = ps.Invoke();

                foreach (var psobject in results)
                {

                    if (psobject != null)
                    {
                        Disk clusteredDisk = new Disk();
                        clusteredDisk.DiskName = Convert.ToString(psobject.Members["DeviceID"].Value);

                        clusteredDisk.FreeSpace = Convert.ToDecimal(psobject.Members["FreeSpace"].Value);
                        var formattedFreeSpace = Helpers.DiskSpaceInGigabytes(clusteredDisk.FreeSpace ?? 0);
                        clusteredDisk.FreeSpace = Decimal.Truncate(formattedFreeSpace);


                        clusteredDisk.TotalSpace = Convert.ToDecimal(psobject.Members["Size"].Value);
                        var formattedTotalSpace = Helpers.DiskSpaceInGigabytes(clusteredDisk.TotalSpace ?? 0);
                        clusteredDisk.TotalSpace = Decimal.Truncate(formattedTotalSpace);

                        clusteredDisk.UsedSpace = clusteredDisk.TotalSpace - clusteredDisk.FreeSpace;
                        clusteredDisk.VolumeName = Convert.ToString(psobject.Members["VolumeName"].Value);

                        diskinfo.Add(clusteredDisk);
                    }

                  

                }
            }

            else
            {

                //Add System.Management to access these utilities
                ConnectionOptions options = new ConnectionOptions
                {

                    Username = Convert.ToString(System.Web.HttpContext.Current.Session["Username"]),
                    Password = Convert.ToString(System.Web.HttpContext.Current.Session["Password"]),
                    Authority = Convert.ToString(System.Web.HttpContext.Current.Session["Authority"]),
                };

                //root - root of the tree, cimv2 - version           
                ManagementScope scope = new ManagementScope("\\\\" + serverIP + "\\root\\CIMV2", options);
                scope.Connect();

                SelectQuery query = new SelectQuery("Select * from Win32_LogicalDisk");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();

                try
                {

                    foreach (ManagementObject mo in queryCollection)
                    {

                       
                        Disk disk = new Disk();
                        disk.DiskName = mo["Name"].ToString();
                        disk.DeviceId = mo["DeviceID"].ToString();
                        disk.SystemName = mo["SystemName"].ToString();

                        disk.FreeSpace = Convert.ToDecimal(mo["FreeSpace"]);
                        var formattedFreeSpace = Helpers.DiskSpaceInGigabytes(disk.FreeSpace ?? 0);
                        disk.FreeSpace = Decimal.Truncate(formattedFreeSpace);


                        disk.TotalSpace = Convert.ToDecimal(mo["Size"]);
                        var formattedTotalSpace = Helpers.DiskSpaceInGigabytes(disk.TotalSpace ?? 0);
                        disk.TotalSpace = Decimal.Truncate(formattedTotalSpace);

                        disk.UsedSpace = disk.TotalSpace - disk.FreeSpace;

                        var HDPercentageUsed = 100 - (100 * disk.FreeSpace / disk.TotalSpace);
                        disk.PercentageUsed = Convert.ToInt32(HDPercentageUsed);
                        diskinfo.Add(disk);
                    }
                }

                catch (DivideByZeroException ex)
                {

                    ExceptionLogger.SendErrorToText(ex);
                }

            }
            return diskinfo;
        }
      
    }
}