using System.Diagnostics;

namespace Hangfire.Web.BackgroudJobs
{
    public class ReccurringJobs
    {
        public static void ReportingJob()
        {
            Hangfire.RecurringJob.AddOrUpdate("reportjob1", () => EmailReport(), Cron.Minutely);
        }
        public static void EmailReport()
        {
            Debug.WriteLine("Report","Email report job is started");
        }
    }
}
