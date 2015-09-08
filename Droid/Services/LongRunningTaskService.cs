using Android.App;
using Android.Content;
using System.Threading.Tasks;
using Android.OS;
using System.Threading;
using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding.Droid
{
	[Service]
	public class LongRunningTaskService : Service
	{
		CancellationTokenSource _cts;

		public override IBinder OnBind (Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			_cts = new CancellationTokenSource ();

			Task.Run (() => {
				try {
					//INVOKE THE SHARED CODE
					var counter = new TaskCounter();
					counter.RunCounter(_cts.Token).Wait();
				}
				catch (OperationCanceledException) {
				}
				finally {
					if (_cts.IsCancellationRequested) {
						var message = new CancelledMessage();
						Device.BeginInvokeOnMainThread (
							() => MessagingCenter.Send(message, "CancelledMessage")
						);
					}
				}

			}, _cts.Token);

			return StartCommandResult.Sticky;
		}

		public override void OnDestroy ()
		{
			if (_cts != null) {
				_cts.Token.ThrowIfCancellationRequested ();

				_cts.Cancel ();
			}
			base.OnDestroy ();
		}
	}
}