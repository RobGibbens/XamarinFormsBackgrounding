using Foundation;
using UIKit;
using Xamarin.Forms;
using System;
using System.IO;

namespace FormsBackgrounding.iOS
{
	public class CustomSessionDownloadDelegate : NSUrlSessionDownloadDelegate
	{
		string targetFileName;

		public CustomSessionDownloadDelegate (string targetFileName) : base ()
		{
			this.targetFileName = targetFileName;
		}

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
			var sourceFile = location.Path;

			var destFile = downloadTask.OriginalRequest.Url.AbsoluteString.Substring (downloadTask.OriginalRequest.Url.AbsoluteString.LastIndexOf ("/") + 1);

			var message = new DownloadFinishedMessage () {
				FilePath = sourceFile,
				Url = downloadTask.OriginalRequest.Url.AbsoluteString
			};
			MessagingCenter.Send<DownloadFinishedMessage> (message, "DownloadFinishedMessage");
			NSFileManager fileManager = NSFileManager.DefaultManager;


			var documentsFolderPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			NSUrl destinationURL = NSUrl.FromFilename (Path.Combine (documentsFolderPath, destFile));

			NSError error;
			fileManager.Remove (targetFileName, out error);
			bool success = fileManager.Copy (sourceFile, targetFileName, out error);
			if (!success) {
				Console.WriteLine ("Error during the copy: {0}", error.LocalizedDescription);
			}
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
	}
}