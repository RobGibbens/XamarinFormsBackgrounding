namespace FormsBackgrounding
{
	public class DownloadProgressMessage
	{
		public long BytesWritten { get; set; }

		public long TotalBytesWritten { get; set; }

		public long TotalBytesExpectedToWrite { get; set; }

		public float Percentage { get; set; }
	}

	public class DownloadFinishedMessage
	{
		public string Url { get; set; }

		public string FilePath { get; set; }
	}
}