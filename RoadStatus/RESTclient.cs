using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RoadStatus
{
	public enum httpVerb
	{
		GET,
		POST,
		PUT,
		DELETE
	}

	public class RESTclient : IRESTclient
	{
		public httpVerb httpMethod { get; set; }

		public RESTclient()
		{
			httpMethod = httpVerb.GET;
		}

		public int getLicKey(ref AppKey appKey)
		{
			string strKeyFile = "devKey.json";
			if (File.Exists(strKeyFile) == false)
				return -1;
			try
			{
				appKey = JsonConvert.DeserializeObject<AppKey>(File.ReadAllText(strKeyFile));
			}
			catch (Exception ex)
			{
				return -2;
			}
			return 0;
		}
		public bool makeRequest(string strRequestURL, ref string strResponse)
		{
			bool madeRequest = true;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strRequestURL);
			request.Method = httpMethod.ToString();

			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (Exception ex)
			{
				strResponse = ex.Message;
				madeRequest = false;
			}
			if (madeRequest == false)
				return madeRequest;

			Stream respStream = null;
			try
			{
				respStream = response.GetResponseStream();
			}
			catch (Exception ex)
			{
				strResponse = ex.Message;
				madeRequest = false;
			}
			if (madeRequest == false)
				return madeRequest;

			StreamReader reader = new StreamReader(respStream);
			try
			{
				strResponse = reader.ReadToEnd();
			}
			catch (Exception ex)
			{
				strResponse = ex.Message;
				madeRequest = false;
			}
			return madeRequest;
		}

		public bool parseResponse(string strResponse, ref RStatus rStatus)
		{
			JArray roadStatus = JsonConvert.DeserializeObject<JArray>(strResponse);
			JObject statusObject = (JObject)roadStatus[0];

			rStatus.id = statusObject["id"].ToString();
			rStatus.displayName = statusObject["displayName"].ToString();
			rStatus.statusSeverity = statusObject["statusSeverity"].ToString();
			rStatus.statusSeverityDescription = statusObject["statusSeverityDescription"].ToString();
			rStatus.url = statusObject["url"].ToString();

			return true;
		}
	}
}
