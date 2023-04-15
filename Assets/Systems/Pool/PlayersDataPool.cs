using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersDataPool : Pool<ITogglePlayerData>
{
    private readonly TogglePlayerData playerDataPrefab;
    private readonly Transform tglPlayerContent;

    public IEnumerable<ITogglePlayerData> GetPlayersIncludedInTournament
        => base.poolElements.Where(x => x.IncludedInTournament);

    public PlayersDataPool(TogglePlayerData playerDataPrefab)
    {
        this.playerDataPrefab = playerDataPrefab;
        tglPlayerContent = playerDataPrefab.transform.parent;
    }

    public void InverseSelection()
        => poolElements.ForEach(poolElement => poolElement.IncludedInTournament = !poolElement.IncludedInTournament);

    protected override ITogglePlayerData CreateNewPlayerData()
        => MonoBehaviour.Instantiate(playerDataPrefab, tglPlayerContent);
}
