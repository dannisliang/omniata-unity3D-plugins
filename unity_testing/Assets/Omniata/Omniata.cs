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
		// Event parameter names constants
		private const string EVENT_PARAM_API_KEY = "api_key";
		private const string EVENT_PARAM_CURRENCY_CODE = "currency_code";
		private const string EVENT_PARAM_EVENT_TYPE = "om_event_type";
		private const string EVENT_PARAM_TOTAL = "total";
		private const string EVENT_PARAM_UID = "uid";
		private const string EVENT_PARAM_OM_DELTA = "om_delta";
		private const string EVENT_PARAM_OM_DEVICE = "om_device";
		private const string EVENT_PARAM_OM_PLATFORM = "om_platform";
		private const string EVENT_PARAM_OM_OS_VERSION = "om_os_version";
		private const string EVENT_PARAM_OM_SDK_VERSION = "om_sdk_version";
		private const string EVENT_PARAM_OM_RETRY = "om_retry";
		private const string EVENT_PARAM_OM_DISCARDED = "om_discarded";
		
		// Event type constants
		private const string EVENT_TYPE_OM_LOAD = "om_load";
		private const string EVENT_TYPE_OM_REVENUE = "om_revenue";
		private const string PLATFORM_WP8 = "wp8";

		// Channel type constns
		private const string CHANNEL_ID = "channel_id";

		/**
         * Setting your personalized api_key, uid and org.
         */
		//test example of api_key, uid & org.
		public static string api_key = "a514370d";
		public static string uid = "uidtest";
		public static string org = "testOrg";
//		public static string api_key = "<API KEY>";
//		public static string uid = "<User ID>";
//		public static string org = "<Orgnization Name>";

		public static string analyzer_url = "https://"+org+".analyzer.omniata.com/event?";
		public static string engager_url = "https://"+org+".engager.omniata.com/channel?";


		/**
         * Get the current context of the activity.
         */	
		#if UNITY_ANDROID
			public static AndroidJavaObject playerActivityContext;
		public static void getContext()testOrg
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
		#else
			public static IEnumerator TrackLoad(){
					Dictionary<string, string> parameters = new Dictionary<string, string>();	
					AddAutomaticParameters(parameters);
					parameters.Add(EVENT_PARAM_API_KEY, api_key);
					parameters.Add(EVENT_PARAM_UID, uid);
					string url = urlGenerater (analyzer_url, parameters);
					WWW www = new WWW(url);
					yield return www;
					Debug.Log (www.url);
					Debug.Log (www.isDone);
					Debug.Log (www.text);
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
		#else
			public static IEnumerator TrackRevenue(double total, string currency_code){
				Dictionary<string, string> parameters = new Dictionary<string, string>();	
				parameters.Add(EVENT_PARAM_API_KEY, api_key);
				parameters.Add(EVENT_PARAM_UID, uid);
				parameters.Add(EVENT_PARAM_TOTAL, total.ToString());
				parameters.Add(EVENT_PARAM_CURRENCY_CODE, currency_code);
				string url = urlGenerater (analyzer_url, parameters);
				WWW www = new WWW(url);
				yield return www;
				Debug.Log (www.url);
				Debug.Log (www.isDone);
				Debug.Log (www.text);
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
		#else
			public static IEnumerator Track(string eventType, Dictionary<string, string> parameters){
				parameters.Add (EVENT_PARAM_EVENT_TYPE, eventType);
				parameters.Add(EVENT_PARAM_API_KEY, api_key);
				parameters.Add(EVENT_PARAM_UID, uid);
				string url = urlGenerater (analyzer_url, parameters);
				WWW www = new WWW(url);
				yield return www;
				Debug.Log (www.url);
				Debug.Log (www.isDone);
				Debug.Log (www.text);
			}
		#endif

		
		/**
         * Extern log of SDK
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
		#else 
			public static void Log(string message){
				message = DateTime.Now + " Omniata" + ": " + message;
				Debug.Log (message);		
			}
		#endif
		
		/**
         * Extern LoadChannelMessage with channelID
         * only support iOS for now.
         */
		#if UNITY_IOS
			[System.Runtime.InteropServices.DllImport("__Internal")]
			public extern static void LoadChannelMessage(int channelID);
		#else 
			public static IEnumerator LoadChanelMessage(int channelID){
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				parameters.Add(EVENT_PARAM_API_KEY, api_key);
				parameters.Add(EVENT_PARAM_UID, uid);
				parameters.Add (CHANNEL_ID, channelID.ToString());
				string url = urlGenerater (engager_url, parameters);
				WWW www = new WWW(url);
				yield return www;
				Debug.Log (www.url);
				Debug.Log (www.isDone);
				Debug.Log (www.text);		
			}
		#endif


		/**
		 * Generated the automatic om parameters for platforms other than android and iOS
		 * 
		 */
		private static void AddAutomaticParameters(Dictionary<string, string> parameters)
		{
			RuntimePlatform platform = Application.platform;
			string platformName = "";
			if (platform == RuntimePlatform.WP8Player)
			{
				platformName = PLATFORM_WP8;
			} else
			{
				platformName = "unknown";
			}
			parameters.Add (EVENT_PARAM_EVENT_TYPE,EVENT_TYPE_OM_LOAD);
			parameters.Add(EVENT_PARAM_OM_PLATFORM, platformName);
			
			parameters.Add(EVENT_PARAM_OM_DEVICE, SystemInfo.deviceModel);
//			parameters.Add(EVENT_PARAM_OM_OS_VERSION, SystemInfo.operatingSystem);
			
			parameters.Add(EVENT_PARAM_OM_SDK_VERSION, SDK_VERSION);
		}

		
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
		 * Genearted url for omniata event API with the parameters.
		 */
		
		private static string urlGenerater(string baseUrl, Dictionary<string, string> parameters){
			string paraString = "";
			string[] words = DictionaryParse (parameters).Split('\n');
			for (int i=0; i<words.Length-1; i++) {
				if(i==0){
					paraString = words[i];
				}else{
					paraString = paraString + "&" +words[i];
				}
			}
			string url = baseUrl + paraString;
			return url;
		}
	}
	
}
















