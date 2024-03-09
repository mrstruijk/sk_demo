using System;
using System.IO;
using StereoKit;


public class DataVault
{
    const string FILE_PATH = "/sdcard/Documents/";
    private string fileName = String.Empty;
    private string output = String.Empty;

 
    public void StoreData(float reactionTime)
    {
        output += reactionTime.ToString() + ", ";
        fileName = String.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), ".csv");

        var file = Path.Combine(FILE_PATH, fileName);
        Platform.WriteFile(file, output);
    }
}