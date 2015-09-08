using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding
{
	public partial class LongRunningPage : ContentPage
	{
		public LongRunningPage ()
		{
			InitializeComponent ();

			//Wire up XAML buttons
			longRunningTask.Clicked += (s, e) => {
				var message = new StartLongRunningTaskMessage ();
				MessagingCenter.Send (message, "StartLongRunningTaskMessage");
			};

			stopLongRunningTask.Clicked += (s, e) => {
				var message = new StopLongRunningTaskMessage ();
				MessagingCenter.Send (message, "StopLongRunningTaskMessage");
			};
				
			HandleReceivedMessages ();
		}
			

		void HandleReceivedMessages ()
		{
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
		}
	}
}