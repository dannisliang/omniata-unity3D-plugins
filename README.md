Plugins for Unity3D to use Omniata SDK
======================================

Plugins define the methods of the native library (Omniata SDK of iOS and Android), which can be directly called in the Unity3D project.


iOS plugin
----------

#### Plugin Description
The plugin is in the path:
* unity3D-iOS-Plugin/OmniataiOSPlugin.m

The plugin should be put in the path of one Unity3D project:
* <unity project name>/Assets/Plugins/iOS

#### Methods Definition in C#
The definition of the methods which can be used by C# code is defined in
* unity3D-iOS-Plugin/OmniataiOS.cs

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
Omniata.TrackPurchaseEvent(total,currency_code);
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
string message = Omniata.loadMessagesForChannel(ChannelId);
Omniata._log (string.Format ("{0}", message));
```


#### Example project
Set the Player settings for the unity3D project.
Files --> Build Settings --> iOS --> Player Settings
Choose 'SDK Version' as 'Device SDK'
After building to an iOS project, open the project in Xcode and add the Omniata iOS SDK (iOmniataAPI.framework or the project folder, check this [link][https://omniata.atlassian.net/wiki/display/DOC/iOS+SDK]), build.
Notes: b,c,d can be viewed [here][https://demo.panel.omniata.com/data_models/55-custom-metrics/developer_console?api_key_ids%5B%5D=1414] dynamically with the buttong click when the debug mode is true.
e can be viewd in the Xcode console when the debug mode if false.

The example project is developed and tested on an iOS device.














