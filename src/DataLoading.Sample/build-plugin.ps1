## get location on user's drive
$basePath = Get-Location

push-location ./bin/Debug

Write-Host $basePath

# plugin tool is required to build plugin for loading!
$command = "..\..\..\..\lib\plugin-tool\Sharedo.PluginTool.exe"

$arg1 = "$basePath\manifest.json"
$arg2 = "$basePath\bin\Debug"

Write-Host $command "$arg1" "$arg2"

cmd /c "$command" "$arg1" "$arg2"

## reset the path, so we can run it again
Set-Location $basePath