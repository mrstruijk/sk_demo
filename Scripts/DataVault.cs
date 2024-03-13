using System;
using System.IO;
using StereoKit;


public class DataVault
{
    const string FilePath = "/sdcard/Documents/";
    const string fileNameFormat = "yyyyMMdd-HHmm";
    const string FileType = ".csv";


    public void StoreData(string[] newDataSet)
    {
        string output = String.Empty;

        foreach (var newData in newDataSet)
        {
            output += newData.ToString() + ", ";
        }

        var file = Path.Combine(FilePath, DateTime.Now.ToString(fileNameFormat), FileType);

        Platform.WriteFile(file, output);
    }
}