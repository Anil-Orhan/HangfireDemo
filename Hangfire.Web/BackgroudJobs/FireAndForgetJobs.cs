using Hangfire.Web.Services;

namespace Hangfire.Web.BackgroudJobs
{
    public class FireAndForgetJobs
    {
        public static void EmailSendToUserJob(string email, string subject, string message)
        {
            BackgroundJob.Enqueue<IEmailSender>(x=> x.SendEmailAsync(email,subject,message));
        }
    }
}
