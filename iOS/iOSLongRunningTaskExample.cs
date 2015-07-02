using System;
using UIKit;
using System.Threading.Tasks;
using System.Threading;

namespace FormsBackgrounding.iOS
{
	public class iOSLongRunningTaskExample : ILongRunningTaskExample
	{
		public event EventHandler<TickedEventArgs> Ticked = delegate {};

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
						UIApplication.SharedApplication.InvokeOnMainThread (() => {
							Ticked (this, new TickedEventArgs (i));
						});
					}
				}, _cts.Token);

			} catch (OperationCanceledException opEx) {
				var s = opEx.Message;

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
			//UIApplication.SharedApplication.EndBackgroundTask (_taskId);
		}
	}
}
