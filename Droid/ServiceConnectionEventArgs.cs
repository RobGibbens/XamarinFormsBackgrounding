using System;

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