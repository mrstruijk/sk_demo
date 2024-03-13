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