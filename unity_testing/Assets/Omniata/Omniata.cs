// ------------------------------------------------------------------------------
//  <Omniata>
//		Unity3D plugins for Omniata iOS and Android SDK.
//		Omniata Android SDK version: 2.0.0
//		Omniata iOS SDK version: 2.0.0
//      Target version of Android: android-21
//		created by Jun, 23-10-2014.
//		
//  </Omniata>
// ------------------------------------------------------------------------------
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
	public class Omniata
	{

		public const string SDK_VERSION = "unitySDK-1.0.0";


		/**
         * Setting your personalized api_key, uid and org.
         */
		//test example of api_key, uid & org.
//		public static string api_key = "a514370d";
//		public static string uid = "uidtest";
//		public static string org = "testOrg";
		public static string api_key = "<API KEY>";
		public static string uid = "<User ID>";
		public static string org = "<Orgnization Name>";
		
		
		/**
         * Get the current context of the activity.
         */	
		#if UNITY_ANDROID
			public static AndroidJavaObject playerActivityContext;
			public static void getContext()
			{
				using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
				
			}
		#endif
		
		/**
         * Extern initialize with api_key, user_id, org
         */	
		#if UNITY_IOS
			[System.Runtime.InteropServices.DllImport("__Internal")]
			public extern static void Initialize(string api_key, string uid, string org);
		#elif UNITY_ANDROID
			public static void Initialize(string apiKey, string userID, string org)
			{
				// Activity class name where you define the initialize method for omniata.
				using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
				{
					getContext();
					javaClass.CallStatic("initialize", playerActivityContext, apiKey, userID, org);
				}
			}
		#endif
		
		/**
         * Extern TrackLoad with default system parameters
         */
		#if UNITY_IOS
			[System.Runtime.InteropServices.DllImport("__Internal")]
			public extern static void TrackLoad();
		#elif UNITY_ANDROID
			public static void TrackLoad()
			{
				using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
				{
					javaClass.CallStatic("trackLoad");
				}
			}
		#endif
		
		/**
         * Extern TrackRevenue with total and currency_code
         */
		#if UNITY_IOS
			[System.Runtime.InteropServices.DllImport("__Internal")]
			public extern static void TrackRevenue(double total, string currency_code);
		#elif UNITY_ANDROID
			public static void TrackRevenue(double total, string currencyCode)
			{
				using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
				{
					javaClass.CallStatic("trackRevenue",total,currencyCode);
				}
			}
		#endif
		
		
		/**
         * Extern TrackEvent with type and parameters
         */
		#if UNITY_IOS
			[System.Runtime.InteropServices.DllImport("__Internal")]
			extern static void TrackEvent(string type, string parameters);
			public static void Track (string type, Dictionary<string, string> dictPara)
			{
				string parameters;
				parameters = DictionaryParse(dictPara);
				TrackEvent(type, parameters);
			}
		#elif UNITY_ANDROID
			public static void Track(string eventType, Dictionary<string, string> dictPara)
			{
				using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
				{
					String parameters;
					parameters = DictionaryParse(dictPara);
					javaClass.CallStatic("unity_track",eventType,parameters);
				}
			}
		#endif
		
		
		
		/**
         * Extern loglevel of SDK
         */
		#if UNITY_IOS
			[System.Runtime.InteropServices.DllImport("__Internal")]
			public extern static void Log(string message);
		
		#elif UNITY_ANDROID
			public static void Log(string message){
				using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
				{
					javaClass.CallStatic("unity_log",message);
				}
			}
		#endif
		
		/**
         * Convert dictionary to strings in order to pass to Object-C
         * Calling TrackEvent with type and attributesString.
         */
		private static string DictionaryParse (Dictionary<string, string> parameters)
		{
			string attributesString = "";
			foreach(KeyValuePair<string, string> kvp in parameters)
			{
				attributesString += kvp.Key + "=" + kvp.Value + "\n";
			}
			return attributesString;
		}
		
		/**
         * Extern LoadChannelMessage with channelID
         * only support iOS for now.
         */
		#if UNITY_IOS
			[System.Runtime.InteropServices.DllImport("__Internal")]
			public extern static void LoadChannelMessage(int channelID);
		#endif
		
	}
	
}
















