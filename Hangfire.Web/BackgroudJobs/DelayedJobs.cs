using System.Drawing;

namespace Hangfire.Web.BackgroudJobs
{
    public class DelayedJobs
    {

        public static string AddWatermarkJob(string fileName, string watermarkText)
        {
            return BackgroundJob.Schedule<DelayedJobs>(x => x.ApplyWatermark(fileName, watermarkText), TimeSpan.FromSeconds(10));
        }

        public void ApplyWatermark(string fileName,string watermarkText)
        {
          string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pictures", fileName);

            using (var bitmap = Bitmap.FromFile(path) as Bitmap)
            {
                using (Bitmap tempBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                {
                    using (var graphics = Graphics.FromImage(tempBitmap))
                    {
                        graphics.DrawImage(bitmap, 0, 0);
                        using (Font font = new("Arial",100))
                        {
                            using (var brush = new SolidBrush(Color.Red))
                            {
                                graphics.DrawString(watermarkText, font, brush, new PointF(bitmap.Width / 2, bitmap.Height / 2));
                            }
                        }
                    }
                    tempBitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pictures", "watermarked", fileName));

                }
            }
          
        }
    }
}
