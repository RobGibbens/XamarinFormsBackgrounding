using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsBackgrounding.Droid
{
    [Service]
    public class DownloaderService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Task.Run(() =>
            {
                for (long i = 0; i < long.MaxValue; i++)
                {
                    var message = new DownloadProgressMessage()
                    {
                        BytesWritten = i,
                        TotalBytesExpectedToWrite = i,
                        TotalBytesWritten = i,
                        Percentage = i
                    };
                    MessagingCenter.Send<DownloadProgressMessage>(message, "DownloadProgressMessage");
                }
            });

            return StartCommandResult.Sticky;
        }
    }
}