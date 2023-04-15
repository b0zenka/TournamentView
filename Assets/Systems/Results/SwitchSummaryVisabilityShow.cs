using UnityEngine;

public class SwitchSummaryVisabilityShow : ISwitchSummaryVisability
{
    private const float TO_VALUE = 1f;
    private const float TIME = .2f;

    public ISwitchSummaryVisability Switch(CanvasGroup canvasGroup)
    {
        LeanTween.value(canvasGroup.gameObject, (value) =>
        {
            canvasGroup.alpha = value;
        }, canvasGroup.alpha, TO_VALUE, TIME)
        .setOnComplete(() =>
        {
            canvasGroup.blocksRaycasts = true;
        });

        return new SwitchSummaryVisabilityHide();
    }
}