# Auto Display Switcher

A lightweight, zero-UI Windows background utility that automatically switches display projection modes between **Extend** (on AC power) and **PC screen only** (on battery power).

---

## A. Project Overview

**Auto Display Switcher** monitors your laptop's power source and switches Windows display projection automatically when you connect or disconnect your charger, eliminating the need to manually press `Win + P`.

- **Target Platform:** 64-bit Windows (`win-x64`)
- **Framework:** .NET 10 (`net10.0`, `WinExe` GUI subsystem with no visible window)
- **Key Features:**
  - Automatic AC/battery power monitoring via Win32 `GetSystemPowerStatus` (`kernel32.dll`)
  - 250 ms polling interval with a 400 ms debounce delay to prevent flickering on brief power transitions
  - Native projection switching through `%WINDIR%\System32\DisplaySwitch.exe` (`/extend` and `/internal`) with hidden process windows
  - Single-instance enforcement using the `Local\AutoDisplaySwitcher` named mutex
  - Zero GUI windows, no system tray icon, no Windows service, no database, and zero network requests or telemetry
  - Diagnostic disk logging disabled in `Release` builds (`#if DEBUG` only)
  - Self-contained, compressed single-file `win-x64` deployment

---

## B. Display Switching Behavior

In [`Program.cs`](Program.cs), `MonitorPower()` polls `NativeMethods.GetSystemPowerStatus` every **250 ms** and transitions display modes according to `ACLineStatus`:

| Power State (`ACLineStatus`) | Action Executed | Windows Display Mode |
| :--- | :--- | :--- |
| `1` (AC power connected) | `DisplaySwitch.exe /extend` | **Extend** |
| `0` (On battery power) | `DisplaySwitch.exe /internal` | **PC screen only** |
| `255` (Unknown status) | Ignored (re-polls after 250 ms) | Unchanged |

### Detection & Switching Limitations (Observed in Source)

1. **Debounce & Timeout:** When `ACLineStatus` changes, the app waits **400 ms** before invoking `DisplaySwitch.exe` and enforces a **3,000 ms** exit timeout.
2. **Power-Driven Only:** Switching is triggered strictly by AC vs. battery power state, not by display hot-plug events. *(While a helper class `DisplayDetector.CountActiveMonitors` exists in `Program.cs`, `MonitorPower()` does not call it.)* Plugging in AC power triggers `/extend` once even if no external monitor is attached.
3. **In-Memory State Tracking:** The app remembers `lastAppliedMode` (`"extend"` or `"internal"`). If you manually change projection using `Win + P` while staying on the same power source, Auto Display Switcher will not override your manual choice until the power source changes again.
4. **Desktop PCs:** Desktop machines without a battery continuously report AC power (`1`) and will not transition to battery mode.

---

## C. Requirements

### End-User Requirements (Published Self-Contained Build)
- **Operating System:** Windows 10 / Windows 11 (64-bit / x64)
- **Architecture:** `win-x64`
- **.NET Runtime:** **Not required separately** — [`AutoDisplaySwitcher.csproj`](AutoDisplaySwitcher.csproj) configures `<SelfContained>true</SelfContained>`, `<PublishSingleFile>true</PublishSingleFile>`, and `<EnableCompressionInSingleFile>true</EnableCompressionInSingleFile>`.
- **Hardware:** A laptop/portable Windows device supporting `GetSystemPowerStatus` AC/battery reporting and an external display when using **Extend** mode.

### Developer Build Requirements
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (`10.0.x`) on Windows
- Git

---

## D. Installation Guide — Normal Users

> **Important Notice Before Downloading**
> - **No signed public GitHub Release exists yet.**
> - `AutoDisplaySwitcher.exe` is **not** committed to the Git source tree.
> - The GitHub Actions CI workflow builds a **temporary unsigned artifact** (`AutoDisplaySwitcher-win-x64-unsigned`) stored on the **Actions** tab (subject to GitHub artifact retention limits; signing into a GitHub account is required to download Actions artifacts).

