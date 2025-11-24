# CI/CD Release-Pipeline für Elite Dangerous Addon Launcher V2

Automatische Build-, Test- und Release-Verarbeitung mit GitHub Actions.

## 📋 Übersicht

Die CI/CD-Pipeline automatisiert:
- ✅ .NET 8.0 Projekt-Builds
- ✅ NSIS Installer-Generierung
- ✅ GitHub Release-Erstellung
- ✅ Automatische Versionsverwaltung

## 🚀 Release-Prozess

### Automatisches Release (empfohlen)

```powershell
# Versionsnummer erhöhen und Release erstellen
.\release.ps1 -Version "2.0.1" -Message "Bug fixes and security updates"
```

Das Skript automatisiert:
1. Version in `installer.nsi` aktualisieren
2. Git Commit mit Versionsbump
3. Git Tag erstellen (z.B. `v2.0.1`)
4. Push zu Remote-Repository
5. Triggert GitHub Actions Workflow

### Manuelles Release (nicht empfohlen)

```bash
git tag v2.0.1
git push origin v2.0.1
```

## 📦 GitHub Actions Workflow

### Trigger
- Push zu Tag mit Format `v*` (z.B. `v2.0.0`)
- Manueller Trigger über "Run workflow"

### Schritte

```yaml
1. Checkout Code
   ↓
2. Setup .NET 8.0
   ↓
3. Restore Dependencies
   ↓
4. Build Release
   ↓
5. Publish Dateien
   ↓
6. Setup NSIS
   ↓
7. Create Installer
   ↓
8. Create GitHub Release
   ↓
9. Upload Artefakte
```

### Ausgaben

Nach erfolgreichem Workflow:

**GitHub Release enthält:**
- `Elite-Dangerous-Addon-Launcher-Setup.exe` - NSIS Installer
- `LICENSE.txt` - Lizenzinformationen
- `CHANGELOG.md` - Version-Notizen

**Artifacts (72h verfügbar):**
- `installer/Elite-Dangerous-Addon-Launcher-Setup.exe`

## 🛠️ Lokales Bauen

### Requirements
- .NET 8.0 SDK
- NSIS 3.x (https://nsis.sourceforge.io/)

### Build-Befehle

```powershell
# Vollständiger Build mit Installer
.\build.ps1

# Nur Debug-Build
.\build.ps1 -BuildType Debug

# Ohne Installer
.\build.ps1 -NoInstaller

# Ohne Publish
.\build.ps1 -NoPublish
```

**Output:**
- `bin/Release/net8.0-windows/` - Kompilierte Dateien
- `publish/` - Veröffentlichte Binaries
- `Elite-Dangerous-Addon-Launcher-Setup.exe` - NSIS Installer

## 📁 Struktur

```
.github/
└── workflows/
    └── build-release.yml          # GitHub Actions Workflow

installer/
├── installer.nsi                  # NSIS Hauptskript
├── icon.ico                       # App-Icon (optional)
├── header.bmp                     # NSIS Header (optional)
├── wizard.bmp                     # NSIS Wizard (optional)
└── README.md                      # Anleitung

build.ps1                          # Lokales Build-Skript
release.ps1                        # Release-Management-Skript
INSTALLER_README.md                # Installer-Dokumentation
```

## 🔧 Konfiguration

### Versionsnummern aktualisieren

#### In `installer.nsi`
```nsi
WriteRegStr HKCU "..." "Version" "2.0.1"
```

#### In `.csproj` (optional)
```xml
<PropertyGroup>
  <Version>2.0.1</Version>
</PropertyGroup>
```

### NSIS Anpassung

Bearbeite `installer/installer.nsi`:

```nsi
; Seiten hinzufügen/entfernen
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "LICENSE.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_INSTFILES
```

```nsi
; Sprachen hinzufügen
!insertmacro MUI_LANGUAGE "German"
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_LANGUAGE "French"
```

## 📊 Workflow-Status

Überprüfe den Status unter:
- GitHub UI: Actions Tab
- Terminal: `gh run list --repo EDToolbox/Elite-Dangerous-Addon-Launcher-V2`

## ⚠️ Troubleshooting

### Workflow schlägt bei NSIS fehl
- NSIS muss lokal nicht installiert sein
- Die Action nutzt vorkonfiguriertes NSIS
- Prüfe `installer/installer.nsi` auf Syntax-Fehler

### GitHub Release wird nicht erstellt
- Prüfe dass `LICENSE.txt` im Root existiert
- Stelle sicher dass `CHANGELOG.md` vorhanden ist
- Validiere Git-Tag-Format (z.B. `v2.0.0`)

### Publish schlägt fehl
- Stelle sicher dass alle Abhängigkeiten in `packages` installiert sind
- Überprüfe dass `bin/` und `obj/` Verzeichnisse nicht-committet sind

## 🔐 Sicherheit

- ✅ Secrets nicht in Code hart-codiert
- ✅ `GITHUB_TOKEN` automatisch verfügbar
- ✅ Nur authentifizierte Pushes erlaubt

## 📝 Beispiel-Release

```bash
# Version auf 2.0.1 erhöhen
$> .\release.ps1 -Version "2.0.1" -Message "
- Fixed path traversal vulnerability
- Improved JSON deserialization security
- Updated dependencies to latest stable
"

# Dann automatisch:
# ✓ Git Commit und Tag erstellt
# ✓ Push zu origin/master und Tag gepusht
# ✓ GitHub Actions Workflow startet
# ✓ Installer wird gebaut
# ✓ GitHub Release erstellt
```

## 📚 Weitere Ressourcen

- [GitHub Actions Dokumentation](https://docs.github.com/actions)
- [NSIS Dokumentation](https://nsis.sourceforge.io/Docs/)
- [.NET Publishing Guide](https://docs.microsoft.com/dotnet/core/deploying/)

