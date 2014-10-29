Plugins for Unity3D to use Omniata SDK
======================================

Plugins define the methods of the native library (Omniata SDK of iOS and Android), which can be directly called in the Unity3D project.


Usage steps of the Plugin:
-----------------------------------
Import the files in the Unity3D package, including Editor and Omniata and Plugins. The details of each folder is explained as follows:

*Editor*: based on open source [XUPorter](https://github.com/onevcat/XUPorter), which includes the XcodePostProcess script to add the Omniata iOS SDK framework to the Xcode project. The open source does not support third part framework adding very well, so right now the framework is added in a folder named "Omniata" after the Xcode project build.

*Omniata*: include a C# Script that uses the plugins and an example project.

*Plugins*: include iOS and Android plugins (notes: subfolder is not supported by Unity3D, check [official documentation](http://docs.unity3d.com/Manual/PluginsForIOS.html)).


iOS plugin
-----------------------------------

#### Methods Definition in C#
The definition of the methods which can be used by C# code is defined in 'Omniata.cs', which should be put in the Assets of the Unity3D project.

#### Usage examples in Unity3D project

a. Initialize
```c#
Omniata.Initialize(Omniata.api_key, Omniata.uid, Omniata.debug);
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

f. Log
```c#
Omniata.Log("<log message>");
```

#### Example project
Import the 'Example' folder, uncomment the test api_key and uid in Omniata.cs file. When debug mode is true, 'Track load', 'Track revenue' and 'Track custom events' can be seen [here](https://demo.panel.omniata.com/data_models/55-custom-metrics/developer_console?api_key_ids%5B%5D=1414). Channel message can be viewd in the Xcode console when the debug mode is false.

The example project is developed and tested on an iOS device.








Android plugin
-----------------------------------

#### Plugin Description
The Android plugin includes the Java method of Omniata API and the local C# file of calling Java method.

#### Plugin usage procedure.
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


#### Usage example

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

#### Example project
The example Android project is 'OmniataAndroidPlugin', and the example Unity3D project is 'unity_plugin_android_example.unitypackage'.
When build the project to Android, set the 'Bundle Identifier' as 'com.omniata.android.plugin'.
Test results can be viewed with [this live demo](https://demo.panel.omniata.com/data_models/55-custom-metrics/developer_console?api_key_ids%5B%5D=1414).

