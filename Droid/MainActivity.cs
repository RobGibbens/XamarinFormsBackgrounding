using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;

namespace FormsBackgrounding.Droid
{
	[Activity (Label = "FormsBackgrounding.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		FormsBackgrounding.App _app;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Forms.Init (this, bundle);

            //ILongRunningTaskExample longRunningTaskExample = null;
            _app = new FormsBackgrounding.App();
            LoadApplication(_app);

            MessagingCenter.Subscribe<DownloadMessage>(this, "Download", message => {
				var intent = new Intent(this, typeof(DownloaderService));
				intent.PutExtra("url", message.Url);
                StartService(intent);
            });
        }

		void OnTick (object sender, TickedEventArgs e)
		{
			var x = e.TickCounter;
		}
	}
}