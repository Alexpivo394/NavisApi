namespace Build.Options;

[Serializable]
public sealed record PackOptions
{
    public string ContentDirectory { get; init; } = Path.Combine("Alexpivo394.Navis.Api", "Content");
    public string? PinnedDllVersion { get; init; }
    public string? PinnedDllName { get; init; }
}