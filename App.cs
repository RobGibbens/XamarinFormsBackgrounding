using System;

using Xamarin.Forms;

namespace FormsBackgrounding
{
	public class App : Application
	{
		public App (ILongRunningTaskExample longRunningTaskExample)
		{
			// The root page of your application
			MainPage = new BackgroundPage(longRunningTaskExample);
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

