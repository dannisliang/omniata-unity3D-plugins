using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace omniata{

	/**
     * The Unity-plugin class of Omniata SDK.
     * Omniata is the only integration point between a Unity application
     * and the SDK.
     * Details of the Omniata iOS and Android SDK, check the official
     * documentation here:
     * https://omniata.atlassian.net/wiki/display/DOC/SDKs
     */
	public class Omniata {

		//test example of api_key & uid
		/**
         * Setting your personalized api_key and uid.
         */
		//		public static string api_key = "a514370d";
		//		public static string uid = "uidtest";
		public const string SDK_VERSION = "unitySDK-1.0.0";
		public static string api_key = "<API KEY>";
		public static string uid = "<User ID>";
		public static bool debug = true;

	#if UNITY_IOS
		/**
         * Extern Log of iOS plugins
         */
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void Log(string message);
	#endif

	#if UNITY_IOS
		/**
         * Extern initialize with api_key, user_id, false, debug
         */
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void Initialize(string api_key, string uid, bool debug);
	#endif

	#if UNITY_IOS
		/**
         * Extern TrackLoad with default system parameters
         */
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void TrackLoad();
	#endif

	#if UNITY_IOS
		/**
         * Extern TrackRevenue with total and currency_code
         */
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static void TrackRevenue(double total, string currency_code);
	#endif

	#if UNITY_IOS
		/**
         * Extern TrackEvent with type and parameters
         */
		[System.Runtime.InteropServices.DllImport("__Internal")]
		extern static void TrackEvent(string type, string parameters);
	#endif

	#if UNITY_IOS
		/**
         * Extern GetChannelMessage with channelID
         */
		[System.Runtime.InteropServices.DllImport("__Internal")]
		public extern static string GetChannelMessage(int channelID);
	#endif

	#if UNITY_IOS
		/**
         * Convert dictionary to strings in order to pass to Object-C
         * Calling TrackEvent with type and attributesString.
         */
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