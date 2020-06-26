using System;
using System.Collections.Generic;
using System.Management;
using System.Linq;
using System.Web;
using FBChecklist.Exceptions;

namespace FBChecklist.Services
{
    public class BizTalkService : IBizTalkService<BizTalk>
    {
        private AppEntities appEntities = new AppEntities();


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

        

        /// <summary>
        /// Enumerates installed RLs i.e
        ///  shows how you can enumerate thru one of
		///	 the BizTalk's WMI objects and show all of its properties
        /// </summary>
        public List<BizTalk> GetBizTalkServicesStatistics()
        {

           // var serverID = Convert.ToString(System.Web.HttpContext.Current.Session["ServerIP"]);

            List<BizTalk> model = new List<BizTalk>();

            try
            {
                //Create the WMI search object.
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher();

                ConnectionOptions options = new ConnectionOptions
                {

                    Username = "tshumae",
                    Password = "@Ert2020",
                    Authority = "ntlmdomain:fbc.corp"
                };

                
                var server = "FBCDCZIMSTP01";
                // create the scope node so we can set the WMI root node correctly.
                ManagementScope Scope = new ManagementScope("\\\\" + server + "\\root\\MicrosoftBizTalkServer", options);
                Searcher.Scope = Scope;

                // Build a Query to enumerate the MSBTS_ReceiveLocation instances if an argument
                // is supplied use it to select only the matching RL.

                    //if (args.Length == 0)
                    SelectQuery Query = new SelectQuery();               
                    Query.QueryString = "SELECT * FROM MSBTS_ReceiveLocation";
                    //          else
					//Query.QueryString = "SELECT * FROM MSBTS_ReceiveLocation WHERE Name = '" + args[0] + "'";


                // Set the query for the searcher.
                Searcher.Query = Query;

                // Execute the query and determine if any results were obtained.
                ManagementObjectCollection QueryCol = Searcher.Get();

                // Use a bool to tell if we enter the for loop
                // below because Count property is not supported
                bool ReceiveLocationFound = false;

                // Enumerate all properties.
                foreach (ManagementBaseObject envVar in QueryCol)
                {
                    // There is at least one Receive Location
                    ReceiveLocationFound = true;

                    PropertyDataCollection envVarProperties = envVar.Properties;
                   
                    foreach (PropertyData envVarProperty in envVarProperties)
                    {
                        BizTalk bizTalk = new BizTalk();
                        bizTalk.Name = Convert.ToString(envVar["Name"]);
                        bizTalk.TransportType = Convert.ToString(envVar["AdapterName"]);
                        bizTalk.Uri = Convert.ToString(envVar["InboundTransportURL"]);
                        bizTalk.Status = Convert.ToString(envVar["Name"]);
                        bizTalk.ReceiveHandler = Convert.ToString(envVar["HostName"]);
                        bizTalk.ReceivePort = Convert.ToString(envVar["ReceivePortName"]);
                        bizTalk.RunDate = DateTime.Now;
                        bizTalk.ApplicationId = 24;
                        bizTalk.ServerId = 8;
                        bizTalk.InstanceName = "FBCZOP";                    
                        //model.Insert(0, header);
                        model.Add(bizTalk);
                        
                    }
                }

                if (!ReceiveLocationFound)
                {
                    Console.WriteLine("No receive locations found matching the specified name.");
                }
            }

            catch (Exception excep)
            {
                ExceptionLogger.SendErrorToText(excep);
            }

            return model;
        }

        public void SaveStatistics(BizTalk entity)
        {
            List<BizTalk> DiskInfo = new List<BizTalk>();
            DiskInfo = GetBizTalkServicesStatistics();
            foreach (var di in DiskInfo)
            {
                entity.RunDate = di.RunDate;
                entity.Name = di.Name;
                entity.Status = di.Status;
                entity.Uri = di.Uri;
                entity.InstanceName = di.InstanceName;
                entity.ReceivePort = di.ReceivePort;
                entity.TransportType= di.TransportType;
                entity.RunDate = DateTime.Now;
                entity.ReceiveHandler = di.ReceiveHandler;                
                entity.ServerId = entity.ServerId;
                entity.ApplicationId = entity.ApplicationId;
                appEntities.BizTalk.Add(entity);
                appEntities.SaveChanges();
            }

        }

      
    }
}