### Downloading the Latest Unsigned CI Build Artifact

1. Open the repository's [Actions tab](https://github.com/mahdi-014/Auto-Display-Switcher/actions) while signed in to GitHub.
2. Select the latest successful **Build and Release** workflow run on `main`.
3. Under **Artifacts**, download **`AutoDisplaySwitcher-win-x64-unsigned`**.
4. Extract the downloaded artifact zip to access:
   - `AutoDisplaySwitcher-win-x64-unsigned.zip`
   - `checksums-unsigned.sha256`
5. *(Recommended)* Verify the SHA256 checksums before extracting or running:
   ```powershell
   Get-Content .\checksums-unsigned.sha256
   Get-FileHash .\AutoDisplaySwitcher-win-x64-unsigned.zip -Algorithm SHA256
   ```
6. Extract `AutoDisplaySwitcher-win-x64-unsigned.zip` into a permanent folder (for example, `%LOCALAPPDATA%\AutoDisplaySwitcher`) so both files sit side by side:
   - `AutoDisplaySwitcher.exe`
   - `AutoDisplaySwitcher.bat`

### First-Time Start (Without Registering Startup)

From inside that folder, run:

```cmd
AutoDisplaySwitcher.bat /run
```

*(Or run `AutoDisplaySwitcher.exe` directly — subject to Windows Smart App Control checks on unsigned binaries; see [Section H](#h-troubleshooting) and [Section K](#k-security-and-release-status).)*

---

## E. Automatic Startup Installation

Automatic startup is **opt-in** and requires user action via [`Final-Release/AutoDisplaySwitcher.bat`](Final-Release/AutoDisplaySwitcher.bat). Running `AutoDisplaySwitcher.exe` by itself does **not** add any startup registration.

### How Registration Works

Running `AutoDisplaySwitcher.bat` with no arguments:
1. Confirms `AutoDisplaySwitcher.exe` exists in the same folder (`%~dp0`).
2. Registers a standard-user Registry entry (no administrator privileges required):
   - **Key:** `HKCU\Software\Microsoft\Windows\CurrentVersion\Run`
   - **Value Name:** `AutoDisplaySwitcher`
   - **Value Data:** `"<path-to-folder>\AutoDisplaySwitcher.exe"`
3. Immediately launches `AutoDisplaySwitcher.exe`.

### Enable Automatic Startup

Place `AutoDisplaySwitcher.exe` and `AutoDisplaySwitcher.bat` in their permanent folder and run:

```cmd
AutoDisplaySwitcher.bat
```

### Verify Automatic Startup Registration

Check the Registry value in PowerShell:

```powershell
(Get-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run").AutoDisplaySwitcher
```

You can also inspect **Task Manager → Startup apps** to confirm `AutoDisplaySwitcher` is not disabled.

### Disable Automatic Startup Safely

Run the built-in uninstall flag to remove the startup entry and stop the running process:

```cmd
AutoDisplaySwitcher.bat /uninstall
```

Or remove only the startup entry manually without deleting files:

```cmd
reg delete "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "AutoDisplaySwitcher" /f
```

### Verify Startup After Windows Sign-In

1. Restart Windows (`Start → Power → Restart`) and sign in normally.
2. Wait 30–60 seconds without manually launching the app.
3. Confirm `AutoDisplaySwitcher.exe` is running from your chosen folder:
   ```powershell
   Get-CimInstance Win32_Process -Filter "Name = 'AutoDisplaySwitcher.exe'" |
     Select-Object ProcessId, ExecutablePath, CreationDate
   ```

---

## F. Usage Guide

1. **Start the application:** Launch via `AutoDisplaySwitcher.bat` (install + start), `AutoDisplaySwitcher.bat /run` (run once), or automatically at Windows sign-in. No window or tray icon appears.
2. **Connect the charger (AC power):** Within ~1 second (`250 ms` poll + `400 ms` debounce), Auto Display Switcher invokes `DisplaySwitch.exe /extend`, extending your desktop across connected external monitors.
3. **Disconnect the charger (battery power):** Within ~1 second, Auto Display Switcher invokes `DisplaySwitch.exe /internal`, switching to **PC screen only**.
4. **Verify switching occurred:**
   - Observe the external display activate on AC power and turn off on battery power.
   - Press `Win + P` to confirm **Extend** (AC) or **PC screen only** (battery) is highlighted.
   - Confirm the process stays active in the background via `Get-Process AutoDisplaySwitcher`.

---

## G. Build From Source

Verified locally against `.NET SDK 10.0.404` and [`AutoDisplaySwitcher.csproj`](AutoDisplaySwitcher.csproj):

1. **Clone the repository:**
   ```powershell
   git clone https://github.com/mahdi-014/Auto-Display-Switcher.git
   cd Auto-Display-Switcher
   ```

2. **Restore dependencies:**
   ```powershell
   dotnet restore AutoDisplaySwitcher.csproj
   ```

3. **Build in Release configuration:**
   ```powershell
   dotnet build AutoDisplaySwitcher.csproj -c Release --no-restore
   ```

4. **Publish the self-contained single-file `win-x64` executable:**
   ```powershell
   dotnet publish AutoDisplaySwitcher.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -o .\publish-output
   ```

- **Expected Publish Output:** `.\publish-output\AutoDisplaySwitcher.exe` (single-file compressed self-contained executable) and `.\publish-output\AutoDisplaySwitcher.pdb`.
- Copy `Final-Release\AutoDisplaySwitcher.bat` next to `.\publish-output\AutoDisplaySwitcher.exe` if you want the startup helper script.
- *(Note: Diagnostic logging to `%LOCALAPPDATA%\AutoDisplaySwitcher\diagnostic.log` is compiled only in `Debug` builds via `#if DEBUG` and is disabled in `Release` builds.)*

---

## H. Troubleshooting

- **Application does not start:**
  - **Already running:** `Program.cs` enforces a single instance (`Local\AutoDisplaySwitcher`). Check `Get-Process AutoDisplaySwitcher`; a second launch exits immediately.
  - **Missing executable:** Ensure `AutoDisplaySwitcher.exe` is in the exact same directory as `AutoDisplaySwitcher.bat`.
- **Windows Smart App Control or Code Integrity blocks the unsigned build:**
  - Current binaries are **unsigned** because SignPath Foundation code signing is still pending. On Windows systems enforcing **Smart App Control**, WDAC, or strict Code Integrity, Windows may block unsigned executables (for example, Event IDs `3033` / `3077` / `3118` in `Microsoft-Windows-CodeIntegrity/Operational`).
  - **Never disable Smart App Control, Microsoft Defender, WDAC, or Windows Code Integrity.** Wait for an officially signed release once SignPath onboarding is complete.
- **Display mode does not change:**
  - Confirm `AutoDisplaySwitcher.exe` is running (`Get-Process AutoDisplaySwitcher`).
  - Remember `lastAppliedMode` only triggers on a power-state **transition** (AC ↔ Battery). If you pressed `Win + P` manually without unplugging/replugging power, unplug and reconnect power to trigger a state change.
  - Test whether Windows built-in `DisplaySwitch.exe /extend` and `DisplaySwitch.exe /internal` work directly on your GPU driver.
- **External monitor is not detected:**
  - Check physical cables/dock power and **Windows Settings → System → Display**. Auto Display Switcher relies entirely on Windows `DisplaySwitch.exe` for monitor detection and layout.
- **Automatic startup does not work after sign-in:**
  - Confirm `HKCU\Software\Microsoft\Windows\CurrentVersion\Run\AutoDisplaySwitcher` points to the current location of `AutoDisplaySwitcher.exe` (re-run `AutoDisplaySwitcher.bat` if you moved the folder).
  - Check **Task Manager → Startup apps** to ensure `AutoDisplaySwitcher` is not marked **Disabled**.
- **GitHub Actions build fails:**
  - Open the failed run under the [Actions tab](https://github.com/mahdi-014/Auto-Display-Switcher/actions) and inspect the `Build & Package Unsigned (win-x64)` job logs (`.NET 10` setup, `restore`, `build`, `publish`, or `Validate Published Files`).

---

## I. Uninstallation

1. **Remove startup registration and stop the running process:**
   ```cmd
   AutoDisplaySwitcher.bat /uninstall
   ```
   *(If you deleted `AutoDisplaySwitcher.bat` already, stop the process and remove the Registry key manually:)*
   ```cmd
   taskkill /IM AutoDisplaySwitcher.exe /F
   reg delete "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "AutoDisplaySwitcher" /f
   ```
2. **Verify startup registration is removed:**
   ```cmd
   reg query "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "AutoDisplaySwitcher"
   ```
   *(Expected: `ERROR: The system was unable to find the specified registry key or value.`)*
3. **Delete application files:** Remove the folder containing `AutoDisplaySwitcher.exe` and `AutoDisplaySwitcher.bat`. *(If you ever ran a local `Debug` build, you can also delete `%LOCALAPPDATA%\AutoDisplaySwitcher\diagnostic.log`.)*

---

## J. Project Structure

```text
AutoDisplaySwitcher/
├── .github/
│   └── workflows/
│       └── build-and-sign.yml     # CI build, SHA256 checksums, unsigned staging & release tag guard
├── Final-Release/
│   └── AutoDisplaySwitcher.bat    # Opt-in HKCU Run installer, /run, and /uninstall helper
│                                  # (AutoDisplaySwitcher.exe is untracked / excluded via .gitignore)
├── .gitignore                     # Ignores bin/, obj/, build outputs, *.exe, *.dll, *.pdb, *.zip, *.sha256
├── AutoDisplaySwitcher.csproj     # .NET 10 win-x64 self-contained single-file project metadata
├── Program.cs                     # Power polling, debounce, mutex & DisplaySwitch.exe runner
├── CHANGELOG.md                   # Release history
├── CODE-SIGNING-POLICY.md         # SignPath Foundation policy & verification guidelines
├── CONTRIBUTING.md                # Contribution guidelines
├── LICENSE                        # MIT License
├── PRIVACY.md                     # Zero-telemetry / local-only privacy statement
├── README.md                      # Project documentation
├── SECURITY.md                    # Vulnerability reporting & security guarantees
├── PHASE_2_PRE_PUBLICATION_VERIFICATION_REPORT.md
├── PHASE_3_GITHUB_PUBLICATION_PREPARATION_REPORT.md
├── PHASE_4A_EMAIL_PRIVACY_REMEDIATION_REPORT.md
└── PHASE_4B_FIRST_GITHUB_PUBLICATION_REPORT.md
```

---

## K. Security and Release Status

- **Current Build Status:** All builds produced from this repository are currently **unsigned**.
- **Code Signing Status:** Onboarding and approval for free open-source code signing through the **SignPath Foundation** are **pending**. No SignPath credentials or private signing keys are stored in the repository. See [CODE-SIGNING-POLICY.md](CODE-SIGNING-POLICY.md) and [SECURITY.md](SECURITY.md).
- **No Signed Release Available:** No signed production release is published yet.
- **Security Guidance:** Never disable Windows Smart App Control, Microsoft Defender, WDAC, or Code Integrity policies to bypass warnings for unsigned executables.

---

## L. License

This project is licensed under the **MIT License** (`Copyright (c) 2026 Mahdi`). See the [LICENSE](LICENSE) file for the full text.
