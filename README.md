# Auto Display Switcher

A lightweight background Windows utility that automatically switches display modes based on laptop power status.

## Behavior

- **AC power connected:** Switches Windows display projection to **Extend** (`DisplaySwitch.exe /extend`).
- **Battery power:** Switches Windows display projection to **PC screen only** (`DisplaySwitch.exe /internal`).

The application runs silently in the background with no GUI window or tray icon, and uses a single-instance mutex so only one copy runs at a time.

## Requirements

- **OS:** Windows 10 or Windows 11 (64-bit / `win-x64`)
- **Runtime:** No separate .NET runtime is required for the published self-contained executable.

## Download & Run (GitHub Actions Artifact)

1. Sign in to GitHub and open the repository's [Actions](https://github.com/mahdi-014/Auto-Display-Switcher/actions) tab.
2. Open the latest successful **Build** run on `main`.
3. Download the **`AutoDisplaySwitcher-win-x64`** artifact.
4. Extract the ZIP into a permanent folder (for example, `%LOCALAPPDATA%\AutoDisplaySwitcher`). Inside you will find:
   - `AutoDisplaySwitcher.exe`
   - `AutoDisplaySwitcher.bat`
5. To run once without adding startup registration:
   ```cmd
   AutoDisplaySwitcher.bat /run
   ```

## Automatic Startup & Uninstall

Keep `AutoDisplaySwitcher.exe` and `AutoDisplaySwitcher.bat` in the same folder:

- **Install & start at Windows sign-in:**
  ```cmd
  AutoDisplaySwitcher.bat
  ```
  This adds `AutoDisplaySwitcher` under `HKCU\Software\Microsoft\Windows\CurrentVersion\Run` pointing to `AutoDisplaySwitcher.exe` and starts the application immediately.

- **Uninstall startup & stop the app:**
  ```cmd
  AutoDisplaySwitcher.bat /uninstall
  ```
  This removes the `HKCU\Software\Microsoft\Windows\CurrentVersion\Run` entry and stops `AutoDisplaySwitcher.exe`.

## Build From Source

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) on Windows:

```powershell
git clone https://github.com/mahdi-014/Auto-Display-Switcher.git
cd Auto-Display-Switcher
dotnet restore AutoDisplaySwitcher.csproj
dotnet build AutoDisplaySwitcher.csproj -c Release --no-restore
dotnet publish AutoDisplaySwitcher.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -o .\publish-output
```

The single-file executable is created at `.\publish-output\AutoDisplaySwitcher.exe`. Copy `Final-Release\AutoDisplaySwitcher.bat` alongside it if you want the startup helper script.

## Security Note

Current builds of `AutoDisplaySwitcher.exe` are **unsigned**. Depending on your system configuration, Windows Smart App Control or Code Integrity may warn about or block unsigned executables. Do not disable Windows security protections such as Smart App Control or Microsoft Defender.

## License

MIT License — see [LICENSE](LICENSE).
