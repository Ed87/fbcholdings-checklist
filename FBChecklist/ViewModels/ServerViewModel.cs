using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBChecklist.ViewModels
{
    public class ServerViewModel
    {
        public ServerViewModel(Server server)
        {
            ServerId = server.ServerId;
            CreatedBy = server.CreatedBy;
            ServerIp = server.ServerIp;
            ServerName = server.ServerName;
            ApplicationId = server.ApplicationId;
            IsActive = server.IsActive;
            CreatedOn = server.CreatedOn;
        }

        public ServerViewModel()
        {
        }

        public int ServerId { get; set; }
        public int? IsActive { get; set; }
        public string ServerIp { get; set; }       
        public string ServerName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ApplicationId { get; set; }
        public IEnumerable<SelectListItem> Applications { get; internal set; }

        public void UpdateModel(Server server)
        {
            server.ServerIp = ServerIp;
            server.ApplicationId = ApplicationId;
            server.ServerName = ServerName;
            server.IsActive = IsActive;
            server.CreatedBy = CreatedBy;
            server.CreatedOn = CreatedOn;
        }
    }
}