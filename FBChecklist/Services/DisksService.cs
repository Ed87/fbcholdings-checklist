using FBChecklist.Common;
using FBChecklist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
                 entity.CPU = di.CPU;                
                 entity.ServerId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ServerId"]);
                 entity.ApplicationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedApp"]);
                 appEntities.Disks.Add(entity);
                 appEntities.SaveChanges();
            }
           
        }

        public List<Disk> GetEnvironmentStatistics()
        {
            Disk disk = new Disk();
            List<Disk> diskinfo = new List<Disk>();

            //Add System.Management to access these utilities
            ConnectionOptions options2 = new ConnectionOptions();
           
            //root - root of the tree, cimv2 - version           
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\CIMV2", options2);
            scope.Connect();

            SelectQuery query = new SelectQuery("Select * from Win32_LogicalDisk");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject mo in queryCollection)
            {
               
                disk.DiskName = mo["Name"].ToString();
                disk.VolumeName = mo["VolumeName"].ToString();
                disk.DeviceId = mo["DeviceID"].ToString();
                disk.SystemName = mo["SystemName"].ToString();

                disk.FreeSpace = Convert.ToDecimal(mo["FreeSpace"]);       
                var formattedFreeSpace = Helpers.DiskSpaceInGigabytes(disk.FreeSpace?? 0);
                disk.FreeSpace = Decimal.Truncate(formattedFreeSpace);

                disk.TotalSpace = Convert.ToDecimal(mo["Size"]);
                var formattedTotalSpace = Helpers.DiskSpaceInGigabytes(disk.TotalSpace ?? 0);
                disk.TotalSpace = Decimal.Truncate(formattedTotalSpace);

                disk.UsedSpace = disk.TotalSpace - disk.FreeSpace;

                var HDPercentageUsed = 100 - (100 * disk.FreeSpace / disk.TotalSpace);
                disk.PercentageUsed = Convert.ToInt32(HDPercentageUsed);
                diskinfo.Add(disk);
            }

            var winQuery = new ObjectQuery("select * from Win32_PerfFormattedData_PerfOS_Processor");
            var searcherr = new ManagementObjectSearcher(winQuery);

            foreach (var item in searcherr.Get())
            {
               
                disk.CPU = Convert.ToString(item["PercentProcessorTime"]);           
                diskinfo.Add(disk);
            }

            return diskinfo;
        }

      
    }
}