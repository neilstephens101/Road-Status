using System;
using RoadStatus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadStatus.Tests
{
	[TestClass]
	public class RoadStatusTest
	{
		[TestMethod]
		public void getKey()
		{
			RESTclient rClient = new RESTclient();
			AppKey appKey = new AppKey();
			int getKey = rClient.getLicKey(ref appKey);
			Assert.AreEqual<int>(0, getKey);
		}

		[TestMethod]
		public void validRoadStatus()
		{
			string strResponse = "";
			string strRoad = "A40";
			string strRequestURL = "https://api.tfl.gov.uk/road/" + strRoad;
			RStatus rStatus = new RStatus();

			RESTclient rClient = new RESTclient();
			bool roadRequest = rClient.makeRequest(strRequestURL, ref strResponse);
			Assert.IsTrue(roadRequest);
		}

		[TestMethod]
		public void invalidRoadStatus()
		{
			string strResponse = "";
			string strRoad = "A123";
			string strRequestURL = "https://api.tfl.gov.uk/road/" + strRoad;
			RStatus rStatus = new RStatus();

			RESTclient rClient = new RESTclient();
			bool roadRequest = rClient.makeRequest(strRequestURL, ref strResponse);
			Assert.IsFalse(roadRequest);
			Assert.AreEqual<string>("The remote server returned an error: (404) Not Found.", strResponse);
		}

		[TestMethod]
		public void invalidURL()
		{
			string strResponse = "";
			string strRoad = "A40";
			string strRequestURL = "https://ap.tfl.gov.uk/road/" + strRoad;
			RStatus rStatus = new RStatus();

			RESTclient rClient = new RESTclient();
			bool roadRequest = rClient.makeRequest(strRequestURL, ref strResponse);
			Assert.IsFalse(roadRequest);
			Assert.AreEqual<string>("Unable to connect to the remote server", strResponse);
		}
	}
}
