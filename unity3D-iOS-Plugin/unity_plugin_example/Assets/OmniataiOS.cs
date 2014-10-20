using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Omniata {
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
	public extern static void _log(string message);
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
	public extern static void Initialize(string api_key, string user_id, bool debug);
	
	// Generate event system parameters automatically.
	[System.Runtime.InteropServices.DllImport("__Internal")]
	public extern static void TrackLoad();
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
	public extern static void TrackPurchaseEvent(double total, string currency_code);
	
	// Generate event with customized parameters.
	[System.Runtime.InteropServices.DllImport("__Internal")]
	extern static void TrackEvent(string type, string parameters);
	
	// Get content with channel ID.
	[System.Runtime.InteropServices.DllImport("__Internal")]
	public extern static string loadMessagesForChannel(int channelID);
	
	//	public static string getMessagesForChannel(int channelID){
	//		return loadMessagesForChannel (channelID);
	//	}
	
	public static void Track (string type, Dictionary<string, string> parameters)
	{
		string attributesString = "";
		foreach(KeyValuePair<string, string> kvp in parameters)
		{
			attributesString += kvp.Key + "=" + kvp.Value + "\n";
		}
		TrackEvent(type, attributesString);
	}
	
	
	
}
