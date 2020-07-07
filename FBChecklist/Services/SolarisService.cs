using FBChecklist.Common;
using FBChecklist.ViewModels;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        public string GetServerId(int AppId)
        {

            var serverId = (from c in appEntities.Servers
                            where c.ApplicationId == AppId
                            select c.ServerId).FirstOrDefault();
            return serverId.ToString();
        }


        public string GetSuperUsername(int AppId)
        {
            var username = (from c in appEntities.Credentials
                            where c.ApplicationId == AppId
                            select c.Username).FirstOrDefault();
            return username.ToString();
        }


        public void GetApps(SolarisViewModel model)
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


        public void SaveDiskInfo(Solaris entity)
        {
            List<Solaris> DiskInfo = new List<Solaris>();
            DiskInfo = GetDiskStatistics();
            foreach (var di in DiskInfo)
            {
                entity.Filesystem = di.Filesystem;
                entity.Blocks = di.Blocks;
                entity.Mount = di.Mount;
                entity.Available = di.Available;
                entity.Capacity = di.Capacity;
                entity.Used = di.Used;
                entity.RunDate = DateTime.Now;                            
                entity.ApplicationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedApp"]);
                entity.ServerId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ServerId"]);
                appEntities.Solaris.Add(entity);
                appEntities.SaveChanges();
            }
        }

        public List<Solaris> GetDiskStatistics()
        {
            List<Solaris> diskinfo = new List<Solaris>();
           
            var serverIP = Convert.ToString(System.Web.HttpContext.Current.Session["ServerIP"]);
            var username = Convert.ToString(System.Web.HttpContext.Current.Session["Username"]);
            var password = Convert.ToString(System.Web.HttpContext.Current.Session["Password"]);

            try
            {

                using (SshClient ssh = new SshClient(serverIP,
                username, password))
                {
                    ssh.Connect();
                    var result = ssh.RunCommand("df -k");
                    var rss = result.Result;
                    string[] lines = rss.Split('\n');
                    lines = lines.Skip(1).ToArray();
                    Int32 count = 6;
                    String[] separator = { " " };

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] strlist = lines[i].Split(separator, count,
                        StringSplitOptions.RemoveEmptyEntries).ToArray();

                        Solaris disk = new Solaris();

                        disk.Filesystem = strlist.GetValue(0).ToString();

                        disk.Blocks = Convert.ToDecimal(strlist[1]);
                        var formattedBlocksSpace = disk.Blocks ?? 0;
                        disk.Blocks = formattedBlocksSpace;

                        disk.Used = Convert.ToDecimal(strlist[2]);
                        var formattedUsedSpace = disk.Used ?? 0;
                        disk.Used = formattedUsedSpace;

                        disk.Available = Convert.ToDecimal(strlist[3]);
                        var formattedAvailableSpace = disk.Available ?? 0;
                        disk.Available = formattedAvailableSpace;

                        disk.Capacity = strlist[4];
                        disk.Mount = strlist[5];
                        diskinfo.Add(disk);
                    }

                    ssh.Disconnect();
                }
            }

            catch (Exception e)
            {
                e.ToString();

            }
            return diskinfo;
        }
    }
}