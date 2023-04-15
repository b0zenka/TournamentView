using System.Collections.Generic;
using System.Linq;
using Tournament.Logic;
using UnityEngine;

public class PlayersPathsContent : MonoBehaviour, IPlayersInTournamentChanged
{
    [SerializeField]
    private TogglePlayerData tglPlayerPrefab;

    private PlayersDataPool playerDataPool;

    private List<IPlayersInTournamentChangedObserver> playersInTournamentObservers;

    private PlayersDataPool PlayersDataPool => playerDataPool ??= new PlayersDataPool(tglPlayerPrefab);

    private void Awake()
    {
        playersInTournamentObservers = new List<IPlayersInTournamentChangedObserver>();
    }

    private void Start()
    {
        RefreshPlayers();
    }

    public void RefreshPlayers()
    {
        PlayersDataPool.FreeAll();

        var paths = Torunament.LoadPlayerPaths(Application.streamingAssetsPath);

        foreach (var path in paths)
            HandlePlayer(path);
    }

    public void InvertSelection()
    {
        PlayersDataPool.InverseSelection();
    }

    private void HandlePlayer(string path)
    {
        var playerDataView = (ITogglePlayerData)PlayersDataPool.Peek();
        playerDataView.SetupToggle(path, OnIncludedInTournamentPlayersCountChanged);
    }

    public void Register(IPlayersInTournamentChangedObserver observer)
    {
        if (playersInTournamentObservers.Contains(observer))
            return;

        playersInTournamentObservers.Add(observer);
        observer.PlayersInTournamentChanged(PlayersDataPool.GetPlayersIncludedInTournament.Count());
    }

    public void Unregister(IPlayersInTournamentChangedObserver observer)
    {
        playersInTournamentObservers.Remove(observer);
    }

    public void OnIncludedInTournamentPlayersCountChanged()
    {
        var includedPlayersInTorunament = PlayersDataPool.GetPlayersIncludedInTournament;
        TournamentConfig.Instance.PlayersInTournament = includedPlayersInTorunament.Select(x => x.PlayerPath).ToArray();
        playersInTournamentObservers.ForEach(x => x.PlayersInTournamentChanged(includedPlayersInTorunament.Count()));
    }
}