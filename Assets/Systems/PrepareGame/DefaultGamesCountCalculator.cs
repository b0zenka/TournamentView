public class DefaultGamesCountCalculator : IGamesCountCalculator
{
    public int Calculate(int playersCount)
    {
        int sum = 0;

        for (int i = playersCount; i >= 1; i--)
            sum += i;

        return sum;
    }
}
