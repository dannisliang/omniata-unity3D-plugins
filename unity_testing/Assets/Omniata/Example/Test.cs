// ------------------------------------------------------------------------------
//  <Omniata>
//		Unity3D project example to use Omniata Android plugin
//		Omniata Android SDK version: 1.1.1
//      Target version of Android: android-21
//		created by Jun, 23-10-2014.
//  </Omniata>
// ------------------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using omniata;

namespace test
{

    public class Test : MonoBehaviour
    {
		bool initialized;
		void Start()
		{
			Omniata.Log ("Start");
			initialized = false;

		}

		void OnApplicationPause(bool pause)
		{
			if (pause)
			{
				// we are in background
			}
			else
			{
				// we are in foreground again.
				if (initialized) 
				{
					// Automatically send om_load
					#if UNITY_IOS
						Omniata.TrackLoad(); //track load for iOS build
					#elif UNITY_ANDROID
						Omniata.TrackLoad(); //track load for Android build
					#else
						StartCoroutine(Omniata.TrackLoad ());//track load for local build
					#endif
				}
			}
		}

        void OnGUI()
        {

			int buttonCount = 5;
			int screenWidth = Screen.width;
			int screenHeight = Screen.height;
			
			int xSize = screenWidth / 3;
			int yMargin = Convert.ToInt32(screenHeight * 0.10);
			int ySize = (screenHeight - 2 * yMargin) / (buttonCount + (buttonCount - 1));
			
			int buttonXLeft = (screenWidth / 2) - (xSize / 2);
			
			// Make a background box
			int buttonIndex = 0;

			
			// Make the first button. If it is pressed, Omniata object will be initialized
			int buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "Initialize"))
			{	
				#if UNITY_IOS
					Omniata.Log ("Initialize");
					Omniata.Initialize(Omniata.api_key, Omniata.uid, Omniata.org);//Initialize for iOS build
					initialized = true;
				#elif UNITY_ANDROID
					Omniata.Log ("Initialize");
					Omniata.Initialize(Omniata.api_key, Omniata.uid, Omniata.org);//Initialize for Android build
					initialized = true;
				#endif

			}
			
			// Make the second button, send track load events to Omniata
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_load"))
			{
				#if UNITY_IOS
					Omniata.Log("track load");//track load for iOS build
					Omniata.TrackLoad();//track load for iOS build
				#elif UNITY_ANDROID
					Omniata.Log("track load");//track load for Android build
					Omniata.TrackLoad();//track load for Android build
				#else
					StartCoroutine(Omniata.TrackLoad ());//track load for local build
				#endif
			}
			
			// Make the third button, send track revenue events to Omniata
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_revenue "))
			{
				double total = 99.9;
				string currency_code = "EUR";
				#if UNITY_IOS
				Omniata.Log("track revenue");//track load for iOS build
				Omniata.TrackRevenue(total,currency_code);//track load for iOS build
				#elif UNITY_ANDROID
				Omniata.Log("track revenue");//track load for Android build
				Omniata.TrackRevenue(total,currency_code);//track load for Android build
				#else
					StartCoroutine(Omniata.TrackRevenue (total, currency_code));//track load for local build
				#endif

			}

			// Make the fourth button, customed the sending events to Omniata
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_event"))
			{
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				parameters.Add("app", "testapp");
				parameters.Add("attack.attacker_won", "0");
				string eventType = "testing_event_type";

				#if UNITY_IOS
					Omniata.Log("track custom event");//track for iOS build
					Omniata.Track(eventType,parameters);//track for iOS build
				#elif UNITY_ANDROID
					Omniata.Log("track custom event");//track for Android build
					Omniata.Track(eventType,parameters);//track for Android build
				#else
					StartCoroutine(Omniata.Track(eventType,parameters));//track for local build
				#endif

			}

			// Make the fifth button.
			/** Load the test message, only work for iOS build now
			 *  uncomment for Android build will return error
			 */
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "channel_info"))
			{
				#if UNITY_IOS
					Omniata.LoadChannelMessage(40);
				#else
					StartCoroutine(Omniata.LoadChanelMessage(40)); //load message for local build
				#endif
			}
       }

		
	}
}
