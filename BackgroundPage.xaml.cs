using System;
using Xamarin.Forms;

namespace FormsBackgrounding
{
	public partial class BackgroundPage : ContentPage
	{
		ILongRunningTaskExample _longRunningTaskExample;

		public BackgroundPage (ILongRunningTaskExample longRunningTaskExample)
		{
			_longRunningTaskExample = longRunningTaskExample;

			InitializeComponent ();

			longRunningTask.Clicked += StartLongRunningTask;
			download.Clicked += Download;

			MessagingCenter.Subscribe<DownloadProgressMessage> (this, "DownloadProgressMessage", (message) => {
				Device.BeginInvokeOnMainThread(() => {
					this.downloadStatus.Text = message.Percentage.ToString("P2");
				});
			});

			MessagingCenter.Subscribe<DownloadFinishedMessage> (this, "DownloadFinishedMessage", (message) => {
				Device.BeginInvokeOnMainThread(() =>
				{
				    catImage.Source = FileImageSource.FromFile(message.FilePath);
					//this.downloadStatus.Text = message.FilePath;
				});
			});
		}

		async void StartLongRunningTask (object sender, EventArgs e)
		{
			_longRunningTaskExample.Ticked += (s1, e1) => {
				ticker.Text = e1.TickCounter.ToString ();
			};

			await _longRunningTaskExample.Start ();
		}

		void Download (object sender, EventArgs e)
		{
		    this.catImage.Source = null;
			var message = new DownloadMessage {
				Url = "http://xamarinuniversity.blob.core.windows.net/ios210/huge_monkey.png"
			};

			MessagingCenter.Send<DownloadMessage> (message, "Download");
		}
	}
}