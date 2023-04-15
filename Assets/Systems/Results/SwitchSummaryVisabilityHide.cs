using UnityEngine;

public class SwitchSummaryVisabilityHide : ISwitchSummaryVisability
{
    private const float TO_VALUE = 0;
    private const float TIME = .2f;

    public ISwitchSummaryVisability Switch(CanvasGroup canvasGroup)
    {
        LeanTween.value(canvasGroup.gameObject, (value) =>
        {
            canvasGroup.alpha = value;
        }, canvasGroup.alpha, TO_VALUE, TIME)
        .setOnStart(() =>
        {
            canvasGroup.blocksRaycasts = false;
        });

        return new SwitchSummaryVisabilityShow();
    }
}
