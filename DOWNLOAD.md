# Downloading Auto Display Switcher

**Auto Display Switcher** is a lightweight, background Windows utility that automatically switches display projection between **Extend** (on AC power) and **PC screen only** (on battery power).

- **Supported Operating Systems:** 64-bit Windows 10 and Windows 11 (`win-x64`)
- **Architecture & Packaging:** Self-contained, compressed single-file .NET 10 `win-x64` executable (no separate .NET runtime installation required)

---

## Current Download Status

- **Current Builds Are Unsigned:** Every pre-built binary currently produced by CI is **unsigned**.
- **SignPath Foundation Status:** A free open-source code signing application has been **submitted** to the SignPath Foundation and **approval is pending**. No signing certificate or signing integration is active yet.
- **No Signed Public Release Available:** There is **no official signed public GitHub Release** yet. Only temporary, unsigned build artifacts from GitHub Actions are currently available.

---

## How to Download the Latest GitHub Actions Build Artifact

> **GitHub Account Required:** GitHub requires you to be **signed in** to a GitHub account to download workflow artifacts from the Actions tab.

1. Sign in to GitHub and open the project's [GitHub Actions](https://github.com/mahdi-014/Auto-Display-Switcher/actions) page.
2. Select the latest successful **Build and Release** workflow run on the `main` branch.
3. Scroll to the **Artifacts** section and download **`AutoDisplaySwitcher-win-x64-unsigned`**.
4. Extract the downloaded artifact wrapper ZIP file. Inside you will find:
   - `AutoDisplaySwitcher-win-x64-unsigned.zip`
   - `checksums-unsigned.sha256`
5. Verify the SHA256 checksums before extracting or running (see below).
6. Extract `AutoDisplaySwitcher-win-x64-unsigned.zip` into a permanent local folder (for example, `%LOCALAPPDATA%\AutoDisplaySwitcher`). Inside are the two release files:
   - `AutoDisplaySwitcher.exe`
   - `AutoDisplaySwitcher.bat`

*(Alternatively, developers with the .NET 10 SDK can build directly from source using the instructions in [README.md](README.md#g-build-from-source).)*

---

## Verify SHA256 Checksums

Open PowerShell in the folder containing `AutoDisplaySwitcher-win-x64-unsigned.zip` and `checksums-unsigned.sha256`, then compare hashes:

```powershell
Get-Content .\checksums-unsigned.sha256
Get-FileHash .\AutoDisplaySwitcher-win-x64-unsigned.zip -Algorithm SHA256
```

After extracting `AutoDisplaySwitcher-win-x64-unsigned.zip`, verify the extracted files against the same `checksums-unsigned.sha256` values:

```powershell
Get-FileHash .\AutoDisplaySwitcher.exe, .\AutoDisplaySwitcher.bat -Algorithm SHA256
```

---

## Windows Security Warning (Unsigned Builds)

Because `AutoDisplaySwitcher.exe` is currently **unsigned**, **Windows Smart App Control**, Windows Defender Application Control (WDAC), or strict Code Integrity policies may block the executable from running until SignPath Foundation code signing is approved and active.

- **Never disable Smart App Control, Microsoft Defender, WDAC, or Windows Code Integrity** to run an unsigned executable.
- If your system blocks the unsigned build, wait for an officially signed release after SignPath onboarding completes.

---

## Related Documentation

- [Project Overview & Setup Guide (README.md)](README.md)
- [Privacy Policy (PRIVACY.md)](PRIVACY.md)
- [Code Signing Policy (CODE-SIGNING-POLICY.md)](CODE-SIGNING-POLICY.md)
- [Security Policy (SECURITY.md)](SECURITY.md)
- [GitHub Repository](https://github.com/mahdi-014/Auto-Display-Switcher)
- [GitHub Actions](https://github.com/mahdi-014/Auto-Display-Switcher/actions)
