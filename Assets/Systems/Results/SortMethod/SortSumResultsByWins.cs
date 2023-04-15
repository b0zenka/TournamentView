using System.Linq;

internal sealed class SortSumResultsByWins : ISortSumResults
{
    public IPlayerSumResult[] SourtSumResults(IPlayerSumResult[] sumResults)
    {
        return sumResults.OrderByDescending(x => x.Wins)
                               .ThenByDescending(x => x.Points)
                               .ToArray();
    }
}