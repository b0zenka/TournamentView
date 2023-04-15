using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class OpenTMPLink : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{
    private TMP_Text tmpText;

    private void Start()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (TryGetLinkUrl(eventData, out var url))
            CursorManager.ChangeCursor(CursorType.Link);
        else
            CursorManager.ChangeCursor(CursorType.Default);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TryGetLinkUrl(eventData, out var url))
            Application.OpenURL(url);
    }

    private bool TryGetLinkUrl(PointerEventData eventData, out string url)
    {

        if (eventData.button != PointerEventData.InputButton.Left)
        {
            url = null;
            return false;
        }

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpText, Input.mousePosition, null);

        if (linkIndex == -1)
        {
            url = null;
            return false;
        }

        url = tmpText.textInfo.linkInfo[linkIndex].GetLinkID();
        return true;
    }
}
