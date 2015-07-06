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

			global::Xamarin.Forms.Forms.Init (this, bundle);

            ILongRunningTaskExample longRunningTaskExample = null;
            //ILongRunningTaskExample longRunningTaskExample = new DroidLongRunningTaskExample(this);
            _app = new FormsBackgrounding.App(longRunningTaskExample);
            LoadApplication(_app);

            MessagingCenter.Subscribe<DownloadMessage>(this, "Download", async (message) => {
                StartService(new Intent(this, typeof(DownloaderService)));
            });


            //			App.Current.ConnectionChanged += (sender, e) =>
            //			{
            //				if (e.IsConnected) {
            //					App.Current.Service.Ticked += OnTick;
            //				}
            //				else {
            //					App.Current.Service.Ticked -= OnTick;
            //				}
            //			};
        }

		void OnTick (object sender, TickedEventArgs e)
		{
			var x = e.TickCounter;
		}
	}
}