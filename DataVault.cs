using System;
using System.IO;
using StereoKit;


public class DataVault
{
    const string FilePath = "/sdcard/Documents/"; // Careful when using this on other HMDs than Quest, they may have different file structures
    const string fileNameFormat = "yyyyMMdd-HHmm"; // YearMonthDay-HourMinute
    const string FileType = ".csv"; // .txt would have been fine too

    string _output = String.Empty;


    /// <summary>
    /// Data comes in as a 'list'. Add as much data as you want!
    /// </summary>
    /// <param name="newDataSet"></param>
    public void StoreData(string[] newDataSet)
    {
        foreach (var newData in newDataSet)
        {
            _output += newData.ToString() + ", "; // Add all data to the output text
        }

        var fileName = String.Concat(DateTime.Now.ToString(fileNameFormat), FileType); // Make a name for yourself
        var file = Path.Combine(FilePath, fileName);

        Platform.WriteFile(file, _output); // Actually write the file
    }
}