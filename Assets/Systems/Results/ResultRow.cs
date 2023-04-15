using MPUIKIT;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MPImage))]
public class ResultRow : MonoBehaviour
{
    [SerializeField] private ResultRowSettings settings;

    [Space]
    [SerializeField] private TMP_Text txtPlayerName;

    [SerializeField] private TMP_Text txtPoints;

    [SerializeField] private TMP_Text txtWins;

    [SerializeField] private TMP_Text txtDraws;

    [SerializeField] private TMP_Text txtDefeats;

    private MPImage image;

    private void OnEnable()
    {
        image ??= GetComponent<MPImage>();

        HandleMainColor();
    }

    public void Setup(IPlayerSumResult sumResult, bool hasBestPoints, bool hasBestWins)
    {
        txtPlayerName.text = sumResult.Player;

        txtPoints.text = sumResult.Points.ToString();
        txtPoints.color = settings.GetTextColor(hasBestPoints);

        txtWins.text = sumResult.Wins.ToString();
        txtWins.color = settings.GetTextColor(hasBestWins);

        txtDraws.text = sumResult.Draws.ToString();
        txtDefeats.text = sumResult.Defeats.ToString();
    }

    private void HandleMainColor()
    {
        image.color = settings.GetMainColor(transform.GetSiblingIndex());
    }
}