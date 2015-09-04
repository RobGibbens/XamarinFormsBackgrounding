using System;
using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding
{
	public partial class LongRunningPage : ContentPage
	{
		public LongRunningPage ()
		{
			InitializeComponent ();

			MessagingCenter.Subscribe<TickedMessage> (this, "TickedMessage", message => {
				Device.BeginInvokeOnMainThread(() => {
					ticker.Text = message.Message;
				});
			});

			MessagingCenter.Subscribe<CancelledMessage> (this, "CancelledMessage", message => {
				Device.BeginInvokeOnMainThread(() => {
					ticker.Text = "Cancelled";
				});
			});

			longRunningTask.Clicked += StartLongRunningTask;
			stopLongRunningTask.Clicked += StopLongRunningTask;
		}

		private void StartLongRunningTask (object sender, EventArgs e)
		{
			var message = new StartLongRunningTaskMessage ();
			MessagingCenter.Send (message, "StartLongRunningTaskMessage");
		}

		private void StopLongRunningTask (object sender, EventArgs e)
		{
			var message = new StopLongRunningTaskMessage ();
			MessagingCenter.Send (message, "StopLongRunningTaskMessage");
		}
	}
}