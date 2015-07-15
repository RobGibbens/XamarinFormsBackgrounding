using Android.App;
using Android.Content;
using System.Threading.Tasks;
using Android.OS;
using System.Threading;
using Xamarin.Forms;
using FormsBackgrounding.Messages;
using System;

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
			_cts.Token.ThrowIfCancellationRequested ();

			try {
				Task.Run (() => {
					_cts.Token.ThrowIfCancellationRequested ();

					for (long i = 0; i < long.MaxValue; i++) {
						Thread.Sleep(250);
						var message = new TickedMessage {
							Message = i.ToString ()
						};

						Android.App.Application.SynchronizationContext.Post (_ => {
							MessagingCenter.Send (message, "TickedMessage");
						}, null);
					}

				}, _cts.Token);

			} catch (System.OperationCanceledException opEx) {
				var message = new TickedMessage {
					Message = "Cancelled"
				};

				Android.App.Application.SynchronizationContext.Post (_ => {
					MessagingCenter.Send (message, "TickedMessage");
				}, null);
			} catch (Exception ex) {
				var ssds = ex.Message;
			}
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