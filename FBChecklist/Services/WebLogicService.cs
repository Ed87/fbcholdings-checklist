using FBChecklist.Exceptions;
using FBChecklist.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FBChecklist.Services
{
    public class WebLogicService : IAsyncWebLogicService<WebLogic>
    {
       
        private AppEntities appEntities = new AppEntities();

        public string GetWebLogicPassword()
        {
            var password = (from c in appEntities.Credentials
                            join a in appEntities.Applications on c.ApplicationId equals a.ApplicationId
                            select  c.Password ).FirstOrDefault();
            return password.ToString();
        }

        public string GetWebLogicUsername()
        {
            var username = (from c in appEntities.Credentials
                            join a in appEntities.Applications on c.ApplicationId equals a.ApplicationId
                            select c.Username ).FirstOrDefault();
            return username.ToString();
        }

        public string GetWebLogicUrl()
        {
            var url = (from c in appEntities.Applications
                            join a in appEntities.Credentials on c.ApplicationId equals a.ApplicationId
                            select  c.Url).FirstOrDefault();
            return url.ToString();
        }

        public async Task<List<WebLogic>> GetServerStatistics()
        {
            List<WebLogic> model = new List<WebLogic>();
            try
            {
               
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetWebLogicUrl());
                    byte[] cred = Encoding.ASCII.GetBytes(GetWebLogicUsername() + ":" + GetWebLogicPassword());
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(GetWebLogicUrl());

                    if (response.IsSuccessStatusCode)
                    {

                        var serverstat = response.Content.ReadAsStringAsync().Result;
                        Root obj = JsonConvert.DeserializeObject<Root>(serverstat);
                        foreach (WebLogic item in obj.items)
                        {
                           
                            model.Add(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.SendErrorToText(ex);
            }

            return model;
        }


    }
}