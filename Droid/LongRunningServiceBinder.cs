using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace FormsBackgrounding.Droid
{

	public class LongRunningServiceBinder : Binder
	{
		public DroidLongRunningService Service { get; private set; }

		public LongRunningServiceBinder (DroidLongRunningService service)
		{
			this.Service = service;
		}
	}

}