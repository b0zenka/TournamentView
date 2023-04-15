using TMPro;
using UnityEngine;

public class InTournamentPlayersCount : MonoBehaviour, IPlayersInTournamentChangedObserver
{
    [SerializeField]
    private TMP_InputField fieldPlayersInTorunamentCount;

    [SerializeField]
    private PlayersPathsContent playersPathsContent;

    private void Start()
    {
        playersPathsContent?.Register(this);
    }

    private void OnDestroy()
    {
        playersPathsContent?.Unregister(this);
    }

    public void PlayersInTournamentChanged(int playersInTorunament)
    {
        fieldPlayersInTorunamentCount.text = playersInTorunament.ToString();
    }
}