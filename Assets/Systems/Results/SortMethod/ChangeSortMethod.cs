using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public abstract class ChangeSortMethod : MonoBehaviour
{
    [SerializeField] private TournamentSummary tournamentSummary;

    protected abstract ISortSumResults SortSummaryMethod { get; }

    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        GetComponent<Toggle>()?.onValueChanged.RemoveAllListeners();
    }

    private async void OnValueChanged(bool isOn)
    {
        Debug.Log($"Change method to: {nameof(SortSummaryMethod)}");
        await tournamentSummary.ChangeSortMethod(SortSummaryMethod);
    }
}