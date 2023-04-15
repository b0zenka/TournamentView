public sealed class ChangeSortMethodByPoints : ChangeSortMethod
{
    private readonly ISortSumResults sortSummaryMethod = new SortSumResultsByPoints();
    protected override ISortSumResults SortSummaryMethod => sortSummaryMethod;
}
