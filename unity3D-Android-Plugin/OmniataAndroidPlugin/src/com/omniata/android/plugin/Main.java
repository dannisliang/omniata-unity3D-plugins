package com.omniata.android.plugin;

import com.unity3d.player.UnityPlayerActivity;
import com.omniata.android.sdk.Omniata;
import org.json.JSONException;
import org.json.JSONObject;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;

public class Main extends UnityPlayerActivity {

	public static Context mContext;
	
    @Override
    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        mContext = this;
    }
    
    /**
     * Initializes the Omniata API
     * @param apiKey
     * @param userId
     * @param debug				if true, events will send to "http://api-test.omniata.com/event?<params>"
     * 							if false, events will send to "http://api.omniata.com/event?<params>"
     */
    public void initialize(String apiKey, String userId,boolean debug){
        Omniata.initialize(this, apiKey, userId, debug);
    }
    
    /**
     * Track load
     */
    public static void trackload(){
        Omniata.trackLoad();
    }
    
    /**
     * Track Revenue
     * @param total				number of value
     * @param currencyCode		e.g. "EUR"
     */
    public static void trackRevenue(double total, String currencyCode){
        Omniata.trackRevenue(total, currencyCode);
    }
    
    /**
     * Track custom event
     * @param eventType			set the value of "om_event_type"
     * @param para				
     */
    public static void track(String eventType, String para){
        JSONObject parameters = new JSONObject();
        String[] paraArray=para.split("\n");
        String[] paraPair;
        // Convert string to JsonObject
        for (int i=0;i<paraArray.length;i++){
            paraPair = paraArray[i].split("=");
            try{
                parameters.put(paraPair[0], paraPair[1]);
            } catch(JSONException e){
                // do something
                Log.e("SDK testing", e.toString());
            }
        }
        Omniata.track(eventType, parameters);
    }
    
    /**
     * Log method
     * @param tag
     * @param message
     */
    public static void log(String tag, String message){
    	Log.i(tag,message);
    }
}

