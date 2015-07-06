using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace FormsBackgrounding.Droid
{

	public class LongRunningServiceConnection : Java.Lang.Object, IServiceConnection
	{
		public event EventHandler<ServiceConnectionEventArgs> ConnectionChanged = delegate{};
		LongRunningServiceBinder _binder;

		public DroidLongRunningTaskExample Service
		{
			get {
				return _binder.Service;
			}
		}

		public void OnServiceConnected (ComponentName name, IBinder binder)
		{
			_binder = binder as LongRunningServiceBinder;
			ConnectionChanged(this, new ServiceConnectionEventArgs(true));
		}

		public void OnServiceDisconnected (ComponentName name)
		{
			ConnectionChanged (this, new ServiceConnectionEventArgs (false));
			_binder = null;
		}
	}
}