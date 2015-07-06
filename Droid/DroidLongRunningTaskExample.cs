using System;

using Android.App;
using Android.Content;
using System.Threading.Tasks;
using Android.OS;

namespace FormsBackgrounding.Droid
{
	[Service]
	public class DroidLongRunningTaskExample : Service
	{
		public event EventHandler<TickedEventArgs> Ticked = delegate {};

		readonly Activity _activity;

		public DroidLongRunningTaskExample ()
		{

		}
		public DroidLongRunningTaskExample (Activity activity)
		{
			_activity = activity;
		}

		public override IBinder OnBind (Intent intent)
		{
			return new LongRunningServiceBinder (this);
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			return StartCommandResult.Sticky;
		}

		public async Task Start ()
		{
			Task.Run (() => {
				for (long i = 0; i < long.MaxValue; i++) {
					App.Current.Service.Ticked(this, new TickedEventArgs(i));
					//Ticked (this, new TickedEventArgs (i));
				}
			});
//			var intent = new Intent (_activity, typeof(DroidLongRunningTaskExample));
//			_activity.RunOnUiThread (() => {
//				Application.Context.StartService (intent);
//			});
		}

		public void Stop ()
		{
			throw new NotImplementedException ();
		}
	}
}