//using System;
//using Android.App;
//using Android.Content;
//using System.Threading.Tasks;
//
//namespace FormsBackgrounding.Droid
//{
//	public class App
//	{
//		static Lazy<App> app = new Lazy<App>(() => new App());
//
//		public static App Current
//		{
//			get { return app.Value; }
//		}
//			
//		LongRunningServiceConnection lsConnection;
//		public DroidLongRunningTaskExample Service
//		{
//			get
//			{
//				return lsConnection.Service;
//			}
//		}
//
//		public event EventHandler<ServiceConnectionEventArgs> ConnectionChanged = delegate{};
//
//		private App()
//		{
//			Task.Run(() => {
//				var context = Application.Context;
//
//				var intent = new Intent(context, typeof(DroidLongRunningTaskExample));
//				context.StartService(intent);
//
//				lsConnection = new LongRunningServiceConnection();
//				lsConnection.ConnectionChanged += (s,e) => ConnectionChanged(this, e);
//				context.BindService(intent, lsConnection, Bind.AutoCreate);
//			});
//		}
//	}
//}