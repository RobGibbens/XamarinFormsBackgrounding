using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FormsBackgrounding
{
	public class TickedEventArgs : EventArgs
	{
		public long TickCounter { get; private set; }

		public TickedEventArgs (long tickCounter)
		{
			this.TickCounter = tickCounter;
		}
	}
}