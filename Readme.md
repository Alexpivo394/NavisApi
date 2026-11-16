<p align="center">
    <h1 align="center">Navisworks API</h1>
    <p align="center">Autodesk Navisworks API assemblies as NuGet packages, for Navisworks 2022–2025</p>
    <p align="center">
        <a href="https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Api"><img src="https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Api?label=nuget" alt="NuGet version"></a>
        <a href="https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Api"><img src="https://img.shields.io/nuget/dt/Alexpivo394.Navis.Api.Autodesk.Navisworks.Api?label=downloads" alt="NuGet downloads"></a>
        <a href="https://github.com/Alexpivo394/NavisApi/blob/main/License.md"><img src="https://img.shields.io/badge/license-MIT-blue" alt="MIT license"></a>
    </p>
</p>

Reference the Navisworks API from NuGet, not from `C:\Program Files\Autodesk`. A Navisworks add-in then builds on a machine without Navisworks: a build server, a colleague's laptop, a fresh checkout.

Each package holds one original assembly from a Navisworks installation image, with the IntelliSense documentation where Autodesk ships it. The assemblies are compile-time references: they are not copied to the output folder, and at run time the add-in uses the ones Navisworks has already loaded.

## Usage

The package version starts with the Navisworks release, so one property selects the API for the whole project:

```xml
<PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <NavisworksVersion>2024</NavisworksVersion>
</PropertyGroup>

<ItemGroup>
    <PackageReference Include="Alexpivo394.Navis.Api.Autodesk.Navisworks.Api" Version="$(NavisworksVersion).*"/>
    <PackageReference Include="Alexpivo394.Navis.Api.Autodesk.Navisworks.Clash" Version="$(NavisworksVersion).*"/>
</ItemGroup>
```

To build one add-in for several releases, set `NavisworksVersion` from the build configuration.

## Packages

Every package ID is `Alexpivo394.Navis.Api.` followed by the assembly name.

| Assembly | What it is for | Package |
|---|---|---|
| `Autodesk.Navisworks.Api` | The core API: documents, models, selection, properties, plug-ins | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Api?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Api) |
| `Autodesk.Navisworks.Automation` | Driving Navisworks from another process | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Automation?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Automation) |
| `Autodesk.Navisworks.Clash` | Clash Detective: tests and results | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Clash?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Clash) |
| `Autodesk.Navisworks.ComApi` | The bridge from the .NET API to the COM API | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.ComApi?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.ComApi) |
| `Autodesk.Navisworks.Controls` | Navisworks viewer controls for your own application | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Controls?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Controls) |
| `Autodesk.Navisworks.Resolver` | Finding the installed Navisworks assemblies at run time | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Resolver?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Resolver) |
| `Autodesk.Navisworks.Takeoff` | Quantification | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Takeoff?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Takeoff) |
| `Autodesk.Navisworks.Timeliner` | TimeLiner: tasks and 4D simulation | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.Autodesk.Navisworks.Timeliner?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.Autodesk.Navisworks.Timeliner) |
| `AdWindows` | The Autodesk ribbon; Navisworks 2024 only | [![NuGet](https://img.shields.io/nuget/v/Alexpivo394.Navis.Api.AdWindows?label=)](https://www.nuget.org/packages/Alexpivo394.Navis.Api.AdWindows) |

## Supported releases

| Navisworks | Package version | Target framework |
|---|---|---|
| 2022 | `2022.*` | `net47` |
| 2023 | `2023.*` | `net48` |
| 2024 | `2024.*` | `net48` |
| 2025 | `2025.*` | `net48` |

Need another assembly or release? [Open an issue](https://github.com/Alexpivo394/NavisApi/issues/new/choose).

## Adding a release

1. Copy the assemblies and their `.xml` documentation from the Navisworks installation into `Alexpivo394.Navis.Api/Content/<version>`, for example `Content/2026.0.0`.
2. Add the release and its target framework to `PackProjectModule` in `build/Modules`.
3. Push a tag. The workflow packs the assemblies and publishes the packages to nuget.org.

To pack locally into `output/`:

```powershell
cd build
dotnet run -c Release -- pack
```

## Acknowledgements

Thanks to [Nice3point](https://github.com/Nice3point) for the advice and the code this project builds on. His [Revit API packages](https://github.com/Nice3point/RevitApi) are the model for this repository.

## License

The packaging code is under the [MIT license](https://github.com/Alexpivo394/NavisApi/blob/main/License.md). The Navisworks assemblies belong to Autodesk and are subject to the Autodesk license terms.
