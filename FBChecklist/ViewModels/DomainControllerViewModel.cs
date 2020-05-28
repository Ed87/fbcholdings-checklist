using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBChecklist.ViewModels
{
    public class DomainControllerViewModel
    {
        public DomainControllerViewModel(DomainController domainController)
        {
            Domain = domainController.Domain;
            LDAP = domainController.LDAP;
            Username= domainController.Username;
            Password = domainController.Password;
           
        }

        public DomainControllerViewModel()
        {

        }

        public string Domain { get; set; }
        public string LDAP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        
    }
}