using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Util;

namespace FormsBackgrounding.Droid
{
	public class ImageHelper
	{
		private string Path
		{
			get {
				string baseDir = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				return System.IO.Path.Combine (baseDir, "monkey.png");
			}
		}

		private bool NeedsDownload ()
		{
			return !File.Exists (this.Path);
		}

		public Task<string> DownloadImageAsync (string url)
		{
			if (NeedsDownload()) {
				return Task.Run (() => DownloadImage (url));
			} else {
				return Task.FromResult(this.Path);
			}
		}

		private string DownloadImage (string url)
		{
			try {
			    using (var stream = new MemoryStream ()) {
					using (var imageUrl = new Java.Net.URL (url)) {
						var options = new BitmapFactory.Options {
							InSampleSize = 1,
							InPurgeable = true
						};

						var bit = BitmapFactory.DecodeStream (imageUrl.OpenStream (), null, options);
						bit.Compress (Bitmap.CompressFormat.Png, 70, stream);
					}
					var imageBytes = stream.ToArray ();

					File.WriteAllBytes (this.Path, imageBytes);
				}
			} catch (Exception ex) {
				Log.WriteLine (LogPriority.Error, "GetImageFromBitmap Error", ex.Message);
			}

			return this.Path;
		}
	}
}