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
			global::Xamarin.Forms.Forms.Init ();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			ILongRunningTaskExample longRunningTaskExample = new iOSLongRunningTaskExample ();
			LoadApplication (new App (longRunningTaskExample));

			MessagingCenter.Subscribe<DownloadMessage> (this, "Download", async (message) => {
				var downloader = new Downloader(message.Url);
				await downloader.DownloadFile();
			});

			return base.FinishedLaunching (app, options);
		}



		/// <summary>
		/// We have to call this if our transfer (of all files!) is done.
		/// </summary>
		public static Action BackgroundSessionCompletionHandler;

		/// <summary>
		/// Gets called by iOS if we are required to handle something regarding our background downloads.
		/// </summary>
		public override void HandleEventsForBackgroundUrl (UIApplication application, string sessionIdentifier, Action completionHandler)
		{
			Console.WriteLine ("HandleEventsForBackgroundUrl(): " + sessionIdentifier);
			// We get a completion handler which we are supposed to call if our transfer is done.
			BackgroundSessionCompletionHandler = completionHandler;
		}
	}



}