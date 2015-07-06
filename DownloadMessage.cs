namespace FormsBackgrounding
{
	public class DownloadMessage
	{
		public string Url { get; set; }
	}

	public class StartLongRunningTaskMessage
	{
	}

	public class TickedMessage
	{
		public string Message { get; set; }
	}

	public class StopLongRunningTaskMessage
	{
	}
}