using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Component.Quartz
{
    class Program
    {
        static void Main(string[] args)
        {
            // initial Scheduler
            IScheduler sched;
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();

            // set job (IndexJob : which job.)
            JobDetail job = new JobDetail("job1", "group1", typeof(IndexJob));

            // After 5s the applcaition start,  quartz will start.
            DateTime ts = TriggerUtils.GetNextGivenSecondDate(null, 5);

            // get interval time for quartz (a per hour )
            TimeSpan interval = TimeSpan.FromSeconds(5);

            // set trigger
            Trigger trigger = new SimpleTrigger("trigger1", "group1", "job1", "group1", ts, null,
                                                    SimpleTrigger.RepeatIndefinitely, interval);
            // add job to Scheduler,
            sched.AddJob(job, true);

            // add trigger to Scheduler
            sched.ScheduleJob(trigger);

            // Scheduler will start to work.
            sched.Start();
            // will close quartz (sched.Shutdown(true));
        }
    }
}
