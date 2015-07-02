using System;

using Android.App;
using Android.Content;
using System.Threading.Tasks;

namespace FormsBackgrounding.Droid
{

	public class DroidLongRunningTaskExample : ILongRunningTaskExample
	{
		public event EventHandler<TickedEventArgs> Ticked = delegate {};

		readonly Activity _activity;

		public DroidLongRunningTaskExample (Activity activity)
		{
			_activity = activity;
		}

		public async Task Start ()
		{
			var intent = new Intent (_activity, typeof(DroidLongRunningService));

			_activity.RunOnUiThread (() => {
				Application.Context.StartService (intent);
			});
		}

		public void Stop ()
		{
			throw new NotImplementedException ();
		}
	}
}