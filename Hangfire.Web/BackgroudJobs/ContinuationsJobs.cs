using System.Diagnostics;

namespace Hangfire.Web.BackgroudJobs
{
    public class ContinuationsJobs
    {

        public static void WriteLogJob(string jobId,string message)
        {
            BackgroundJob.ContinueJobWith(jobId, ()=> Debug.WriteLine($"{DateTime.Now} - {message}"));
        }
    }
}
