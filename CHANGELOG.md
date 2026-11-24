# Changelog

All notable changes to Auto Display Switcher are documented here.

## [1.0.0] - 2026-11-24

### Added
- Automated display profile switching between AC power (Extend) and Battery (PC screen only).
- Debounce timer (400ms) to prevent transient power fluctuations from causing display flicker.
- Single-instance enforcement using named mutex (Local\AutoDisplaySwitcher).
- Self-contained, single-file Windows x64 executable packaging.
- Two-file portable distribution (AutoDisplaySwitcher.exe and AutoDisplaySwitcher.bat).
- User-level startup registration and uninstallation via helper script.
- GitHub Actions CI/CD workflow with build validation, checksum generation, and SignPath signing integration readiness.
- Comprehensive open-source documentation suite.
