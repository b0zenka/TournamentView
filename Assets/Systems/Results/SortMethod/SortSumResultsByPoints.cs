using System.Linq;

internal sealed class SortSumResultsByPoints : ISortSumResults
{
    public IPlayerSumResult[] SourtSumResults(IPlayerSumResult[] sumResults)
    {
        return sumResults.OrderByDescending(x => x.Points)
                               .ThenByDescending(x => x.Wins)
                               .ToArray();
    }
}