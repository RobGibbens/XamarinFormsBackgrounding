
namespace FormsBackgrounding.Droid
{


	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using Android.App;
	using Android.Content;
	using Android.Graphics;
	using Android.Graphics.Drawables;
	using Android.OS;
	using Android.Runtime;
	using Android.Util;
	using Android.Views;
	using Android.Widget;

	public class ImageHelper
	{
		static string baseDir = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);

		private static bool NeedsDownload (string uid)
		{
			return !File.Exists (System.IO.Path.Combine (baseDir, uid));
		}

//		public static async void SetImage (Context context, ImageView iv, FacebookFriend f)
//		{
//			FileInfo fi = new FileInfo (System.IO.Path.Combine (baseDir, f.uid));
//			if (!fi.Exists || fi.LastWriteTime < DateTime.Now.AddDays (-7)) {
//				using (var bmp = await DownloadImageAsync (f.pic_square, f.uid)) {
//					iv.SetImageBitmap (bmp);
//				}
//			} else {
//				using (var bmp = BitmapFactory.DecodeFile (System.IO.Path.Combine (baseDir, f.uid))) {
//					iv.SetImageBitmap (bmp);
//				}
//			}
//		}

		public static Task<string> DownloadImageAsync (string url)
		{
			return Task.Run<string> (() => DownloadImage (url));
		}

		private static string DownloadImage (string url)
		{
			Bitmap imageBitmap = null;
			string path = null;
			try {
				byte[] imageBytes;
				using (var mstream = new MemoryStream ()) {
					using (var imageUrl = new Java.Net.URL (url)) {
						var options = new BitmapFactory.Options {
							InSampleSize = 1,
							InPurgeable = true
						};

						var bit = BitmapFactory.DecodeStream (imageUrl.OpenStream (), null, options);
						bit.Compress (Bitmap.CompressFormat.Jpeg, 70, mstream);
					}
					imageBytes = mstream.ToArray ();
					path = System.IO.Path.Combine (baseDir, "monkey.png");
					File.WriteAllBytes (path, imageBytes);
//					if (imageBytes != null && imageBytes.Length > 0) {
//						var options = new BitmapFactory.Options {
//							InJustDecodeBounds = true,
//						};
//						// BitmapFactory.DecodeResource() will return a non-null value; dispose of it.
//						using (var dispose = BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length)) {
//						}
//						var imageHeight = options.OutHeight;
//						var imageWidth = options.OutWidth;
//						var imageType = options.OutMimeType;
//						var height = (float)options.OutHeight;
//						var width = (float)options.OutWidth;
//						var inSampleSize = 1D;
//
//						if (height > 100 || width > 100) {
//							inSampleSize = width > height
//							? height / 100
//							: width / 100;
//						}
//						options.InSampleSize = (int)inSampleSize;
//						options.InJustDecodeBounds = false;
//						return BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length, options);
//					}
				}
			} catch (Exception ex) {
				Log.WriteLine (LogPriority.Error, "GetImageFromBitmap Error", ex.Message);
			}
			return path;
		}
	}
}