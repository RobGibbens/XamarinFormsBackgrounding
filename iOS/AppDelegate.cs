using Foundation;
using UIKit;
using Xamarin.Forms;
using System;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		iOSLongRunningTaskExample longRunningTaskExample;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			LoadApplication (new App ());

			WireUpDownloadTask ();
			WireUpLongRunningTask ();

			return base.FinishedLaunching (app, options);
		}

		void WireUpLongRunningTask ()
		{
			MessagingCenter.Subscribe<StartLongRunningTaskMessage> (this, "StartLongRunningTaskMessage", async message =>  {
				longRunningTaskExample = new iOSLongRunningTaskExample ();
				await longRunningTaskExample.Start ();
			});
			MessagingCenter.Subscribe<StopLongRunningTaskMessage> (this, "StopLongRunningTaskMessage", message =>  {
				longRunningTaskExample.Stop ();
			});
		}

		void WireUpDownloadTask ()
		{
			MessagingCenter.Subscribe<DownloadMessage> (this, "Download", async message =>  {
				var downloader = new Downloader (message.Url);
				await downloader.DownloadFile ();
			});
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