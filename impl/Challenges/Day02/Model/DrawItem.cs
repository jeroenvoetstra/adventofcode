namespace Challenges.Day02.Model;

public class DrawItem
{
    public int Number { get; set; }
    public string Color { get; set; } = null!;

    public bool IsValid => Color?.ToLower() switch
    {
        "green" => Number <= Constants.MaxGreen,
        "red" => Number <= Constants.MaxRed,
        "blue" => Number <= Constants.MaxBlue,
        _ => false,
    };
}
