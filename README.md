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

e. Log
```c#
Omniata.Log("<log message>");
```

#### Example project
Import the 'Example' folder, uncomment the test api_key and uid in Omniata.cs file. When debug mode is true, 'Track load', 'Track revenue' and 'Track custom events' can be seen [here](https://demo.panel.omniata.com/data_models/55-custom-metrics/developer_console?api_key_ids%5B%5D=1414). Channel message retrieve feature is not supported for Android for now.