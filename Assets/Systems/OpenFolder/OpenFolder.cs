using System.IO;
using UnityEngine;

public abstract class OpenFolder : MonoBehaviour
{
    protected abstract string FolderPath { get; }

    public void Open()
    {
        if (string.IsNullOrEmpty(FolderPath))
        {
            Debug.LogError("FolderPath is null or empty");
            return;
        }

        if (!Directory.Exists(FolderPath))
        {
            Debug.LogError($"Directory with path={FolderPath} not exists.");
            return;
        }

        Application.OpenURL(FolderPath);
    }
}
