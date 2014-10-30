using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace omniata{

	public class Omniata {

		//test example of api_key & uid
//		public static string api_key = "a514370d";
//		public static string uid = "uidtest";
		private const string SDK_VERSION = "unitySDK-1.0.0";
		public static string api_key = "<API KEY>";
		public static string uid = "<User ID>";
		public static bool debug = true;

	#if UNITY_IOS
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void Log(string message);
	#endif

	#if UNITY_IOS
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void Initialize(string api_key, string user_id, bool debug);
	#endif

	#if UNITY_IOS
		// Generate event system parameters automatically.
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void TrackLoad();
	#endif

	#if UNITY_IOS
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void TrackRevenue(double total, string currency_code);
	#endif

	#if UNITY_IOS
		// Generate event with customized parameters.
		[System.Runtime.InteropServices.DllImport("__Internal")]
		extern static void TrackEvent(string type, string parameters);
	#endif

	#if UNITY_IOS
		// Get content with channel ID.
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static string GetChannelMessage(int channelID);
	#endif

	#if UNITY_IOS
		public static void Track (string type, Dictionary<string, string> parameters)
		{
			string attributesString = "";
			foreach(KeyValuePair<string, string> kvp in parameters)
			{
				attributesString += kvp.Key + "=" + kvp.Value + "\n";
			}
			TrackEvent(type, attributesString);
		}
	#endif
		
	}

}