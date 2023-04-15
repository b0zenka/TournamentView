using TMPro;
using UnityEngine;

public class CalculateGamesCount : MonoBehaviour, IPlayersInTournamentChangedObserver
{
    [SerializeField]
    private TMP_InputField fieldGamesCount;

    [SerializeField]
    private PlayersPathsContent playersPathsContent;

    private IGamesCountCalculator gamesCountCalculator;

    private void Start()
    {
        gamesCountCalculator = new DefaultGamesCountCalculator();
        playersPathsContent?.Register(this);
    }

    private void OnDestroy()
    {
        playersPathsContent?.Unregister(this);
    }

    public void PlayersInTournamentChanged(int playersInTorunament)
    {
        int gamesCount = gamesCountCalculator.Calculate(playersInTorunament);
        fieldGamesCount.text = gamesCount.ToString();
    }
}
