using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace RoadStatus
{
	class Program
	{
		public IRESTclient _RESTclient;

		public Program(IRESTclient RESTclient)
		{
			_RESTclient = RESTclient;
		}

		static int Main(string[] args)
		{
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterType<RESTclient>().As<IRESTclient>();
			IContainer container = builder.Build();

			AppKey appKey = new AppKey();
			int getKey = container.Resolve<IRESTclient>().getLicKey(ref appKey);
			switch (getKey)
			{
				case -1:
					Console.WriteLine("Error - Unable to retrieve developer key.");
					return -2;
				case -2:
					Console.WriteLine("Error - Unable to read developer key file.");
					return -2;
			}

			string strResponse = "";
			string strRequestURL = "https://api.tfl.gov.uk/road/" + args[0] + "?app_id=" + appKey.appId + "&app_key=" + appKey.appKey;
			RStatus rStatus = new RStatus();

			bool roadRequest = container.Resolve<IRESTclient>().makeRequest(strRequestURL, ref strResponse);
			if (roadRequest == false)
			{
				if (strResponse == "The remote server returned an error: (404) Not Found.")
					Console.WriteLine("{0} is not a valid road.", args[0]);
				else
					Console.WriteLine("An unknown error has occured -\n{0}", strResponse);
				return -1;
			}
			bool parse = container.Resolve<IRESTclient>().parseResponse(strResponse, ref rStatus);

			Console.WriteLine("The status of the {0} is as follows", rStatus.displayName);
			Console.WriteLine("\tRoad Status is {0} ", rStatus.statusSeverity);
			Console.WriteLine("\tRoad Status Description is {0} ", rStatus.statusSeverityDescription);

			return 0;
		}
	}
}
