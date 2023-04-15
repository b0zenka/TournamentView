public sealed class ChangeSortMethodByWins : ChangeSortMethod
{
    private readonly ISortSumResults sortSummaryMethod = new SortSumResultsByWins();
    protected override ISortSumResults SortSummaryMethod => sortSummaryMethod;
}
