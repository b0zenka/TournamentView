using System;
using System.IO;
using UnityEngine;

public class StreamingAssetsDestination : IExportDestination
{
    public string GetPath() => Path.Combine(Application.streamingAssetsPath, "Exports", $"Results_{GetFormattedDateTime()}.csv");

    private string GetFormattedDateTime()
    {
        DateTime now = DateTime.Now;
        string formattedDate = $"{now.Day:00}-{now.Month:00}-{now.Year:0000}";
        string formattedTime = $"{now.Hour:00}-{now.Minute:00}";

        return $"{formattedDate}_{formattedTime}";
    }
}