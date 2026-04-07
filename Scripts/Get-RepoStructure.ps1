# Récupère le dossier où se trouve le script
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Dossier Output dans Scripts
$outputDir = Join-Path $scriptDir "Output"

# Création du dossier Output si nécessaire
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir | Out-Null
}

# Racine du repo = working directory
$root = (Get-Location).Path

# Génère la structure du repo (profondeur 2)
Get-ChildItem -Directory -Recurse -Depth 2 |
    ForEach-Object {
        $_.FullName.Replace($root, ".")
    } |
    Set-Content "$outputDir\repoStructure.txt" -Encoding UTF8

Write-Host "✔ Structure du repo générée dans Scripts/Output/repoStructure.txt" -ForegroundColor Green