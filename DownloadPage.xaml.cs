using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding
{
	public partial class DownloadPage : ContentPage
	{
		public DownloadPage ()
		{
			InitializeComponent ();

			MessagingCenter.Subscribe<DownloadProgressMessage> (this, "DownloadProgressMessage", message => {
				Device.BeginInvokeOnMainThread(() => {
					this.downloadStatus.Text = message.Percentage.ToString("P2");
				});
			});

			MessagingCenter.Subscribe<DownloadFinishedMessage> (this, "DownloadFinishedMessage", message => {
				Device.BeginInvokeOnMainThread(() =>
					{
						catImage.Source = FileImageSource.FromFile(message.FilePath);
					});
			});

			downloadButton.Clicked += Download;
		}

		void Download (object sender, EventArgs e)
		{
			this.catImage.Source = null;
			var message = new DownloadMessage {
				Url = "http://xamarinuniversity.blob.core.windows.net/ios210/huge_monkey.png"
			};

			MessagingCenter.Send (message, "Download");
		}
	}
}