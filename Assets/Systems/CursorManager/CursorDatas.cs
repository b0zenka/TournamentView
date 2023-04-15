using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cursor Data", menuName = "Create Cursor Data")]
public class CursorDatas : ScriptableObject
{
    [SerializeField] CursorData[] cursorDatas;

    public Dictionary<CursorType, Texture2D> GetCursorsData()
        => cursorDatas.ToDictionary(x => x.CursorType, x => x.Cursor);
}
