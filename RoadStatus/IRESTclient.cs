using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus
{
	public interface IRESTclient
	{
		bool makeRequest(string strRequestURL, ref string strResponse);
		bool parseResponse(string strResponse, ref RStatus rStatus);
		int getLicKey(ref AppKey appKey);
	}
}
