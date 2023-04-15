using System;
using System.Collections;
using System.Linq;
using Tournament.Logic;
using Tournament.Models;
using UnityEngine;
using UnityEngine.UI;

public class TournamentTable : MonoBehaviour
{
    public static event Action<IPlayerSumResult[]> onEndDisplayTournament;

    public static event Action<float> onUpdateTournamentProgress;

    private readonly byte[,] payoffs = new byte[2, 2]
    {
        {3, 0},
        {5, 1}
    };

    [Tooltip("Prefab dla nazwy gracza kolumnowego")]
    [SerializeField] private CellText colHeaderPrefab;

    [Tooltip("Prefab dla nazwy gracza wierszowego")]
    [SerializeField] private CellText rowHeaderPrefab;

    [Tooltip("Prefab dla wyniku gry pomiêdzy dwoma graczami")]
    [SerializeField] private CellInt resultPrefab;

    [Tooltip("Prefab dla podsumowania wyników gracza")]
    [SerializeField] private CellSumInt sumResultPrefab;

    [Space]
    [SerializeField] private float displaySpeed = .25f;

    [Header("Cell Colors")]
    [SerializeField] private Color oddResultCellColor = Color.white;

    [SerializeField] private Color oddSumResultCellColor = Color.white;

    [SerializeField] private Color evenSumResultColor = Color.white;

    [Header("Sum Result Colors")]
    [SerializeField] private Color bestResultColor = Color.green;

    [SerializeField] private Color worstResultColor = Color.red;

    private Transform ColumnsHeaderContent => colHeaderPrefab.transform.parent;

    private Transform RowsHeaderContent => rowHeaderPrefab.transform.parent;

    private Transform SumResultsContent => sumResultPrefab.transform.parent;

    private GridLayoutGroup ResultsGrid { get; set; }

    private CellSumInt[] SumResultArray { get; set; }

    private CellInt[,] ResultArray { get; set; }

    private void Start()
    {
        //Init required components on start:
        InitComponents();

        //Create torunament:
        var tournament = HandleTorunament();

        //Create clear table for results:
        var players = tournament.GetScoreboard.GetPlayers.ToArray();
        HandleTable(players);

        //Play the tournament:
        tournament.Play();

        //Display results:
        StartCoroutine(DisplayResults(tournament.GetScoreboard, displaySpeed));
    }

    private void OnDestroy()
    {
        onEndDisplayTournament = null;
        onUpdateTournamentProgress = null;
    }

    private Torunament HandleTorunament()
    {
        var paths = TournamentConfig.Instance.PlayersInTournament;
        var payoff = new PayoffMatrix(payoffs);

        Torunament torunament = new Torunament(paths, payoff, TournamentConfig.Instance.Iterations);

        return torunament;
    }

    private void HandleTable(string[] players)
    {
        InitPlayerRows(players);
        InitPlayerCols(players);
        InitPlayerSumResults(players);
        InitPlayerResults(players);
    }

    private void InitComponents()
    {
        ResultsGrid = resultPrefab.GetComponentInParent<GridLayoutGroup>(true);
    }

    private void InitPlayerRows(string[] players)
    {
        InitPlayerHeader(players, rowHeaderPrefab, RowsHeaderContent);
    }

    private void InitPlayerCols(string[] players)
    {
        InitPlayerHeader(players, colHeaderPrefab, ColumnsHeaderContent);
    }

    private void InitPlayerHeader(string[] players, CellText prefab, Transform content)
    {
        foreach (var player in players)
        {
            var newPlayer = Instantiate(prefab, content);
            newPlayer.Value = player;
            newPlayer.gameObject.SetActive(true);
        }
    }

    private void InitPlayerSumResults(string[] players)
    {
        SumResultArray = new CellSumInt[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            SumResultArray[i] = Instantiate(sumResultPrefab, SumResultsContent);
            SumResultArray[i].SetPlayerName(players[i]);
            SumResultArray[i].Value = 0;

            if (i % 2 != 0)
                SumResultArray[i].SetBackgroundColor(oddSumResultCellColor);

            SumResultArray[i].gameObject.SetActive(true);
        }
    }

