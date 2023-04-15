using System;

public interface ITogglePlayerData : IPoolElement
{
    string PlayerPath { get; }

    bool IncludedInTournament { get; set; }

    void SetupToggle(string path, Action onToggleChanged);
}