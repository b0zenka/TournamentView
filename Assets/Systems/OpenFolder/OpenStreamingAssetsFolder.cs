using UnityEngine;

public class OpenStreamingAssetsFolder : OpenFolder
{
    protected override string FolderPath => Application.streamingAssetsPath;
}
