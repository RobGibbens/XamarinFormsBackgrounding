using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace FormsBackgrounding.iOS
{
	public class iOSLongRunningTaskExample : ILongRunningTaskExample
	{
		public event EventHandler<TickedEventArgs> Ticked = delegate {};

		nint _taskId;

		public async Task Start ()
		{
			_taskId = UIApplication.SharedApplication.BeginBackgroundTask ("LongRunningTask", OnExpiration);

			await Task.Run (() => {
				for (long i = 0; i < long.MaxValue; i++) {
					UIApplication.SharedApplication.InvokeOnMainThread(() => {
						Ticked(this, new TickedEventArgs(i));
					});
				}
			});

			UIApplication.SharedApplication.EndBackgroundTask (_taskId);
		}

		void OnExpiration ()
		{
			UIApplication.SharedApplication.EndBackgroundTask (_taskId);
		}
	}
}
