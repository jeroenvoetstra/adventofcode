using System.Reflection;

namespace Utility;

public class AssemblyResource
{
    public static Stream? Load(string resourcePath)
    {
        var entryAssembly = Assembly.GetEntryAssembly()!;
        return entryAssembly.GetManifestResourceStream($"{entryAssembly.GetName().Name}.{resourcePath.Replace("\\", ".").Replace("/", ".")}");
    }
}
