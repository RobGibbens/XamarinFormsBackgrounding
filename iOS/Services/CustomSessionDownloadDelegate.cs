using Foundation;
using Xamarin.Forms;
using System;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding.iOS
{
	public class CustomSessionDownloadDelegate : NSUrlSessionDownloadDelegate
	{
		public override void DidWriteData (NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
		{
			float percentage = (float)totalBytesWritten / (float)totalBytesExpectedToWrite;

			var message = new DownloadProgressMessage () {
				BytesWritten = bytesWritten,
				TotalBytesExpectedToWrite = totalBytesExpectedToWrite,
				TotalBytesWritten = totalBytesWritten,
				Percentage = percentage
			};

			MessagingCenter.Send<DownloadProgressMessage> (message, "DownloadProgressMessage");
		}

		public override void DidFinishDownloading (NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			CopyDownloadedImage (location);

			var message = new DownloadFinishedMessage () {
				FilePath = targetFileName,
				Url = downloadTask.OriginalRequest.Url.AbsoluteString
			};

			MessagingCenter.Send<DownloadFinishedMessage> (message, "DownloadFinishedMessage");

		}

		#region Methods
		readonly string targetFileName;

		public CustomSessionDownloadDelegate (string targetFileName) : base ()
		{
			this.targetFileName = targetFileName;
		}


		public override void DidCompleteWithError (NSUrlSession session, NSUrlSessionTask task, NSError error)
		{
			if (error == null) {
				return;
			}

			Console.WriteLine ("DidCompleteWithError - Task: {0}, Error: {1}", task.TaskIdentifier, error);

			task.Cancel ();
		}

		public override void DidFinishEventsForBackgroundSession (NSUrlSession session)
		{
			var handler = AppDelegate.BackgroundSessionCompletionHandler;
			AppDelegate.BackgroundSessionCompletionHandler = null;
			if (handler != null) {
				handler.Invoke ();
			}
		}

		private void CopyDownloadedImage (NSUrl location)
		{
			NSFileManager fileManager = NSFileManager.DefaultManager;
			NSError error;
			if (fileManager.FileExists (targetFileName)) {
				fileManager.Remove (targetFileName, out error);
			}
			bool success = fileManager.Copy (location.Path, targetFileName, out error);
			if (!success) {
				Console.WriteLine ("Error during the copy: {0}", error.LocalizedDescription);
			}
		}

		#endregion
	}
}