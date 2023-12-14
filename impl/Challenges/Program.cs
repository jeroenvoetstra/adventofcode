using System.Reflection;
using Utility;

if (args.Length > 0 && args[0] == "all")
{
    ExecuteAll();
}
else
{
    ExecuteChallenge<Challenges.Day13.Part2>();
}
return;

static void ExecuteChallenge<TChallenge>()
    where TChallenge : AdventOfCodeChallenge, new()
{
    var challenge = new TChallenge();
    challenge.Execute();
    Console.WriteLine();
    Console.WriteLine();
}

#pragma warning disable IL2067
static void ExecuteChallengeType(Type challengeType)
{
    var challenge = (AdventOfCodeChallenge)Activator.CreateInstance(challengeType)!;
    challenge.Execute();
    Console.WriteLine();
    Console.WriteLine();
}
#pragma warning restore IL2067

#pragma warning disable IL2026
static void ExecuteAll()
{
    var types = Assembly.GetEntryAssembly()!.GetTypes();
    foreach (var type in types.OrderBy((t) => t.FullName))
    {
        if (type.BaseType == typeof(AdventOfCodeChallenge))
        {
            ExecuteChallengeType(type);
        }
    }
}
#pragma warning restore IL2026
