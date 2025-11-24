# NSIS Installer-Konfigurationsdatei

Alle erforderlichen Dateien für den Installer sind im `installer/` Verzeichnis:

## Dateien

- **installer.nsi** - Hauptinstaller-Skript (NSIS)
- **icon.ico** - Anwendungs-Icon (bitte hinzufügen: 256x256px)
- **header.bmp** - NSIS Header-Bild (bitte hinzufügen: 150x57px, BMP-Format)
- **wizard.bmp** - Willkommens-Bild (bitte hinzufügen: 164x314px, BMP-Format)

## Icon erstellen

### Mit ImageMagick
```bash
convert -background none Elite-Dangerous-Addon-Launcher-V2.png -define icon:auto-resize=256,128,64,48,32,16 icon.ico
```

### Mit Online-Tools
- https://icoconvert.com/
- https://convertio.co/

## BMP-Bilder erstellen

Die Bilder können mit GIMP oder Paint erstellt werden:

1. **header.bmp** (150x57px)
   - Header-Design mit App-Logo und Branding

2. **wizard.bmp** (164x314px)
   - Willkommens-/Wizard-Design mit App-Icon

Farbtiefe: 24-bit oder 32-bit BMP

## Automatische Generierung

Wenn Icon und Bilder fehlen, verwendet der Installer Standard-NSIS-Grafiken.

Die Icon- und Bild-Verweise sind optional in `installer.nsi` mit `!define` definiert.
