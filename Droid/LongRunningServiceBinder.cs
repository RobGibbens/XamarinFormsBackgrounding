using Android.OS;

namespace FormsBackgrounding.Droid
{
	public class LongRunningServiceBinder : Binder
	{
		public DroidLongRunningTaskExample Service { get; private set; }

		public LongRunningServiceBinder (DroidLongRunningTaskExample service)
		{
			this.Service = service;
		}
	}
}