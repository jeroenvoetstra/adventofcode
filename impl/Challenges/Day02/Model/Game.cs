namespace Challenges.Day02.Model;

public class Game
{
    public string Raw { get; set; } = null!;

    public int Number { get; set; }
    public IEnumerable<Turn> Turns { get; set; } = null!;

    public Dictionary<string, int> Summary => Turns.SelectMany((item) => item.Items).GroupBy((item) => item.Color).ToDictionary((item) => item.Key, (item) => item.Sum((subItem) => subItem.Number));

    public (int Red, int Green, int Blue) MinimumSet =>
        (
            Turns.OrderByDescending((turn) => turn.Set.Red).FirstOrDefault()?.Set.Red ?? 0,
            Turns.OrderByDescending((turn) => turn.Set.Green).FirstOrDefault()?.Set.Green ?? 0,
            Turns.OrderByDescending((turn) => turn.Set.Blue).FirstOrDefault()?.Set.Blue ?? 0
            );
    public int Power => MinimumSet.Red * MinimumSet.Green * MinimumSet.Blue;
}