    private void InitPlayerResults(string[] players)
    {
        ResultsGrid.constraintCount = players.Length;
        ResultArray = new CellInt[players.Length, players.Length];
        int indexCount = players.Length * players.Length;

        for (int playerIndex = 0; playerIndex < indexCount; playerIndex++)
        {
            int firstPlayerIndex = playerIndex / players.Length;
            int secondPlayerIndex = playerIndex % players.Length;

            ResultArray[firstPlayerIndex, secondPlayerIndex] = Instantiate(resultPrefab, ResultsGrid.transform);
            ResultArray[firstPlayerIndex, secondPlayerIndex].Value = null;

            if (firstPlayerIndex % 2 != 0)
                ResultArray[firstPlayerIndex, secondPlayerIndex].SetBackgroundColor(oddResultCellColor);

            ResultArray[firstPlayerIndex, secondPlayerIndex].gameObject.SetActive(true);
        }
    }

    private IEnumerator DisplayResults(IScoreboard scoreboard, float delay)
    {
        var players = scoreboard.GetPlayers.ToArray();

        float allGames = players.Length * players.Length;
        float progress = 0;
        int calculatedGames = 0;

        var wfs = new WaitForSeconds(delay);

        for (int secondPlayerIndex = 0; secondPlayerIndex < players.Length; secondPlayerIndex++)
        {
            string secondPlayer = players[secondPlayerIndex];

            for (int firstPlayerIndex = 0; firstPlayerIndex < players.Length; firstPlayerIndex++)
            {
                string firstPlayer = players[firstPlayerIndex];
                var payoffs = scoreboard.GetPayoffs(firstPlayer, secondPlayer);

                ResultArray[firstPlayerIndex, secondPlayerIndex].Value = payoffs.FirstPlayerPayoff;

                UpdateSumResultView(firstPlayerIndex);
                UpdatePlayerSum(firstPlayerIndex, payoffs);
                yield return wfs;

                progress = ++calculatedGames / allGames;
                onUpdateTournamentProgress?.Invoke(progress);

                UpdateSumResultColors();
            }

            yield return wfs;
        }

        onEndDisplayTournament?.Invoke(SumResultArray);
    }

    private void UpdateSumResultView(int firstPlayerIndex)
    {
        if (firstPlayerIndex < 0 || firstPlayerIndex > SumResultArray.Length)
        {
            Debug.LogAssertion($"FirstPlayerIndex ({firstPlayerIndex}) is out of SumResultArray range.", gameObject);
            return;
        }

        var resultSum = SumResultsInColumn(firstPlayerIndex);
        SumResultArray[firstPlayerIndex].Value = resultSum;
    }

    private void UpdatePlayerSum(int firstPlayerIndex, IPayoffs payoffs)
    {
        var sumResult = SumResultArray[firstPlayerIndex];

        if (payoffs.FirstPlayerPayoff > payoffs.SecondPlayerPayoff)
            sumResult.AddWin();
        else if (payoffs.FirstPlayerPayoff < payoffs.SecondPlayerPayoff)
            sumResult.AddDefeat();
        else
            sumResult.AddDraw();
    }

    private void UpdateSumResultColors()
    {
        int bestResult = (int)SumResultArray.Max(x => x.Value);
        int worstResult = (int)SumResultArray.Min(x => x.Value);

        for (int i = 0; i < SumResultArray.Length; i++)
        {
            var value = SumResultArray[i].Value;
            Color setColor = oddSumResultCellColor;

            if (value.Value == bestResult)
                setColor = bestResultColor;
            else if (value.Value == worstResult)
                setColor = worstResultColor;
            else if (i % 2 != 0)
                setColor = evenSumResultColor;

            SumResultArray[i].SetBackgroundColor(setColor);
        }
    }

    private int SumResultsInColumn(int columnIndex)
    {
        int columnsCount = ResultArray.GetLength(1);

        if (columnIndex < 0 || columnIndex > columnsCount)
            return 0;

        int sum = 0;

        for (int i = 0; i < columnsCount; i++)
        {
            if (ResultArray[columnIndex, i].Value.HasValue)
                sum += ResultArray[columnIndex, i].Value.Value;
        }

        return sum;
    }
}