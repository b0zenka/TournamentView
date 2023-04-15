public class CellSumInt : CellInt, IPlayerSumResult
{
    public string Player { get; private set; }
    public int Wins { get; private set; }
    public int Draws { get; private set; }
    public int Defeats { get; private set; }

    public int Points => (int)Value;

    public void SetPlayerName(string name) => Player = name;
    public void AddWin() => Wins++;
    public void AddDraw() => Draws++;
    public void AddDefeat() => Defeats++;
}