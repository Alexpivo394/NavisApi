using Build.Options;
using EnumerableAsyncProcessor.Extensions;
using Microsoft.Extensions.Options;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Options;
using ModularPipelines.FileSystem;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using Shouldly;
using Sourcy.DotNet;
using File = ModularPipelines.FileSystem.File;

namespace Build.Modules;

/// <summary>
///     Pack the NuGet package.
/// </summary>
[DependsOn<UpdateReadmeModule>(Optional = true)]
[DependsOn<CleanProjectModule>(Optional = true)]
public sealed class PackProjectModule(IOptions<BuildOptions> buildOptions, IOptions<PackOptions> packOptions) : Module
{
    private readonly Dictionary<string, string> _navisFrameworks = new()
    {
        { "2022", "net47" },
        { "2023", "net48" },
        { "2024", "net48" },
        { "2025", "net48" },
    };

    protected override async Task ExecuteModuleAsync(IModuleContext context, CancellationToken cancellationToken)
    {
        var rootContentFolder = context.Git().RootDirectory.GetFolder(packOptions.Value.ContentDirectory);
        var outputFolder = context.Git().RootDirectory.GetFolder(buildOptions.Value.OutputDirectory);
        var targetFolders = string.IsNullOrEmpty(packOptions.Value.PinnedDllVersion)
            ? rootContentFolder.ListFolders().ToArray()
            : [rootContentFolder.GetFolder(packOptions.Value.PinnedDllVersion)];

        targetFolders.Length.ShouldBePositive("No folders were found to pack");

        foreach (var targetFolder in targetFolders)
        {
            var targetFiles = string.IsNullOrEmpty(packOptions.Value.PinnedDllName)
                ? targetFolder.GetFiles(file => file.Extension == ".dll").ToArray()
                : targetFolder.GetFiles(file => file.Extension == ".dll" && file.NameWithoutExtension == packOptions.Value.PinnedDllName).ToArray();

            targetFiles.ShouldNotBeEmpty("No files were found to pack");

            await PackAsync(context, targetFiles, outputFolder, cancellationToken);
        }
    }

    private async Task PackAsync(IPipelineContext context, File[] files, Folder output, CancellationToken cancellationToken)
    {
        await files.SelectAsync(async file =>
            {
                var version = file.Folder!.Name;
                return await context.DotNet().Pack(new DotNetPackOptions
                {
                    ProjectSolution = Projects.Alexpivo394_Navis_Api.FullName,
                    Configuration = "Release",
                    Output = output,
                    Properties = new List<KeyValue>
                    {
                        ("Version", version),
                        ("PackageId", $"Alexpivo394.Navis.Api.{file.NameWithoutExtension}"),
                        ("LibraryName", file.NameWithoutExtension),
                        ("NavisFramework", _navisFrameworks[version[..4]])
                    },
                }, cancellationToken: cancellationToken);
            }, cancellationToken)
            .ProcessOneAtATime();
    }
}