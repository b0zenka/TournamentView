using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TournamentSummary : MonoBehaviour, ITournamentSumResults
{
    [SerializeField] private ResultRow rowPrefab;
    [SerializeField] CanvasGroup canvasGroup;

    [Header("Show/Hide Settings")]
    [SerializeField] KeyCode showHideVisability = KeyCode.H;
    [SerializeField] TMP_Text txtShowHide;

    private Transform RowsContent => rowPrefab.transform.parent;

    public IPlayerSumResult[] SortedSumResults { get; private set; }
    private ISwitchSummaryVisability switchSummaryVisability = new SwitchSummaryVisabilityDisabled();

    private void Start()
    {
        HandleCanvasGroup();
        TournamentTable.onEndDisplayTournament += OnDisplaySummary;
        txtShowHide.text = $"Naciœnij <b>{showHideVisability}</b> aby pokazaæ/ukryæ okno";
    }

    private void Update()
    {
        HandleSwitchVisability();
    }

    private void OnDestroy()
    {
        TournamentTable.onEndDisplayTournament -= OnDisplaySummary;
    }

    public async Task ChangeSortMethod(ISortSumResults sortMethod)
    {
        await ClearRows();

        SortedSumResults = sortMethod.SourtSumResults(SortedSumResults);
        DisplayRows(SortedSumResults);
    }

    private void OnDisplaySummary(IPlayerSumResult[] sumResults)
    {
        Debug.Log("OnDisplaySummary: Start");

        LeanTween.value(gameObject, (progress) => { canvasGroup.alpha = progress; }, 0, 1f, .5f)
        .setOnComplete(async () =>
        {
            SortedSumResults = sumResults;
            await ChangeSortMethod(new SortSumResultsByPoints());
            canvasGroup.blocksRaycasts = true;

            switchSummaryVisability = new SwitchSummaryVisabilityHide();
        });

        Debug.Log("OnDisplaySummary: End");
    }

    private void DisplayRows(IPlayerSumResult[] sumResults)
    {
        var bestPoints = sumResults.Max(x => x.Points);
        var bestWins = sumResults.Max(x => x.Wins);

        foreach (var sumResult in sumResults)
        {
            DisplayRow(sumResult, sumResult.Points >= bestPoints, sumResult.Wins >= bestWins);
        }
    }

    private void DisplayRow(IPlayerSumResult sumResult, bool hasBestPoints, bool hasBestWins)
    {
        var row = Instantiate(rowPrefab, RowsContent);
        row.Setup(sumResult, hasBestPoints, hasBestWins);
        row.gameObject.SetActive(true);
    }

    private void HandleCanvasGroup()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    private void HandleSwitchVisability()
    {
        if (Input.GetKeyDown(showHideVisability))
            switchSummaryVisability = switchSummaryVisability.Switch(canvasGroup);

    }

    private async Task ClearRows()
    {
        Debug.Log("ClearRows: Start");

        while (RowsContent.childCount > 1)
        {
            Destroy(RowsContent.GetChild(1).gameObject);
            await Task.Yield();
        }

        Debug.Log("ClearRows: End");
    }
}
