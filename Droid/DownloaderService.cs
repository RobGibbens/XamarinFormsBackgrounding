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

        public  override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
			var url = intent.GetStringExtra ("url");
			Task.Run (() => {
				ImageHelper.DownloadImageAsync (url).ContinueWith((filePath) =>{
					var message = new DownloadFinishedMessage {
						FilePath = filePath.Result
					};
					//Android.App.Application.SynchronizationContext.Post (_ => {
						MessagingCenter.Send<DownloadFinishedMessage>(message, "DownloadFinishedMessage");
					
					//}, null);
				});


			});
//            Task.Run(() =>
//            {
//                for (long i = 0; i < long.MaxValue; i++)
//                {
//                    var message = new DownloadProgressMessage()
//                    {
//                        BytesWritten = i,
//                        TotalBytesExpectedToWrite = i,
//                        TotalBytesWritten = i,
//                        Percentage = i / 100
//                    };
//                    MessagingCenter.Send<DownloadProgressMessage>(message, "DownloadProgressMessage");
//                }
//            });

            return StartCommandResult.Sticky;
        }
    }
}