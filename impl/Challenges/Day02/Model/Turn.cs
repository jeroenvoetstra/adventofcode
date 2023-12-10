namespace Challenges.Day02.Model;

public class Turn
{
    public string Raw { get; set; } = null!;

    public IEnumerable<DrawItem> Items { get; set; } = null!;

    public (int Red, int Green, int Blue) Set =>
        (
            Items.Where((draw) => draw.Color.ToLower() == "red").FirstOrDefault()?.Number ?? 0,
            Items.Where((draw) => draw.Color.ToLower() == "green").FirstOrDefault()?.Number ?? 0,
            Items.Where((draw) => draw.Color.ToLower() == "blue").FirstOrDefault()?.Number ?? 0
            );
}
