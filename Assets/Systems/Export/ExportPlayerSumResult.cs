using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ITournamentSumResults))]
public class ExportPlayerSumResult : MonoBehaviour
{
    [SerializeField] Button btnExport;

    private ITournamentSumResults tournamentSumResults;

    private readonly IExportDestination exportDestination = new StreamingAssetsDestination();
    private readonly IExportPlayerSumResult exportMethod = new PlayerSumResultCSVExport();

    private void Start()
    {
        tournamentSumResults = GetComponent<ITournamentSumResults>();
        btnExport.onClick.AddListener(async () => { await ExportAsync(tournamentSumResults.SortedSumResults); });
    }

    private void OnDestroy()
    {
        btnExport?.onClick.RemoveAllListeners();
    }

    public async Task ExportAsync(IEnumerable<IPlayerSumResult> playerSumResults)
    {
        if (exportDestination == null || exportMethod == null)
        {
            Debug.LogError("Export destination or method is null", gameObject);
            return;
        }

        if (playerSumResults == null || playerSumResults.Count() == 0)
        {
            Debug.LogError("Nothing to export. playerSumResults parameter is null or empty", gameObject);
            return;
        }

        string path = exportDestination.GetPath();
        var isSuccess = await exportMethod.ExportAsync(path, playerSumResults);

        if (isSuccess) Application.OpenURL(path);
        else Debug.LogError("Something went wrong.", gameObject);
    }
}
