using System.Collections.Generic;

namespace RoadStatus
{
	public class RStatus
	{
		public string id { get; set; }
		public string displayName { get; set; }
		public string statusSeverity { get; set; }
		public string statusSeverityDescription { get; set; }
		public string url { get; set; }
	}

	public class AppKey
	{
		public string appId { get; set; }
		public string appKey { get; set; }
	}
}
