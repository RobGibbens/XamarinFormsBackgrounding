using Foundation;
using UIKit;
using Xamarin.Forms;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FormsBackgrounding.iOS
{
	public class Downloader
	{
		/// <summary>
		/// Url of the 5MB monkey PNG file.
		/// </summary>

		/// <summary>
		/// This is where the PNG will be saved to.
		/// </summary>
		public string targetFilename =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "huge_monkey.png");

		/// <summary>
		/// Every session needs a unique identifier.
		/// </summary>
		const string sessionId = "com.xamarin.transfersession";

		/// <summary>
		/// Our session used for transfer.
		/// </summary>
		public NSUrlSession session;

		string _downloadFileUrl;

		public Downloader (string downloadFileUrl)
		{
			this._downloadFileUrl = downloadFileUrl;
			
		}
		public async Task DownloadFile()
		{
			this.InitializeSession ();

			var pendingTasks = await this.session.GetTasksAsync();
			if(pendingTasks != null && pendingTasks.DownloadTasks != null)
			{
				foreach(var task in pendingTasks.DownloadTasks)
				{
					task.Cancel();
				}
			}

			// Delete downloaded file.
			if(File.Exists(targetFilename))
			{
				File.Delete(targetFilename);
			}

			// Update UI.
			//				this.imgView.Image = null;
			//				this.progressView.SetProgress(0, true);
			//				this.btnStartDownload.SetTitle("Start download!", UIControlState.Normal);
			//				this.btnStartDownload.Enabled = true;

			this.EnqueueDownload();
		}

		/// <summary>
		/// Initializes the session.
		/// </summary>
		void InitializeSession()
		{
			// Initialize our session config. We use a background session to enabled out of process uploads/downloads. Note that there are other configurations available:
			// - DefaultSessionConfiguration: behaves like NSUrlConnection. Used if *background* transfer is not required.
			// - EphemeralSessionConfiguration: use if you want to achieve something like private browsing where all sesion info (e.g. cookies) is kept in memory only.
			using (var sessionConfig = UIDevice.CurrentDevice.CheckSystemVersion(8, 0)
				? NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration(sessionId)
				: NSUrlSessionConfiguration.BackgroundSessionConfiguration (sessionId))
			{
				// Allow downloads over cellular network too.
				sessionConfig.AllowsCellularAccess = true;

				// Give the OS a hint about what we are downloading. This helps iOS to prioritize. For example "Background" is used to download data that was not requested by the user and
				// should be ready if the app gets activated.
				sessionConfig.NetworkServiceType = NSUrlRequestNetworkServiceType.Default;

				// Configure how many downloads we allow at the same time. Set to 2 to see that further downloads start once the first two have been completed.
				sessionConfig.HttpMaximumConnectionsPerHost = 2;

				// Create a session delegate and the session itself
				// Initialize the session itself with the configuration and a session delegate.
				var sessionDelegate = new CustomSessionDownloadDelegate (targetFilename);
				this.session = NSUrlSession.FromConfiguration (sessionConfig, sessionDelegate, null);
			}
		}

		/// <summary>
		/// Adds the download to the session.
		/// </summary>
		void EnqueueDownload()
		{
			Console.WriteLine ("Creating new download task.");
			// Create a new download task.
			var downloadTask = this.session.CreateDownloadTask (NSUrl.FromString (_downloadFileUrl));

			// The creation can fail. 
			if (downloadTask == null)
			{
				new UIAlertView (string.Empty, "Failed to create download task! Please retry.", null, "OK").Show ();
				return;
			}

			// Resume / start the download.
			downloadTask.Resume ();
			Console.WriteLine ("Starting download. State of task: '{0}'. ID: '{1}'", downloadTask.State, downloadTask.TaskIdentifier);
		}
	}

}