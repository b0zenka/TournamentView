using UnityEngine;

public interface ITournamentConfig
{
    string[] PlayersInTournament { get; set; }

    int Iterations { get; set; }
}

public class TournamentConfig : MonoBehaviour, ITournamentConfig
{
    public static ITournamentConfig Instance { get; private set; }

    public string[] PlayersInTournament { get; set; }

    public int Iterations { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Instance = this;
    }
}