# Contributing to Auto Display Switcher

Thank you for your interest in contributing to Auto Display Switcher.

## Design Philosophy
- **Lightweight**: Keep executable size, memory footprint, and CPU overhead minimal.
- **Focused**: Single purpose utility for AC/Battery display switching. Do not add GUI windows, trays, background services, or network listeners.
- **Secure**: No elevation required; strict user-level operation.

## Development Workflow
1. Ensure the .NET 10 SDK is installed.
2. Build locally: dotnet build -c Release.
3. Publish single-file bundle: dotnet publish -c Release -r win-x64 --self-contained true.
4. Run validation checks before submitting pull requests.
