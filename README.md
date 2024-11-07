# Repo Goal and Aims
This repo was made for a live demo that was held for a bunch of social scientists and support staff. The aim was to briefly show how awesome StereoKit is, and highlight a few things it can do. 

You can find the presentation slides in the Presentation folder. The demo was recorded, and if you'd like to see that, please send us an email at sosxr@fsw.leidenuniv.nl and I'll get you the link.

In this demo we're going to create a little program that's (very) loosely based on the [VAAST](https://www.psytoolkit.org/experiment-library/vaast_images.html) cognitive functioning task using [StereoKit](https://stereokit.net) . We're going to deploy this to the Meta Quest. To get up and running we're going to need a few things.

# Requirements to get started
## Operating System
### For the Mac users amongst us
I hate to say this, but we're gonna need Windows. StereoKit (v0.3.9) does not work well on MacOS. It runs, if you really want to, but it behaves like a diva. If you look at her the wrong way, she won't play along anymore. I tried for a long time to get this working, but it's just not fun to do.

So. Windows. Use a virtual machine. I had mixed results with [UTM](https://getutm.app), but it is delightfully free. I opted for [Parallels](https://www.parallels.com/products/desktop/trial/). It runs like a dream on Apple Silicon. It has 14-day free trial (and then [€44 via SurfSpot](https://www.surfspot.nl/nieuw-bij-surfspot/parallels-desktop-19-voor-mac-1-jaar.html)). Or boot Windows directly through BootCamp. Either way, you can start using Windows 11 without activating a key yet, but you'll need one eventually. 

For Parallels users: Make sure that the StereoKit project is not in a Mac-shared folder. (Parallels has this lovely feature where you can easily share your normal Mac folders with Windows. This works fine 99% of the time. StereoKit (diva) is the 1% here and won't compile past the OpenXR call).

### For the Linux users
Should work. No promises. You're on your own here. Good luck.

## Hardware
An HMD is not strictly mandatory to code along with the demo, but it does make it a lot more fun. But, since Stereokit has an excellent simulator, you can use that to get a sense of your app, without having to use an HMD.

 This repo is made with / for the Meta Quest 3 headset. We're going to play around with the passthrough feature just a little bit. This works nicest on the Quest 3, but other than that, you will be good with the Q1 or Q2 as well. And, since StereoKit runs on OpenXR, it should deploy quite nicely to a whole bunch of other HMDs too.

If your Quest is not yet in 'developer mode', do this too. [Here's a robot explaining how](https://youtu.be/8WxK8QeaEIc?si=xSYWaS7WbxLl8AvQ&t=37).

## Software
Check the StereoKit repo and instruxtions on the latest install preceduc
We're going to need these packages to get going:
- [.NET7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) (.NET8 works for most things, but `dotnet watch` seems to require .NET7)
- [OpenJDK 17](https://learn.microsoft.com/en-gb/java/openjdk/download#openjdk-17) (Neither 8 nor 21 works. It's gotta be 17)
- An IDE like [VS Code](https://code.visualstudio.com/download), [Visual Studio](https://visualstudio.microsoft.com/downloads/), or [Rider](https://www.jetbrains.com/rider/download/#section=mac). If you're new to this: VS Code is an excellent option.
- Android SDK 33 "Tiramisu". Theoretically this should install via the `dotnet workload restore` command in your IDE, but for me that didn't pan out. Another way to get the SDK is through the [Android Studio](https://developer.android.com/studio)
	- Open Android Studio --> "More Actions" --> SDK Manager --> Checkbox Android 13 "Tiramisu", which is API level 33 --> Apply --> Wait for install --> Grab some coffee --> Ok
	- On Windows you sometimes need to manually set the Environment Variable, for the system to recognize where the SDK is installed. Search for "Edit system environment variables" in the Windows task bar --> click Environment Variables --> Look for the System Variables (bottom section --> click New --> Set the 'Variable Name' to 'AndroidSdkDirectory', and the 'Variable value' = 'C:\Users\YOUR-USERNAME-HERE\AppData\Local\Android\Sdk' (without the quotation marks).
- [SideQuest](https://sidequestvr.com/setup-howto) , so that we can have a look at our logfiles on the HMD.
- Optional but useful: [ADB](https://www.androidpolice.com/install-adb-windows-mac-linux-guide/) (scroll down to "Perform a manual setup in Windows", and do the 4 steps to install ADB *and* the 6 steps below on how to get ADB installed system-wide)
- Optional but fun: 6-8 of your favourite photos, stored as `.png`.



----


# Installation Guide For New SK Projects

Open VS Code

> Ctrl + Shift + P --> "Focus on Terminal View"

In the terminal, copy or type the below commands, hitting Enter/Return after each.

`mkdir StereoKit\MyFirstStereoKit`

`cd StereoKit\MyFirstStereoKit`

`dotnet new install StereoKit.Templates`

`dotnet new sk-multi`

> Go to File -> Open Folder -> Open 'MyFirstStereoKit' folder we created.

If your HMD is connected and you're on .NET7:

`dotnet publish -c Release Projects\Android\MyFirstStereoKit_Android.csproj`

`adb install Projects\Android\bin\Release\net7.0-android\publish\com.companyname.MyFirstStereoKit-Signed.apk`

If your HMD is connected and you're on .NET8:

`dotnet run --project Projects\Android\MyFirstStereoKit_Android.csproj` 

Or to use the live simulator:

`dotnet watch`



# Installation Guide for this Project

## If github is very new to you:

> Hit the green <>CODE icon, and choose "Download ZIP"

In Windows File Explorer go to your Downloads folder. 

> Right click on the zipped file "sk-demo" --> "Extract All"

Go back to VSCode

> Go to File -> Open Folder -> Open 'sk-demo' folder we just unzipped.

To build this project, if your HMD is connected and you're still using .NET7:

`dotnet publish -c Release Projects\Android\sk_demo_Android.csproj`

`adb install Projects\Android\bin\Release\net7.0-android\publish\com.companyname.sk_demo-Signed.apk`

.NET8 whoohoo:

`dotnet run --project Projects\Android\sk_demo_Android.csproj` 

Or to use the live simulator:

`dotnet watch`



## If you know git a little:

Clone / Fork / Spork. Do the thing. You know how it works. 



# Tools and Resources

[StereoKit website](https://stereokit.net/)

[PsyToolkit VAAST example](https://www.psytoolkit.org/experiment-library/vaast_images.html)

[Quest Passthrough](https://github.com/StereoKit/StereoKit/tree/master/Examples/StereoKitTest/Tools)

[WikiMedia Search CC0 Images](https://commons.wikimedia.org/w/index.php?search)

[PolyHaven Public 3D Asset Library - Skyboxes](https://polyhaven.com/hdris)

[Smithsonian Scanned Artifacts](https://3d.si.edu/explore)

[StereoKit Learning Resources](https://stereokit.net/Pages/Guides/Learning-Resources.html)

[StereoKit YouTube Playlist](https://youtube.com/playlist?list=PLLhA_jQG6_Hbquqhj6f0V3H3Xm5c1ecA4&si=FnjUjTaRHBlblPd4)




# Panic

If things are not working out too well, maybe these things (again) can help out.

`dotnet new install StereoKit.Templates`

`dotnet new console`

`dotnet add package StereoKit`

`dotnet new sk-multi --force`

Sometimes doing the first one a few times may help. 
I have no idea what the second one does. 

`dotnet workload restore` 

`dotnet workload install wasi-experimental`

Removing project from HMD:

`adb uninstall com.companyname.sk_demo`

Deleting build folder on computer:

`rm -r Projects/Android/bin/Release/net7.0-android`

`rm -r Projects/Android/bin/Release/net8.0-android`

Reboot the machine. Windows likes that.

Alternatively: Despair. 
