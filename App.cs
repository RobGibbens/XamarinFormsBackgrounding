using Xamarin.Forms;

namespace FormsBackgrounding
{
	public class App : Application
	{
		BackgroundPage _backgroundPage;

		public App ()
		{
			_backgroundPage = new BackgroundPage ();

			MainPage = _backgroundPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}