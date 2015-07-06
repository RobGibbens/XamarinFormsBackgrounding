using System;
using UIKit;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using FormsBackgrounding.Messages;

namespace FormsBackgrounding.iOS
{
	public class iOSLongRunningTaskExample
	{
		nint _taskId;
		CancellationTokenSource _cts = new CancellationTokenSource ();

		public async Task Start ()
		{
			_cts = new CancellationTokenSource ();
			_cts.Token.ThrowIfCancellationRequested ();

			_taskId = UIApplication.SharedApplication.BeginBackgroundTask ("LongRunningTask", OnExpiration);

			try {

				await Task.Run (() => {

					for (long i = 0; i < long.MaxValue; i++) {
						_cts.Token.ThrowIfCancellationRequested ();

						var message = new TickedMessage { 
							Message = i.ToString()
						};
								
						UIApplication.SharedApplication.InvokeOnMainThread (() => {
							MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
						});
					}
				}, _cts.Token);

			} catch (OperationCanceledException opEx) {
				var message = new TickedMessage { 
					Message = "Cancelled"
				};
				UIApplication.SharedApplication.InvokeOnMainThread (() => {
					MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
				});

			}

			UIApplication.SharedApplication.EndBackgroundTask (_taskId);
		}

		public void Stop ()
		{
			_cts.Cancel ();
		}

		void OnExpiration ()
		{
			_cts.Cancel ();
		}
	}
}