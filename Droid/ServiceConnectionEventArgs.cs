using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace FormsBackgrounding.Droid
{

	public class ServiceConnectionEventArgs : EventArgs
	{
		public bool IsConnected { get; private set;}
		public ServiceConnectionEventArgs(bool isConnected)
		{
			IsConnected = isConnected;
		}
	}

}