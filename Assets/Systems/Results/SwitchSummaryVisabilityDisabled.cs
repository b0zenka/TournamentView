using UnityEngine;

public class SwitchSummaryVisabilityDisabled : ISwitchSummaryVisability
{
    public ISwitchSummaryVisability Switch(CanvasGroup canvasGroup)
    {
        return this;
    }
}
