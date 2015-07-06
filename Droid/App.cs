using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace FormsBackgrounding.Droid
{

	public class App
	{
		static Lazy<App> app = new Lazy<App>(() => new App());

		public static App Current
		{
			get { return app.Value; }
		}

		// TODO: Demo3 - Step 4 - Provide access to bound service
		LongRunningServiceConnection lsConnection;
		public DroidLongRunningTaskExample Service
		{
			get
			{
				return lsConnection.Service;
				//				throw new NotImplementedException();
			}
		}

		public event EventHandler<ServiceConnectionEventArgs> ConnectionChanged = delegate{};

		private App()
		{
			Task.Run(() => {
				var context = Application.Context;

				// Start the service - since it's in our process.
				var intent = new Intent(context, typeof(DroidLongRunningTaskExample));
				context.StartService(intent);

				// TODO: Demo3 - Step 5 - Bind to the service
				lsConnection = new LongRunningServiceConnection();
				lsConnection.ConnectionChanged += (s,e) => ConnectionChanged(this, e);
				context.BindService(intent, lsConnection, Bind.AutoCreate);
			});
		}
	}
}