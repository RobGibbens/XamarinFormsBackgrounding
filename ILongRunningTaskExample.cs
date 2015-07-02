using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace FormsBackgrounding
{
	public interface ILongRunningTaskExample
	{
		event EventHandler<TickedEventArgs> Ticked;
		Task Start();
		void Stop();
	}	
}