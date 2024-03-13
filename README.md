# sk_demo
 

# Installation Guide For New SK Projects

> Open VS Code

> Ctrl + Shift + P --> "Focus on Terminal View"

In the terminal, copy or type the below commands, hitting Enter/Return after each.

`mkdir StereoKit\MyFirstStereoKit`

`cd StereoKit\MyFirstStereoKit`

`dotnet new install StereoKit.Templates`

`dotnet new sk-multi`

> Go to File -> Open Folder -> Open 'MyFirstStereoKit' folder we created.

If your headset is connected:
`dotnet run --project Projects\Android\MyFirstStereoKit_Android.csproj` 

If your headset is not connected:
`dotnet watch`

