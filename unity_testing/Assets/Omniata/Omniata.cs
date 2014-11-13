// ------------------------------------------------------------------------------
//  <Omniata>
//		Unity3D plugins for Omniata iOS and Android SDK.
//		Omniata Android SDK version: 2.0.1
//		Omniata iOS SDK version: 2.0.1
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
	public class Omniata: MonoBehaviour
	{
		public const string SDK_VERSION = "unitySDK-1.1.0";
		// Event parameter names consts
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
		private const string EVENT_PARAM_OM_UNITY_SDK_VERSION = "om_unity_sdk_version";
		// Event type consts
		private const string EVENT_TYPE_OM_LOAD = "om_load";
		private const string EVENT_TYPE_OM_REVENUE = "om_revenue";
		
		// Channel type consts
		private const string CHANNEL_ID = "channel_id";
		
		private static string api_key;
		private static string uid;
		private static string org;
		public static string analyzerUrl;
		public static string engagerUrl;

		public enum LogLevel {
			Verbose = 2,
			Debug,
			Info,
			Warn,
			Error,
			Assert
		}
		/**
         * Setting your personalized api_key, uid and org in Omniata.prefab.
         */
		public string API_KEY = "<API KEY>";
		public string UID = "<User ID>";
		public string ORG = "<Orgnization Name>";
		public LogLevel LOGLEVEL = LogLevel.Verbose;
		public bool startManually = false;


		void Awake() {
			if (!this.startManually) {
				Debug.Log ("Omniata Monobehavior Start");
				#if UNITY_IOS
				this.SetOmLoglevel((int)this.LOGLEVEL);
				#elif UNITY_ANDROID
				this.SetOmLoglevel((int)this.LOGLEVEL);
				#endif
				this.appDidLaunch (this.API_KEY, this.UID, this.ORG);
			}
		}


		/**
		 * Set the static api_key, uid and org for static method usage
		 */ 
		public void appDidLaunch(string API_KEY, string UID, string ORG){
			api_key = API_KEY;
			uid = UID;
			org = ORG;
			setURL (org);
			#if UNITY_IOS
			Initialize (api_key, uid, org);
			#elif UNITY_ANDROID
			Initialize (api_key, uid, org);
			#endif
		}



		public void SetOmLoglevel(int priority){
			SetLogLevel (priority);	
		}
		
		public void LogOm(string message){
			Log (message);
		}

		public void TrackOmLoad() {
			#if UNITY_IOS
			TrackLoad();
			#elif UNITY_ANDROID
			TrackLoad();
			#else
			StartCoroutine(this.TrackOmLoadCoroutine ());
			#endif
		}
		
		public void TrackOmRevenue(double total, string currency_code){
			#if UNITY_IOS
			TrackRevenue(total, currency_code);
			#elif UNITY_ANDROID
			TrackRevenue(total, currency_code);
			#else
			StartCoroutine(this.TrackOmRevenueCoroutine (total, currency_code));
			#endif	
		}
		
		public void TrackOm(string eventType, Dictionary<string, string> parameters){
			#if UNITY_IOS
			Track(eventType, parameters);
			#elif UNITY_ANDROID
			Track(eventType, parameters);
			#else
			StartCoroutine(this.TrackOmCoroutine (eventType, parameters));
			#endif	
			
		}
		
		public void LoadOmChannelMessage(int channelID){
			#if UNITY_IOS
			LoadChannelMessage(channelID);
			#else
			StartCoroutine(this.LoadOmChannelMessageCoroutine (channelID));
			#endif	
		}


		
		/**
		 * Extern loglevel of android and iOS SDK
		 * 
		 */
		
		#if UNITY_IOS
		/**
		 * iOS loglevel message should be one of these:
		 * SMT_LOG_ERROR, SMT_LOG_WARN, SMT_LOG_INFO, SMT_LOG_VERBOSE
		 * calling example is SetLogLevel();
		 */ 
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern void SetLogLevel(int priority);
		
		#elif UNITY_ANDROID
		/**
		 * priority in Android is from 2-8, the lower the loglevel, the more verbose the log is
		 * check details in Android API documentation.
		 */
		private static void SetLogLevel(int priority){
			using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
			{
				javaClass.CallStatic("setLogLevel",priority);
			}
		}
		#else
		private static void SetLogLevel(int priority){
			
		}
		#endif


		/**
         * Extern log of SDK
         */
		#if UNITY_IOS
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern void Log(string message);
		
		#elif UNITY_ANDROID
		private static void Log(string message){
			using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
			{
				javaClass.CallStatic("unity_log",message);
			}
		}
		#else 
		private void Log(string message){
			message = DateTime.Now + " Omniata" + ": " + message;
			Debug.Log (message);		
		}
		#endif

		/**
         * Get the current context of the activity.
         */	
		#if UNITY_ANDROID
		private static AndroidJavaObject playerActivityContext;
		private static void getContext()
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
		private extern static void Initialize(string api_key, string uid, string org);
		#elif UNITY_ANDROID
		private static void Initialize(string apiKey, string userID, string org)
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
		private extern static void TrackLoadWithParameters(string parameters);
		private static void TrackLoad(){
			Dictionary<string, string> dictPara = new Dictionary<string, string>();
			AddUnitySDKVersion(dictPara);
			String parameters;
			parameters = ToKeyValuePairString(dictPara);
			TrackLoadWithParameters(parameters);
		}
		#elif UNITY_ANDROID
		private static void TrackLoad(){
			using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
			{
				Dictionary<string, string> dictPara = new Dictionary<string, string>();
				AddUnitySDKVersion(dictPara);
				String parameters;
				parameters = ToKeyValuePairString(dictPara);
				javaClass.CallStatic("unityTrackLoad",parameters);
			}		
		}

		#else

		private IEnumerator TrackOmLoadCoroutine() {
			Dictionary<string, string> parameters = new Dictionary<string, string>();	
			AddAutomaticParameters(parameters);
			parameters.Add(EVENT_PARAM_API_KEY, api_key);
			parameters.Add(EVENT_PARAM_UID, uid);
			string url = urlGenerator (analyzerUrl, parameters);
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
		private extern static void TrackRevenue(double total, string currency_code);
		#elif UNITY_ANDROID
		private static void TrackRevenue(double total, string currencyCode)
		{
			using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
			{
				javaClass.CallStatic("trackRevenue",total,currencyCode);
			}
		}
		#else
		private IEnumerator TrackOmRevenueCoroutine(double total, string currency_code){
			Dictionary<string, string> parameters = new Dictionary<string, string>();	
			parameters.Add(EVENT_PARAM_API_KEY, api_key);
			parameters.Add(EVENT_PARAM_UID, uid);
			parameters.Add(EVENT_PARAM_TOTAL, total.ToString());
			parameters.Add(EVENT_PARAM_CURRENCY_CODE, currency_code);
			string url = urlGenerator (analyzerUrl, parameters);
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
		private static void Track (string type, Dictionary<string, string> dictPara)
		{
			string parameters;
			parameters = ToKeyValuePairString(dictPara);
			TrackEvent(type, parameters);
		}
		#elif UNITY_ANDROID
		private static void Track(string eventType, Dictionary<string, string> dictPara)
		{
			using (AndroidJavaClass javaClass = new AndroidJavaClass("com.omniata.android.sdk.Omniata"))
			{
				String parameters;
				parameters = ToKeyValuePairString(dictPara);
				javaClass.CallStatic("unityTrack",eventType,parameters);
			}
		}
		#else
		private IEnumerator TrackOmCoroutine(string eventType, Dictionary<string, string> parameters){
			parameters.Add (EVENT_PARAM_EVENT_TYPE, eventType);
			parameters.Add(EVENT_PARAM_API_KEY, api_key);
			parameters.Add(EVENT_PARAM_UID, uid);
			string url = urlGenerator (analyzerUrl, parameters);
			WWW www = new WWW(url);
			yield return www;
			Debug.Log (www.url);
			Debug.Log (www.isDone);
			Debug.Log (www.text);
		}
		#endif
		


		/**
         * Extern LoadChannelMessage with channelID
         * only support iOS for now.
         */
		#if UNITY_IOS
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private extern static void LoadChannelMessage(int channelID);
		#else
		private static IEnumerator LoadOmChannelMessageCoroutine(int channelID){
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add(EVENT_PARAM_API_KEY, api_key);
			parameters.Add(EVENT_PARAM_UID, uid);
			parameters.Add (CHANNEL_ID, channelID.ToString());
			string url = urlGenerator (engagerUrl, parameters);
			WWW www = new WWW(url);
			yield return www;
			Debug.Log (www.url);
			Debug.Log (www.isDone);
			Debug.Log (www.text);		
		}
		#endif

		/**
		 * Set analyzer and engager url with org
		 */
		private static void setURL(string morg){
			analyzerUrl = "https://"+morg+".analyzer.omniata.com/event?";
			engagerUrl = "https://"+morg+".engager.omniata.com/channel?";
		}

		/**
		 * Generated the automatic om parameters for platforms other than android and iOS
		 * 
		 */
		private static void AddAutomaticParameters(Dictionary<string, string> parameters)
		{
			RuntimePlatform platform = Application.platform;
			parameters.Add(EVENT_PARAM_EVENT_TYPE,EVENT_TYPE_OM_LOAD);
			parameters.Add(EVENT_PARAM_OM_PLATFORM, platform.ToString());
			parameters.Add(EVENT_PARAM_OM_DEVICE, SystemInfo.deviceModel);
			parameters.Add(EVENT_PARAM_OM_OS_VERSION, SystemInfo.operatingSystem);
			parameters.Add(EVENT_PARAM_OM_SDK_VERSION, SDK_VERSION);
		}

		private static void AddUnitySDKVersion(Dictionary<string, string> parameters)
		{
			parameters.Add(EVENT_PARAM_OM_UNITY_SDK_VERSION,SDK_VERSION);
		}
		
		/**
         * Convert dictionary to URL encoded key value pair strings.
         * Calling TrackEvent with type and attributesString.
         */
		private static string ToKeyValuePairString (Dictionary<string, string> parameters)
		{
			string attributesString = "";
			foreach(KeyValuePair<string, string> kvp in parameters)
			{
				attributesString += WWW.EscapeURL(kvp.Key) + "=" + WWW.EscapeURL(kvp.Value) + "\n";
			}
			return attributesString;
		}
		
		/**
		 * Genearted url for omniata event API with the parameters.
		 */
		private static string urlGenerator(string baseUrl, Dictionary<string, string> parameters){
			return baseUrl + String.Join ("&", ToKeyValuePairString (parameters).Split ('\n'));
		}

	}
}
















