using System.Collections.Generic;
using UnityEngine;

public static class CursorManager
{
    private static readonly Dictionary<CursorType, Texture2D> cursorDatas;

    static CursorManager()
    {
        CursorDatas cursorDatasScriptableObject = Resources.Load<CursorDatas>("CursorDatas");

        if (!cursorDatasScriptableObject)
        {
            Debug.LogError("CursorDatas not found.");
            return;
        }

        cursorDatas = cursorDatasScriptableObject.GetCursorsData();
        ChangeCursor(CursorType.Default);
    }

    public static void ChangeCursor(CursorType cursorType)
    {
        if (!cursorDatas.TryGetValue(cursorType, out var cursor))
        {
            Debug.LogError($"Cursor for type = {cursorType} not found.");
            return;
        }

        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}