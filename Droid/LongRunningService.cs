using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace FormsBackgrounding.Droid
{

	[Service]
	public class LongRunningService : Service
	{
		public event EventHandler<TickedEventArgs> Ticked = delegate{};

		public override IBinder OnBind (Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			Task.Run (() => {
				for (long i = 0; i < long.MaxValue; i++) {
					Ticked (this, new TickedEventArgs (i));
				}
			});

			return StartCommandResult.Sticky;
		}
	}
}