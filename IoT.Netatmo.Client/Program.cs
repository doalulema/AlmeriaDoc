using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;

namespace IoT.Netatmo.Client
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }

        static void Main()
        {
            BuildConfiguration();
            ScheduleJob();
            
            Console.WriteLine("Press ENTER to shutdown the app.");
            Console.ReadLine();
        }

        private static void ScheduleJob()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<DataJob>()
                .WithIdentity("job1", "group1")
                .Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(int.Parse(Configuration["periodMins"]))
                    .RepeatForever())
                .Build();
            
            scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();
        }

        private static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
    }
}
