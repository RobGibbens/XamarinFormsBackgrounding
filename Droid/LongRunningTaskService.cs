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
		CancellationTokenSource _cts = new CancellationTokenSource ();


		public override IBinder OnBind (Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			_cts = new CancellationTokenSource ();
			_cts.Token.ThrowIfCancellationRequested ();

			try {
				Task.Run (() => {
					for (long i = 0; i < long.MaxValue; i++) {
						_cts.Token.ThrowIfCancellationRequested ();

						Thread.Sleep(1000);
						var message = new TickedMessage { 
							Message = i.ToString ()
						};

						Android.App.Application.SynchronizationContext.Post (_ => {
							MessagingCenter.Send<TickedMessage> (message, "TickedMessage");
						}, null);

					}
				}, _cts.Token);

			} catch (System.OperationCanceledException opEx) {
				var message = new TickedMessage { 
					Message = "Cancelled"
				};
				Android.App.Application.SynchronizationContext.Post (_ => {
					MessagingCenter.Send<TickedMessage> (message, "TickedMessage");
				}, null);
			}
			return StartCommandResult.Sticky;
		}

		public override void OnDestroy ()
		{
			_cts.Cancel ();
			base.OnDestroy ();
		}

	}
}