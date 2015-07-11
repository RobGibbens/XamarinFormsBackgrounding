using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding.Droid
{
	[Activity (Label = "FormsBackgrounding.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Forms.Init (this, bundle);

			LoadApplication(new App());

			WireUpLongRunningTask ();
			WireUpLongDownloadTask ();
        }

		void WireUpLongRunningTask()
		{
			MessagingCenter.Subscribe<StartLongRunningTaskMessage> (this, "StartLongRunningTaskMessage", message =>  {
				var intent = new Intent(this, typeof(LongRunningTaskService));
				StartService(intent);
			});

			MessagingCenter.Subscribe<StopLongRunningTaskMessage> (this, "StopLongRunningTaskMessage", message =>  {
				var intent = new Intent(this, typeof(LongRunningTaskService));
				StopService(intent);
			});
		}

		void WireUpLongDownloadTask ()
		{
			MessagingCenter.Subscribe<DownloadMessage> (this, "Download", message =>  {
				var intent = new Intent (this, typeof(DownloaderService));
				intent.PutExtra ("url", message.Url);
				StartService (intent);
			});
		}
	}
}