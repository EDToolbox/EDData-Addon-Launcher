# Build und Installer-Generierung für Elite Dangerous Addon Launcher V2

## Voraussetzungen

- .NET 8.0 SDK
- NSIS 3.x (https://nsis.sourceforge.io/)
- Visual Studio oder Build Tools

## Lokales Bauen

### 1. Projekt kompilieren
```powershell
dotnet build --configuration Release
```

### 2. Projekt veröffentlichen
```powershell
dotnet publish --configuration Release --output ./publish
```

### 3. Installer erzeugen (mit NSIS)

#### Windows (PowerShell)
```powershell
# NSIS-Installer ausführen
& "C:\Program Files (x86)\NSIS\makensis.exe" installer/installer.nsi
```

#### Alternativ: Mit GitHub Actions
Der Installer wird automatisch erzeugt bei:
- Push zu einem Git-Tag (z.B. `v2.0.0`)
- Manueller Trigger über "Run workflow"

## Release-Prozess

### Automatische Release (empfohlen)
```bash
git tag v2.0.0
git push origin v2.0.0
```

Die GitHub Actions Pipeline wird automatisch:
1. Das Projekt kompilieren
2. NSIS Installer erzeugen
3. GitHub Release mit Installer und Changelog erstellen

## Installer-Features

### ✅ Implementiert
- **Lizenzanzeige** - Zeigt LICENSE.txt während der Installation an
- **Installationspfad wählbar** - Benutzer kann Zielverzeichnis festlegen
- **Deinstallation** - Vollständige Entfernung aller Dateien
- **Startmenü-Einträge** - Verknüpfungen im Startmenü
- **Desktop-Shortcut** - Optional auf Desktop
- **Registry-Einträge** - Für System-Integration
- **Admin-Rechte** - Automatische Erhöhung von Rechten
- **Doppelinstallation-Prävention** - Warnt bei Neuinstallation über bestehender Installation
- **Deinstallations-Bestätigung** - Sicherheitsfrage beim Deinstallieren

## NSIS-Skript-Struktur

```
installer/
├── installer.nsi          # Hauptinstaller-Skript
├── icon.ico              # Anwendungs-Icon
├── header.bmp            # NSIS Header-Bild (150x57px)
└── wizard.bmp            # NSIS Wizard-Bild (164x314px)
```

## Bilder für Installer (optional)

Um den Installer zu personalisieren, können Sie Bilder hinzufügen:

1. **icon.ico** - Anwendungs-Icon (256x256px recommended)
2. **header.bmp** - Header-Bild (150x57px)
3. **wizard.bmp** - Wizard/Willkommens-Bild (164x314px)

Platzieren Sie diese im `installer/`-Verzeichnis.

## Versions-Update

Aktualisieren Sie die Version in:

1. **installer.nsi**
   ```
   WriteRegStr HKCU "..." "Version" "2.0.0"
   ```

2. **.csproj** (wenn vorhanden)
   ```xml
   <PropertyGroup>
     <Version>2.0.0</Version>
   </PropertyGroup>
   ```

## Troubleshooting

### NSIS-Fehler: "File not found"
- Stelle sicher, dass `dotnet publish` erfolgreich lief
- Der `publish/` Ordner muss alle Dateien enthalten

### GitHub Actions schlägt fehl
- Prüfe, dass `LICENSE.txt` im Root vorhanden ist
- Kontrolliere dass Git-Tag korrekt formatiert ist (z.B. `v2.0.0`)

### Installer startet nicht nach Installation
- Kontrolliere dass die Executable-Pfade in installer.nsi korrekt sind
- Stelle sicher, dass alle DLL-Dependencies im publish-Ordner sind

## Sicherheit

Der Installer:
- ✅ Fordert Admin-Rechte an (nur wenn nötig)
- ✅ Speichert Installationspfad in separatem Registry-Key pro Benutzer
- ✅ Validiert bestehende Installation
- ✅ Erstellt sichere Deinstallation

