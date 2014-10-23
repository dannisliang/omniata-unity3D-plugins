Plugins for Unity3D to use Omniata SDK
======================================

Plugins define the methods of the native library (Omniata SDK of iOS and Android), which can be directly called in the Unity3D project.


iOS plugin
----------

### Plugin Description
The plugin is in the path:
* unity3D-iOS-Plugin/OmniataiOSPlugin.m

The plugin should be put in the path of one Unity3D project:
* <unity project name>/Assets/Plugins/iOS

### Methods Definition in C#
The definition of the methods which can be used by C# code is defined in
* unity3D-iOS-Plugin/OmniataiOS.cs

### Usage example

a. Initialize

```c#
string apikey = <api key>;
string uid = <uid>;
bool debug = <debug>;
Omniata.Initialize(apikey, uid, debug);
```

b. Track load
```c#
Omniata.TrackLoad();
```

c. Track revenue
```c#
double total = 99.9;
string currency_code = "EUR";
Omniata.TrackRevenue(total,currency_code);
```

d. Track events
```c#
Dictionary<string, string> parameters = new Dictionary<string, string>();
parameters.Add("app", "testapp");
parameters.Add("attack.attacker_won", "0");
string type="testing_type";
Omniata.Track(type, parameters);
```

e. Get channel message
```c#
int ChannelId = <channelID>;
string message = Omniata.GetChannelMessage(ChannelId);
```

### Example project
Set the Player settings for the unity3D project.
Files --> Build Settings --> iOS --> Player Settings
Choose 'SDK Version' as 'Device SDK'

After building to an iOS project, open the project in Xcode and add the Omniata iOS SDK (iOmniataAPI.framework or the project folder, check this [link](https://omniata.atlassian.net/wiki/display/DOC/iOS+SDK])), and then build.

Notes: b,c,d can be viewed [here](https://demo.panel.omniata.com/data_models/55-custom-metrics/developer_console?api_key_ids%5B%5D=1414) dynamically with the buttong click when the debug mode true.
e can be viewd in the Xcode console when the debug mode is false.

The example project is developed and tested on an iOS device.


Android plugin
---------------

### Plugin Description
The Android plugin includes the Java method of Omniata API and the local C# file of calling Java method.

### Plugin usage procedure.
1. Inside of the main activity of the Android project, initialize a public context object and add the following public methods.

a. Initialize 
```java
public void initialize(String apiKey, String userId,boolean debug){
    Omniata.initialize(this, apiKey, userId, debug);
}
```
b. Track load
```java
public static void trackload(){
    Omniata.trackLoad();
}
```
c. Track revenue
```java
public static void trackRevenue(double total, String currencyCode){
    Omniata.trackRevenue(total, currencyCode);
}
```
d. Track custom event
```java
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
```
e. Log method
```java
public static void log(String tag, String message){
	Log.i(tag,message);
}
```

2. Add network permission in the AndroidManifest.xml file as follows:
```xml
<uses-permission android:name="android.permission.INTERNET"/>
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
```
3. Export the project to jar file. 
Right click the project --> Export --> jar
Note: AndroidManifest.xml can be included inside of the jar file or can be excluded and put independently as in the example project.

4. Put the output jar file as well as the omniata Android SDK file 'omniata-android-sdk.jar' together in the following path of a Unity3D project '/Assets/Plugins/Android'.

5. Inside of the Unity3D project, add the file 'OmniataAndroid.cs' inside of the Assets folder and change the corresponding name of the main activity class inside. 
Notes: The namespace of the project can also be added for this file.

6. Use the API method of Omniata instance inside of the project.

7. Build and run the project for Android. 
Notes: in this step, check the 'Bundle Identifier' name inside of File --> Build Settings --> Player Settings --> Other Settings, it should be the same name of the exported jar package name in the above step. 


### Usage example

a. Initialize

```c#
string apikey = <api key>;
string uid = <uid>;
bool debug = <debug>;
Omniata.Initialize(apikey, uid, debug);
```

b. Track load
```c#
Omniata.TrackLoad();
```

c. Track revenue
```c#
double total = 99.9;
string currency_code = "EUR";
Omniata.TrackRevenue(total,currency_code);
```

d. Track events
```c#
Dictionary<string, string> parameters = new Dictionary<string, string>();
parameters.Add("app", "testapp");
parameters.Add("attack.attacker_won", "0");
string type="testing_type";
Omniata.Track(type, parameters);
```


### Example project
The example Android project is OmniataAndroidPlugin, and the example Unity3D project is 'unity_plugin_android_example.unitypackage'.
When build the project to Android, set the 'Bundle Identifier' as 'com.omniata.android.plugin'.
Test results can be viewed throught the live demo of this [url](https://demo.panel.omniata.com/data_models/55-custom-metrics/developer_console?api_key_ids%5B%5D=1414).









