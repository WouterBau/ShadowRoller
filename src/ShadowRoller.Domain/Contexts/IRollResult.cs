public interface IRollResult
{
    public IEnumerable<int> DiceResults { get; init; }
    public string ToString(string player);
}