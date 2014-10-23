// ------------------------------------------------------------------------------
//  <Omniata>
//		Unity3D local file to declare the method in Java.
//		Omniata Android SDK version: 1.1.1
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


public class Omniata
	{

		
		public static void Initialize(string apiKey, string userID, bool debug)
		{
			// Activity class name where you define the initialize method for omniata.
			using (AndroidJavaClass javaClass = new AndroidJavaClass(<android activity class name>))
			{
				using (AndroidJavaObject activity = javaClass.GetStatic<AndroidJavaObject>(<get activity context>))
				{
					activity.Call("initialize",apiKey,userID,debug);
				}
			}
		}
	
		
		public static void TrackLoad()
		{
			using (AndroidJavaClass javaClass = new AndroidJavaClass(<android activity class name>))
			{
				javaClass.CallStatic("trackload");
			}
		}

		public static void TrackRevenue(double total, string currencyCode)
		{
			using (AndroidJavaClass javaClass = new AndroidJavaClass(<android activity class name>))
			{
				javaClass.CallStatic("trackRevenue",total,currencyCode);
			}
		}

		public static void Track(string eventType, Dictionary<string, string> dictPara)
		{
			using (AndroidJavaClass javaClass = new AndroidJavaClass(<android activity class name>))
			{
				String parameters;
				parameters = DictionaryParse(dictPara);
				javaClass.CallStatic("track",eventType,parameters);
			}
		}

		public static void Log(string tag, string message){
			using (AndroidJavaClass javaClass = new AndroidJavaClass(<android activity class name>))
			{
				javaClass.CallStatic("log",tag,message);
			}
		}

		private static string DictionaryParse (Dictionary<string, string> parameters)
		{
			string attributesString = "";
			foreach(KeyValuePair<string, string> kvp in parameters)
			{
				attributesString += kvp.Key + "=" + kvp.Value + "\n";
			}
			return attributesString;
		}
}


