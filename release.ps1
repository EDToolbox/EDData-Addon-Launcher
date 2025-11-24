#!/usr/bin/env pwsh
<#
.SYNOPSIS
Release-Management Skript für Elite Dangerous Addon Launcher V2

.DESCRIPTION
Vereinfacht die Versionsverwaltung und Release-Erstellung

.PARAMETER Version
Neue Versionsnummer (z.B. 2.0.1)

.PARAMETER Message
Release-Notizen/Beschreibung

.EXAMPLE
.\release.ps1 -Version "2.0.1" -Message "Bug fixes and security updates"
#>

param(
    [Parameter(Mandatory=$true)]
    [string]$Version,
    
    [string]$Message = "Release $Version"
)

$ErrorActionPreference = "Stop"

# Farben
$InfoColor = "Cyan"
$SuccessColor = "Green"
$ErrorColor = "Red"

function Write-Info { Write-Host "[INFO] $args" -ForegroundColor $InfoColor }
function Write-Success { Write-Host "[✓] $args" -ForegroundColor $SuccessColor }
function Write-Error { Write-Host "[✗] $args" -ForegroundColor $ErrorColor }

# Validiere Version-Format
if ($Version -notmatch '^\d+\.\d+\.\d+$') {
    Write-Error "Ungültiges Versionsformat! Verwende: X.Y.Z (z.B. 2.0.1)"
    exit 1
}

$TagName = "v$Version"

Write-Info "Release-Prozess für Version: $Version"
Write-Info "Git-Tag: $TagName"

# Schritt 1: Versionsnummer aktualisieren
Write-Info "Aktualisiere Versionsnummern..."

# Update installer.nsi
$installerPath = "installer\installer.nsi"
if (Test-Path $installerPath) {
    $content = Get-Content $installerPath -Raw
    $content = $content -replace 'WriteRegStr HKCU.*?"Version"\s+"[\d.]+?"', `
        "WriteRegStr HKCU `"Software\Elite Dangerous Addon Launcher V2`" `"Version`" `"$Version`""
    Set-Content $installerPath $content
    Write-Success "installer.nsi aktualisiert"
}

# Schritt 2: Git Commit und Tag
Write-Info "Erstelle Git Commit und Tag..."

git add -A
git commit -m "chore(release): bump version to $Version

## Changes
$Message"

if ($LASTEXITCODE -ne 0) {
    Write-Error "Git commit fehlgeschlagen!"
    exit 1
}

Write-Success "Commit erstellt"

# Schritt 3: Git Tag
git tag -a $TagName -m "Release $Version`n`n$Message"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Git tag fehlgeschlagen!"
    exit 1
}

Write-Success "Tag erstellt: $TagName"

# Schritt 4: Push
Write-Info "Push zu Remote..."
git push origin master
git push origin $TagName

if ($LASTEXITCODE -ne 0) {
    Write-Error "Git push fehlgeschlagen!"
    exit 1
}

Write-Success "Push erfolgreich"

Write-Host ""
Write-Success "Release erfolgreich erstellt!"
Write-Host ""
Write-Host "Release-Details:" -ForegroundColor $InfoColor
Write-Host "  Version: $Version"
Write-Host "  Tag: $TagName"
Write-Host "  GitHub Release wird automatisch erstellt..."
Write-Host ""
Write-Host "GitHub Actions wird jetzt:" -ForegroundColor $InfoColor
Write-Host "  1. Das Projekt kompilieren"
Write-Host "  2. NSIS Installer erzeugen"
Write-Host "  3. GitHub Release mit Dateien erstellen"
Write-Host ""
Write-Host "Status überprüfen: https://github.com/EDToolbox/Elite-Dangerous-Addon-Launcher-V2/actions"
