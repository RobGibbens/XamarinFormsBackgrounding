using Foundation;
using UIKit;
using Xamarin.Forms;
using System;

namespace FormsBackgrounding.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			ILongRunningTaskExample longRunningTaskExample = new iOSLongRunningTaskExample ();
			LoadApplication (new App (longRunningTaskExample));

			MessagingCenter.Subscribe<DownloadMessage> (this, "Download", async (message) => {
				var downloader = new Downloader(message.Url);
				await downloader.DownloadFile();
			});

			return base.FinishedLaunching (app, options);
		}

		public static Action BackgroundSessionCompletionHandler;

		public override void HandleEventsForBackgroundUrl (UIApplication application, string sessionIdentifier, Action completionHandler)
		{
			Console.WriteLine ("HandleEventsForBackgroundUrl(): " + sessionIdentifier);
			// We get a completion handler which we are supposed to call if our transfer is done.
			BackgroundSessionCompletionHandler = completionHandler;
		}
	}
}