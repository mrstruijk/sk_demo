# sk_demo
 

# Installation Guide For New SK Projects

Open VS Code

> Ctrl + Shift + P --> "Focus on Terminal View"

In the terminal, copy or type the below commands, hitting Enter/Return after each.

`mkdir StereoKit\MyFirstStereoKit`

`cd StereoKit\MyFirstStereoKit`

`dotnet new install StereoKit.Templates`

`dotnet new sk-multi`

> Go to File -> Open Folder -> Open 'MyFirstStereoKit' folder we created.

If your headset is connected:

`dotnet run --project Projects\Android\MyFirstStereoKit_Android.csproj` 

Or to use the live simulator:

`dotnet watch`



# Tools and Resources

[PsyToolkit VAAST example](https://www.psytoolkit.org/experiment-library/vaast_images.html)

[Quest Passthrough](https://github.com/StereoKit/StereoKit/tree/master/Examples/StereoKitTest/Tools)

[WikiMedia Search CC0 Images](https://commons.wikimedia.org/w/index.php?search)

[PolyHaven Public 3D Asset Library - Skyboxes](https://polyhaven.com/hdris)

[Smithsonian Scanned Artifacts](https://3d.si.edu/explore)



# Learning Resources

[StereoKit Learning Resources](https://stereokit.net/Pages/Guides/Learning-Resources.html)

[StereoKit YouTube Playlist](https://youtube.com/playlist?list=PLLhA_jQG6_Hbquqhj6f0V3H3Xm5c1ecA4&si=FnjUjTaRHBlblPd4)




# Panic

If things are not working out too well, maybe these things can help out.

`dotnet new console`
`dotnet add package StereoKit`
`dotnet new sk-multi --force`

Sometimes doing the first one a few times may help. 
I have no idea what the second one does. 

`dotnet workload restore` 
`dotnet workload install wasi-experimental`

The first is to build the project.
The second is to push the .NET7 version to the HMD.
The last is the same, but then for if you're using .NET8.

`dotnet publish -c Release Projects\Android\sk_demo_Android.csproj`
`adb install Projects\Android\bin\Release\net7.0-android\publish\com.companyname.sk_demo-Signed.apk`
`adb install Projects\Android\bin\Release\net8.0-android\publish\com.companyname.sk_demo-Signed.apk`

Removing project from HMD / deleting build folder on computer.

`adb uninstall com.companyname.sk_demo`
`rm -r Projects/Android/bin/Release/net7.0-android`
`rm -r Projects/Android/bin/Release/net8.0-android`
