using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding.Droid
{
	[Service]
	public class DownloaderService : Service
	{
		public override IBinder OnBind (Intent intent)
		{
			return null;
		}

		public  override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			var url = intent.GetStringExtra ("url");

            Task.Run (() => {
				var imageHelper = new ImageHelper ();
				imageHelper.DownloadImageAsync (url)
                        .ContinueWith (filePath => {
					                        var message = new DownloadFinishedMessage {
						                        FilePath = filePath.Result
					                        };
					                        MessagingCenter.Send (message, "DownloadFinishedMessage");
				                        });
			});

			return StartCommandResult.Sticky;
		}
	}
}