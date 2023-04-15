using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameValidator : MonoBehaviour, IPlayersInTournamentChangedObserver
{
    [SerializeField]
    private Button btnStartGame;

    [SerializeField]
    private TMP_Text txtErrorInfo;

    [SerializeField]
    private PlayersPathsContent playersPathsContent;

    private void Start()
    {
        playersPathsContent.Register(this);
    }

    private void OnDestroy()
    {
        playersPathsContent?.Unregister(this);
    }

    public void PlayersInTournamentChanged(int playersInTorunament)
    {
        if (playersInTorunament >= 2)
        {
            txtErrorInfo.text = string.Empty;
            btnStartGame.interactable = true;
            return;
        }

        txtErrorInfo.text = "Aby m�c wystartowa� turniej nale�y doda� do niego co najmniej dw�ch graczy.";
        btnStartGame.interactable = false;
    }
}