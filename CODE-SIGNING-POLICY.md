# Code Signing Policy

## Current Status
**Planned / Pending Approval** via the SignPath Foundation.

Auto Display Switcher is prepared for free open-source code signing through the SignPath Foundation. Code signing has not yet been granted; initial release binaries are provided as **unsigned** with verifiable SHA256 hashes.

## Planned Signing Architecture
- **Trusted CI/CD Pipeline**: Builds are compiled directly from public repository source code using GitHub Actions.
- **HSM Protection**: Private signing keys are secured within SignPath Foundation Hardware Security Modules and are never exposed, exported, or stored in repository secrets.
- **Traceability**: Signing requests will link directly to release tags and corresponding Git commit hashes.
- **Transparency**: Artifacts will never be marked as signed until SignPath approval and integration are active.

## Verification
Users may independently verify downloaded binaries against published SHA256 checksums in each release.
