# Security Policy

## Supported Versions
| Version | Supported |
| :--- | :--- |
| 1.0.x | Yes |

## Reporting Security Issues
If you identify a security concern, vulnerability, or unexpected privilege behavior, please report it directly through GitHub Security Advisories or by contacting the project author (Mahdi).

## Security Guarantees
- **No Elevation Needed**: Runs entirely within standard user permissions.
- **Local Windows APIs**: Utilizes kernel32 power status queries and standard System32\DisplaySwitch.exe.
- **Integrity**: Release artifacts include SHA256 checksums to verify package authenticity.
- **Security Compliance**: Never attempts to bypass Windows Smart App Control, WDAC, or Code Integrity policies.
