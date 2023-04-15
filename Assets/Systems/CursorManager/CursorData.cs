using UnityEngine;

[System.Serializable]
public class CursorData
{
    [SerializeField]
    private CursorType cursorType;

    [SerializeField]
    private Texture2D cursor;

    public CursorType CursorType => cursorType;
    public Texture2D Cursor => cursor;
}
