# Auto Display Switcher

An automated, lightweight background utility for Windows laptops that switches display configurations between AC power and battery power.

## Overview

When connected to AC power (or a docking station), Auto Display Switcher automatically switches your Windows display mode to **Extend** (`/extend`). When unplugged and running on battery power, it switches to **PC screen only** (`/internal`) to conserve battery life.

## Features

- **Automatic Power Detection**: Polls Windows power status via Win32 `GetSystemPowerStatus` every 250 ms with a 400 ms transition debounce filter.
- **Native Display Switching**: Invokes Windows built-in `%WINDIR%\System32\DisplaySwitch.exe` (`/extend` and `/internal`) silently with a hidden window.
- **Single-Instance Protection**: Uses a named local mutex (`Local\AutoDisplaySwitcher`) so only one instance runs per session.
- **Minimal Background Design**: No GUI window, no system tray icon, no background service, no database, and zero network connections.
- **Self-Contained Single-File Deployment**: Targets .NET 10 (`net10.0`) for Windows x64 (`win-x64`) with no separate .NET runtime installation required.
- **Two-File Portable Release**: Distributed locally as `AutoDisplaySwitcher.exe` and `AutoDisplaySwitcher.bat`.

## Installation and Usage

1. Place both `AutoDisplaySwitcher.exe` and `AutoDisplaySwitcher.bat` in the same permanent folder (for example, `%LOCALAPPDATA%\AutoDisplaySwitcher`).
2. Install for user startup and start immediately:

```cmd
AutoDisplaySwitcher.bat
```

3. Run once without registering Windows startup:

```cmd
AutoDisplaySwitcher.bat /run
```

4. Uninstall (removes `HKCU\Software\Microsoft\Windows\CurrentVersion\Run` entry and stops the running process):

```cmd
AutoDisplaySwitcher.bat /uninstall
```

> **Note on Windows Smart App Control and Code Integrity**
>
> Current release executables are **unsigned**. On systems where **Windows Smart App Control**, Windows Defender Application Control (WDAC), or strict Code Integrity enforcement is enabled, Windows may block `AutoDisplaySwitcher.exe` from running until code signing is enabled.
>
> - **Do not** disable Smart App Control, WDAC, Windows Defender, or Code Integrity policies.
> - Always verify published SHA256 checksums before running downloaded binaries.
> - If Smart App Control blocks the unsigned pre-built binary, you can inspect the source code and wait for future SignPath-signed releases.

## Building from Source

- **Prerequisites**: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) on Windows.
- **Restore and Build (Release)**:

```powershell
dotnet restore AutoDisplaySwitcher.csproj
dotnet build AutoDisplaySwitcher.csproj -c Release --no-restore
```

- **Publish Single-File Self-Contained Executable (`win-x64`)**:

```powershell
dotnet publish AutoDisplaySwitcher.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -o publish-output
```

*(Diagnostic logging in `Program.cs` is compiled only in `Debug` builds via `#if DEBUG` and is disabled in `Release` builds.)*

## Code Signing Status

Free open-source code signing is **planned and pending approval** through the **SignPath Foundation**. No SignPath approval or digital signature exists yet; current builds and artifacts are explicitly **unsigned** and accompanied by SHA256 checksums. See [CODE-SIGNING-POLICY.md](CODE-SIGNING-POLICY.md) for details.

## Documentation

- [Code Signing Policy](CODE-SIGNING-POLICY.md)
- [Privacy Policy](PRIVACY.md)
- [Security Policy](SECURITY.md)
- [Contributing Guidelines](CONTRIBUTING.md)
- [Changelog](CHANGELOG.md)
- [License (MIT)](LICENSE)
