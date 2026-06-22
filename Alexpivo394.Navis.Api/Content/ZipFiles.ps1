$assemblies = @(
    "AdWindows"
    "Autodesk.Navisworks.Api"
    "Autodesk.Navisworks.Automation"
    "Autodesk.Navisworks.Clash"
    "Autodesk.Navisworks.ComApi"
    "Autodesk.Navisworks.Controls"
    "Autodesk.Navisworks.Resolver"
    "Autodesk.Navisworks.Takeoff"
    "Autodesk.Navisworks.Timeliner"
)

$defaultPath = "C:\Program Files\Autodesk\Navisworks Manage 2023"
$input = Read-Host "Path to files (Press Enter for $defaultPath)"
$path = if ([string]::IsNullOrWhiteSpace($input)) { $defaultPath } else { $input.Trim('"').Trim("'") }

if (-not (Test-Path $path)) {
    Write-Error "Path not found: $path"
    exit 1
}

Push-Location $path

$files = $assemblies | ForEach-Object { "$_.dll", "$_.xml" } | Where-Object { Test-Path $_ }

if (-not $files) {
    Write-Error "No matching files found in: $path"
    Pop-Location
    exit 1
}

$destination = "$env:USERPROFILE\Desktop\NavisFiles.zip"
Compress-Archive -Path $files -DestinationPath $destination -Force

Pop-Location

Write-Host "Done! Archive saved to: $destination"