using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace test
{
    public class Test : MonoBehaviour
    {
		void Start()
		{

		}

		void OnApplicationPause(bool pause)
		{

		}

        void OnGUI()
        {

			int buttonCount = 4;
			int screenWidth = Screen.width;
			int screenHeight = Screen.height;
			
			int xSize = screenWidth / 3;
			int yMargin = Convert.ToInt32(screenHeight * 0.10);
			int ySize = (screenHeight - 2 * yMargin) / (buttonCount + (buttonCount - 1));
			
			int buttonXLeft = (screenWidth / 2) - (xSize / 2);
			
			// Make a background box
			int buttonIndex = 0;

			
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			int buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "Initialize"))
			{		
				string apikey1 = "a514370d";
				string apikey2 = "4a86cc2f";
				string uid1 = "uidtest1";
				string uid2 = "uidtest2";
				bool debug = true;
				Omniata.Initialize(apikey1, uid1, debug);
			}
			
			// Make the second button.
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_load"))
			{
				Omniata.TrackLoad();
			}
			
			// Make the third button.
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_revenue "))
			{
				double total = 99.9;
				string currency_code = "EUR";
				Omniata.TrackPurchaseEvent(total,currency_code);
			}
			// Make the fourth button.
			buttonIndex++;
			buttonYTop = yMargin + (buttonIndex * ySize) + (buttonIndex * ySize);
			if (GUI.Button(new Rect(buttonXLeft, buttonYTop, xSize, ySize), "om_event"))
			{
//				omniataComponent.Channel(123, new TestNetworkResponseHandler());
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				parameters.Add("app", "testapp");
				parameters.Add("attack.attacker_won", "0");
				string type="testing_type";
				Omniata.Track(type, parameters);
			}
       }
    }
}
