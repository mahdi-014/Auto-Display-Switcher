# Privacy Policy

Auto Display Switcher is a local Windows utility designed with a strict privacy-first architecture.

## Data Collection
- Auto Display Switcher does not collect, store, transmit, or share any personal data, device identifiers, or usage telemetry.
- The application requires no user accounts, registration, or internet connection.

## Network Access
- Auto Display Switcher performs zero network calls. It contains no embedded web servers, telemetry clients, or remote analytics endpoints.

## Local Operations
- The application monitors power status using the local Windows Win32 API (GetSystemPowerStatus).
- In production builds, diagnostic logging to disk is disabled by default.
- In debug builds, diagnostic events are recorded strictly on the local machine at %LOCALAPPDATA%\AutoDisplaySwitcher\diagnostic.log for troubleshooting.

## Windows Registry Usage
- The optional helper script (AutoDisplaySwitcher.bat) interacts solely with HKCU\Software\Microsoft\Windows\CurrentVersion\Run to register or unregister user startup. No administrator privileges are required.
