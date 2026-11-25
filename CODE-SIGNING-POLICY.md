# Code Signing Policy

## Current Status
**Application Submitted — Approval Pending** (SignPath Foundation).

An application for free open-source code signing through the **SignPath Foundation** has been submitted for **Auto Display Switcher**, and **approval is still pending**:
- No code-signing certificate or signing permission has been granted yet.
- Signing integration in [`.github/workflows/build-and-sign.yml`](.github/workflows/build-and-sign.yml) is **planned, not operational**.
- Current GitHub Actions build artifacts (`AutoDisplaySwitcher-win-x64-unsigned`) remain **unsigned**, and no signed public GitHub Release exists yet.

## Planned Signing Architecture (Post-Approval)
Once SignPath Foundation approval and onboarding are completed:
- **Trusted CI/CD Pipeline**: Builds will be compiled directly from public repository source code using GitHub Actions.
- **HSM Protection**: Private signing keys are managed within SignPath Foundation Hardware Security Modules; no private signing keys or signing secrets are stored in GitHub or this repository.
- **Traceability**: Signing requests will link directly to Git release tags and corresponding commit hashes.
- **Transparency**: Build artifacts will never be labeled as signed unless SignPath signing has executed and `Get-AuthenticodeSignature` confirms a valid signature.

## Verification & Windows Security
Users should always verify SHA256 checksums (`checksums-unsigned.sha256`) published alongside downloaded build artifacts and must **never** disable Windows Smart App Control, Microsoft Defender, WDAC, or Code Integrity policies to run an unsigned executable.
