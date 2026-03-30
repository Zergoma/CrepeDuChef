$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$outputDir = Join-Path $scriptDir "Output"

if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir | Out-Null
}

$root = (Get-Location).Path

Get-ChildItem -Recurse -File |
    Where-Object { $_.FullName -notmatch '(?i)\\(bin|obj)\\' } |
    ForEach-Object {
        $_.FullName.Replace($root, ".")
    } |
    Set-Content "$outputDir\repoFiles.txt" -Encoding UTF8
	
Write-Host "✔ Liste des fichiers générée dans Scripts/Output/repoFiles.txt" -ForegroundColor Green