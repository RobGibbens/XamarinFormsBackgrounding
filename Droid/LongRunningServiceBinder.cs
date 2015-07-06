using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

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