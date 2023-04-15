public interface IPlayersInTournamentChanged
{
    void Register(IPlayersInTournamentChangedObserver observer);
    void Unregister(IPlayersInTournamentChangedObserver observer);
    void OnIncludedInTournamentPlayersCountChanged();

}
