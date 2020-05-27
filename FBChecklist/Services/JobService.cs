using FBChecklist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    public class JobService : IJobsService<Job>
    {
        private AppEntities appEntities = new AppEntities();

        public void AddJob(Job entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = "TshumaE";
            appEntities.Jobs.Add(entity);
            appEntities.SaveChanges();
        }

        public void EditJob(Job dbentity, Job entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> GetAll()
        {
            //var frequency = (from j in appEntities.Jobs join f in appEntities.Frequencies
            //                 on j.FrequencyId equals f.FrequencyId
            //                 select new
            //                 {
            //                     f.FrequencyText
            //                 });
       // appEntities.Jobs.Add(frequency);
         return   appEntities.Jobs.ToList();
        }

        public IEnumerable<SelectListItem> GetFrequencies()
        {
            using (var db = new AppEntities())
            {
                List<SelectListItem> frequencies = db.Frequencies.AsNoTracking()
                    .OrderBy(n => n.FrequencyText)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.FrequencyId.ToString(),
                            Text = n.FrequencyText
                        }).ToList();
                var apptip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select frequency ---"
                };
                frequencies.Insert(0, apptip);
                return new SelectList(frequencies, "Value", "Text");
            }
        }

        public void GetJobFrequencies(JobViewModel model)
        {
            model.Frequencies = GetFrequencies();
        }

        public Job GetServiceById(int id)
        {
            throw new NotImplementedException();
        }
    }
}