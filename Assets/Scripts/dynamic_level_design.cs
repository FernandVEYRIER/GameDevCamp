using UnityEngine;
using System.Collections;
using System.Net;
using System;

public class dynamic_level_design : MonoBehaviour
{
	// create a new instance of WebClient
	WebClient client = new WebClient();

	// Use this for initialization
	void Start ()
	{
		// set the user agent to IE6
		client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	String	GetLevelFromWeb(String levelName)
	{
		try
		{
			// actually execute the GET request
			string ret = client.DownloadString("http://www.daveamenta.com/2008-05/c-webclient-usage/"); //"http://sitedefernand/" + levelName
			return(ret);
		}
		catch (WebException we)
		{
			// WebException.Status holds useful information
			Debug.Log("Web ex: " + we.Message + "\n" + we.Status.ToString());
		}
		catch (NotSupportedException ne)
		{
			// other errors
			Debug.Log("Not supp ex :" + ne.Message);
		}
		return (null);
	}
}
