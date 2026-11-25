# Phase 2 — Pre-Publication Verification Report

**Project:** Auto Display Switcher  
**Project Directory:** `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher`  
**Intended Remote (Future Use Only):** `https://github.com/mahdi-014/Auto-Display-Switcher.git`  
**Audit Date:** 2026-11-25  
**Final Decision:** **B. READY WITH LIMITATIONS**

---

## 1. Executive Summary

An independent, local-only pre-publication verification of **Auto Display Switcher** was completed without modifying source files, changing Windows security policies, executing unsigned binaries, adding Git remotes, creating commits, or pushing to GitHub.

- **Git & Source Hygiene (`PASS`)**: Local branch `main` at commit `2f5bb2031187e534c59dd82e3b5b9738261ac14b` contains 12 clean text/source/documentation/workflow files, zero secrets, and zero binary blobs (`.exe`/`.dll`/`.pdb`). `Final-Release\AutoDisplaySwitcher.exe` remains physically present on disk, untracked, and untouched.
- **Backups & Recoverability (`PASS`)**: All three backup artifacts exist and are intact. The pre-remediation Git bundle passed `git bundle verify`, and a temporary clone/checkout recovery test (`git clone -b main` + `git fsck --full`) succeeded cleanly.
- **.NET Build & Publish (`PASS`)**: `.NET SDK 10.0.404` restored, built (`0` warnings, `0` errors), and published a `win-x64` self-contained single-file executable to a temporary verification folder without errors.
- **Workflow Syntax (`NOT VERIFIED`) & Release Safety (`PASS`)**: No local YAML parser (`actionlint`, `yamllint`, `PyYAML`, `js-yaml`, `yq`, PowerShell YAML module) is installed, so YAML syntax is **NOT VERIFIED** by an automated parser. Manual structural and safety review confirms least-privilege permissions, explicit unsigned labeling, checksum coverage, and no fake signing steps, with one non-unsafe operational caveat around `workflow_dispatch` on branch refs without a tag.
- **Signing & Windows Security (`PASS` / Unsigned Limitation)**: Windows Smart App Control (`On`), VBS/HVCI (`CodeIntegrityPolicyEnforcementStatus = 2`, `UsermodeCodeIntegrityPolicyEnforcementStatus = 2`), and Microsoft Defender real-time protection (`True`) are actively enforced. `Final-Release\AutoDisplaySwitcher.exe` is confirmed `NotSigned`. GitHub source publication is ready (with the caveats below), whereas **signed executable distribution is NOT ready** until SignPath Foundation onboarding and signing integration are completed.

| Verification Area | Status | Notes |
| :--- | :--- | :--- |
| Baseline & Git State | **PASS** | `main` at `2f5bb2031187e534c59dd82e3b5b9738261ac14b`, clean before report creation, no remotes |
| Backup & Bundle Recoverability | **PASS** | `git bundle verify` and temporary `git clone -b main` + `git fsck --full` passed |
| Git History & Tracked Files | **PASS** | 12 tracked text files; zero `.exe`/`.dll`/`.pdb`/secrets in reachable history |
| Workflow YAML Parser Validation | **NOT VERIFIED** | No local YAML validator/parser installed (`actionlint`/`yamllint`/`PyYAML` absent) |
| GitHub Actions Structure & Safety | **PASS** | Truthful unsigned pipeline, least privilege, no unsafe signed-release path |
| .NET 10 Restore / Build / Publish | **PASS** | `0` warnings, `0` errors; single-file self-contained `win-x64` publish verified |
| Final-Release Integrity | **PASS** | Exact 2 files (`AutoDisplaySwitcher.exe`, `AutoDisplaySwitcher.bat`), hashes match baseline |
| Windows Security Compatibility | **PASS** | Active SAC/HVCI/Defender verified read-only; EXE verified `NotSigned` |
| Documentation Consistency | **PASS** | Consistent across all 7 docs; minor non-blocking notes recorded |

---

## 2. Baseline Commit and Git Status

Verified prior to creating this report file:

- **Project Path Exists**: `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher` -> **PASS**
- **Current Branch**: `main` -> **PASS**
- **HEAD Commit**: `2f5bb2031187e534c59dd82e3b5b9738261ac14b` (matches expected `2f5bb2031187e534c59dd82e3b5b9738261ac14b`) -> **PASS**
- **Working Tree Status (baseline)**: Clean (`## main`, zero modified/untracked files prior to generating `PHASE_2_PRE_PUBLICATION_VERIFICATION_REPORT.md`) -> **PASS**
- **Configured Remotes**: None (`git remote -v` returned empty output) -> **PASS**

### Baseline `Final-Release` Measurements
- `Final-Release\AutoDisplaySwitcher.exe`: `37,603,734` bytes, SHA256 `3D359426E2F4D835B3B5EE8F551834CF58B84DCB7773B35A12E49460C9029816` (matches expected baseline) -> **PASS**
- `Final-Release\AutoDisplaySwitcher.bat`: `960` bytes, SHA256 `B8437182525BA4E45812D21EF5756D356380D2E870CD9FF410A5EC8164C8C7A9` (matches expected baseline) -> **PASS**

---

## 3. Backup and Bundle Verification

| Artifact | Path | Result | Details |
| :--- | :--- | :--- | :--- |
| Full Backup (Post-Commit) | `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-Backup-20261125-130712` | **PASS** | Exists; contains `.git` directory (`True`), `bin/`, `obj/`, `Final-Release/`, `Program.cs.pre-release.bak`, and all source/doc files. |
| Pre-Remediation Git Bundle | `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-pre-remediation-20261125-130712.bundle` | **PASS** | Exists; `git bundle verify` exited `0` (`... is okay`, contains complete history for `1981ac771f84a36ecb901429508bcbef3404a08c refs/heads/main`). |
| Prior Baseline Backup | `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-Backup-20261124-105151` | **PASS** | Exists; preserved untouched (does not contain `.git`, as it was created before `git init`). |

### File Comparison: Current Project vs `AutoDisplaySwitcher-Backup-20261125-130712`
- **Identical (`Match=True`)**: `Program.cs` (`0A9207C8E8F0...`), `AutoDisplaySwitcher.csproj` (`157CB6FF478D...`), `CHANGELOG.md` (`B8E617F5C365...`), `CODE-SIGNING-POLICY.md` (`DB748BA2CE34...`), `CONTRIBUTING.md` (`9B6522AE8424...`), `PRIVACY.md` (`C312E8EDA051...`), `SECURITY.md` (`6D2673653A65...`), `Final-Release\AutoDisplaySwitcher.bat` (`B8437182525B...`), `Final-Release\AutoDisplaySwitcher.exe` (`3D359426E2F4...`).
- **Expected Differences (`Match=False`, from Phase 1 remediation)**: `.gitignore` (`EE84699F1A34...` vs `271844966EB3...`), `LICENSE` (`829F00747D0C...` vs `0D7F2590684A...`), `README.md` (`4364764CD30B...` vs `23D0E90FB854...`), `.github\workflows\build-and-sign.yml` (`C1154C13EFB1...` vs `DD4700ABB52D...`).

### Temporary Bundle Recovery Test
- Executed `git clone -b main "D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-pre-remediation-20261125-130712.bundle" "D:\AutoDisplaySwitcher-Dev\temp-bundle-recovery-verify"` (`ExitCode: 0`).
- *(Note: An initial `git clone` without `-b main` failed to auto-checkout because the bundle advertises `refs/heads/main` without a `HEAD` symref; specifying `-b main` resolved checkout cleanly.)*
- Verified recovered commit `1981ac7` (`13 files changed, 623 insertions(+)`), verified repository object integrity via `git fsck --full` (`ExitCode: 0`), and deleted the temporary verification directory (`Exists? False`).
- **What remains unverified**: Restoring the workspace directly over the live project directory (explicitly avoided per safety rules).

---

## 4. Git History and Tracked-File Audit

### Command Evidence
- `git log --oneline --decorate --all`:
  `2f5bb20 (HEAD -> main) Initial release: Auto Display Switcher v1.0.0 source and CI/CD`
- `git show --stat --oneline HEAD`: `12 files changed, 698 insertions(+)`
- `git ls-files` (12 tracked files):
  1. `.github/workflows/build-and-sign.yml`
  2. `.gitignore`
  3. `AutoDisplaySwitcher.csproj`
  4. `CHANGELOG.md`
  5. `CODE-SIGNING-POLICY.md`
  6. `CONTRIBUTING.md`
  7. `Final-Release/AutoDisplaySwitcher.bat`
  8. `LICENSE`
  9. `PRIVACY.md`
  10. `Program.cs`
  11. `README.md`
  12. `SECURITY.md`
- `git rev-list --objects --all`: Returns only commit `2f5bb203...`, root tree `b42961ab...`, subdirectories (`.github`, `.github/workflows`, `Final-Release`), and the 12 text blobs listed above.
- `git log --all --stat -- "*.exe" "*.dll" "*.pdb"`: Empty output (**PASS** — no executables, DLLs, or PDBs in Git history).
- `git grep -n -i -E "(ghp_|github_pat_|AKIA|-----BEGIN|api_key|password|secret|token)"`: Matched only non-secret documentation/comment lines referencing GitHub Environment secret names (`SIGNPATH_API_TOKEN`) and HSM key policy in `.github/workflows/build-and-sign.yml` lines 107–108 and `CODE-SIGNING-POLICY.md` line 10 (**PASS** — no secrets committed).
- `.gitignore`: Ignores `[Bb]in/`, `[Oo]bj/`, `publish-temp/`, `publish-output/`, `publish-release/`, `artifacts/`, `release-files/`, `*.exe`, `*.dll`, `*.pdb`, `*.zip`, `*.sha256`, IDE files, `*.bak`, `*.bundle`, and `diagnostic.log`. `Final-Release\AutoDisplaySwitcher.exe` and `Program.cs.pre-release.bak` remain present on disk and untracked/ignored.

---

## 5. Workflow / YAML Validation Results

Inspected: `.github\workflows\build-and-sign.yml` (158 lines).

### A. YAML Syntax Check (Ordered Validator Search)
1. `actionlint`: Not installed (`Get-Command` empty).
2. `yamllint`: Not installed (`Get-Command` empty).
3. Python `PyYAML`: Not installed (`ImportError` / `Not installed`).
4. Other already-installed YAML parsers (`yq`, `ruby`, PowerShell `*yaml*` modules, Node `js-yaml`): None installed.

**YAML Syntax Result:** **NOT VERIFIED** (no automated YAML validator or parser available locally; no packages were installed per project rules). Manual review observed consistent 2-space indentation, valid mapping/sequence nesting, and properly quoted strings/block scalars.

### B. GitHub Actions Structure Review (`PASS`)
- **Name & Triggers**: `Build and Release`; triggers on `push` (`branches: [ main ]`, `tags: [ 'v*' ]`), `pull_request` (`branches: [ main ]`), and `workflow_dispatch`.
- **Runner & .NET Setup**: `windows-latest`, `actions/checkout@v4`, `actions/setup-dotnet@v4` with `dotnet-version: '10.0.x'`.
- **Build & Publish**: Restores `AutoDisplaySwitcher.csproj`, builds `-c Release --no-restore`, publishes `-c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -o publish-output`.
- **Staging, Checksums & Artifact Paths**: Validates `publish-output\AutoDisplaySwitcher.exe` (`>= 10MB`) and `Final-Release\AutoDisplaySwitcher.bat`, copies only those two files into `stage-unsigned\`, compresses `artifacts\AutoDisplaySwitcher-win-x64-unsigned.zip`, and hashes `stage-unsigned\AutoDisplaySwitcher.exe`, `stage-unsigned\AutoDisplaySwitcher.bat`, and `artifacts\AutoDisplaySwitcher-win-x64-unsigned.zip` into `artifacts\checksums-unsigned.sha256`. Uploads `path: artifacts/` as `AutoDisplaySwitcher-win-x64-unsigned` and downloads into `release-files`, where `AutoDisplaySwitcher-win-x64-unsigned.zip` and `checksums-unsigned.sha256` match lines 139 and 149–150 directly.
- **Action Pinning**: Uses major version tags (`@v4`, `@v2`), which are standard; pinning to full 40-character commit SHAs is recommended for stronger supply-chain hardening prior to SignPath onboarding.

---

## 6. GitHub Actions Security Review

- **Least-Privilege Permissions (`PASS`)**: Workflow top-level `permissions: contents: read`; `build` job `permissions: contents: read`; `release` job `permissions: contents: write`.
- **No Fake Signing / No Misleading Signed Labels (`PASS`)**: Truthful unsigned build-only pipeline. No placeholder signing step exists; post-approval integration points are documented solely in comments (lines 98–112). Explicitly rejects unexpected `Valid` signature state in build-only output (lines 68–72), labels archive/checksums `-unsigned`, sets `prerelease: true`, and states `**Artifact Signing Status: UNSIGNED**` in release body.
- **Approval Gate (`PASS` with external prerequisite)**: `release` job specifies `environment: release-approval`, which requires manual configuration of Environment protection rules (required reviewers) in GitHub repository settings.
- **Non-Unsafe Operational Caveat (`workflow_dispatch` on `main`)**: Line 123 allows `github.event_name == 'workflow_dispatch'` to run the `release` job, while line 146 (`softprops/action-gh-release@v2`) omits `tag_name`. Triggering `workflow_dispatch` on branch `main` (instead of a `v*` tag ref) would fail safely at `Create Unsigned GitHub Release` because GitHub Releases require a tag; it does **not** permit an unsafe or misleading release.

---

## 7. .NET Restore / Build / Publish Results

Inspected `AutoDisplaySwitcher.csproj`, `Program.cs`, and `README.md`.

| Step | Command / Evidence | Exit Code | Result |
| :--- | :--- | :--- | :--- |
| Environment | `dotnet --info`: .NET SDK `10.0.404`, Host `10.0.15`, OS `Windows 10.0.26300`, RID `win-x64` | `0` | **PASS** |
| Restore | `dotnet restore AutoDisplaySwitcher.csproj` | `0` | **PASS** |
| Build | `dotnet build AutoDisplaySwitcher.csproj -c Release --no-restore` (`0 Warning(s)`, `0 Error(s)`, `1.08s`) | `0` | **PASS** |
| Publish (Temp) | `dotnet publish AutoDisplaySwitcher.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -o .\phase2-verification-output` | `0` | **PASS** |

### Temporary Publish Output Verification (`.\phase2-verification-output`)
- **Output Files**: `AutoDisplaySwitcher.exe` (`37,603,827` bytes) and `AutoDisplaySwitcher.pdb` (`12,800` bytes).
- **Authenticode Status**: `NotSigned`, `SignatureType: None`, `IsOSBinary: False`.
- **Metadata**: `ProductName = Auto Display Switcher`, `ProductVersion = 1.0.0+2f5bb2031187e534c59dd82e3b5b9738261ac14b`, `FileVersion = 1.0.0.0`, `CompanyName = Mahdi`, `FileDescription = AutoDisplaySwitcher`, `LegalCopyright = Copyright © 2026 Mahdi`.
- **Comparison vs Preserved `Final-Release\AutoDisplaySwitcher.exe`**:
  - Temporary EXE SHA256: `EFC03102FC49826569901D5440C166BFEFE832F91453515DCED003548C34811D` (`37,603,827` bytes)
  - Preserved Final-Release EXE SHA256: `3D359426E2F4D835B3B5EE8F551834CF58B84DCB7773B35A12E49460C9029816` (`37,603,734` bytes)
  - **Explanation of difference**: Expected. `Final-Release\AutoDisplaySwitcher.exe` was published on `2026-11-24` before `git init` (`ProductVersion = 1.0.0`), whereas the temporary build ran inside the initialized Git repo so .NET SourceLink appended `+2f5bb2031187e534c59dd82e3b5b9738261ac14b` (`InformationalVersion`) alongside commit/build metadata. Note that `AutoDisplaySwitcher.pdb` is also produced alongside the EXE, and `.github\workflows\build-and-sign.yml` explicitly stages only the `.exe` and `.bat`.
- **Cleanup**: `.\phase2-verification-output` was deleted after inspection (`Still exists? False`). Neither EXE was executed.

---

## 8. Final-Release Integrity and SHA256 Hashes

- **File Count**: Exactly `2` files in `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher\Final-Release` (**PASS**).
- **`Final-Release\AutoDisplaySwitcher.exe`**:
  - Size: `37,603,734` bytes
  - SHA256: `3D359426E2F4D835B3B5EE8F551834CF58B84DCB7773B35A12E49460C9029816` (matches baseline)
  - Authenticode: `NotSigned`, `SignatureType = None`, `IsOSBinary = False`
- **`Final-Release\AutoDisplaySwitcher.bat`**:
  - Size: `960` bytes
  - SHA256: `B8437182525BA4E45812D21EF5756D356380D2E870CD9FF410A5EC8164C8C7A9` (matches baseline)
  - Behavior: Verified default HKCU Run key install + launch, `/run`, and `/uninstall`.

---

## 9. Smart App Control and Authenticode Findings

All checks were strictly read-only:

| Security Control / Query | Observed Output | Status |
| :--- | :--- | :--- |
| `Get-MpComputerStatus` | `RealTimeProtectionEnabled: True`, `BehaviorMonitorEnabled: True`, `OnAccessProtectionEnabled: True`, `SmartAppControlState: On` | **PASS** |
| `HKLM:\SYSTEM\CurrentControlSet\Control\CI\Policy` | `VerifiedAndReputablePolicyState: 1`, `SAC_EnforcementReason: 1`, `SAC_PreviousState: 2` | **PASS** |
| `Win32_DeviceGuard` | `VirtualizationBasedSecurityStatus: 2`, `CodeIntegrityPolicyEnforcementStatus: 2`, `UsermodeCodeIntegrityPolicyEnforcementStatus: 2`, `SecurityServicesConfigured/Running: {2}` | **PASS** |
| `CiTool.exe -lp` | `An error occurred: 0x80070005` (`ExitCode: -2147024891`) | **BLOCKED** (Access Denied in standard non-elevated shell; does not imply policies are off, as `Win32_DeviceGuard` and Registry confirm active enforcement) |
| `Get-AuthenticodeSignature .\Final-Release\AutoDisplaySwitcher.exe` | `Status: NotSigned`, `SignatureType: None`, `IsOSBinary: False` | **PASS** (Confirmed unsigned; Smart App Control / Code Integrity will block unsigned execution on enforced hosts until signed) |

---

## 10. Documentation Audit

Reviewed all 7 documentation files for consistency:

| File | Result | Notes |
| :--- | :--- | :--- |
| `README.md` | **PASS** | Accurate description, Windows x64 `.NET 10` requirements, fenced `cmd`/`powershell` blocks matching `AutoDisplaySwitcher.bat` and `AutoDisplaySwitcher.csproj`, clear Smart App Control warning, and accurate pending-approval signing notice. |
| `LICENSE` | **PASS** | Complete standard 21-line MIT License (`Copyright (c) 2026 Mahdi`). |
| `CHANGELOG.md` | **PASS** | Documents `1.0.0` release features and workflow/docs preparation. |
| `SECURITY.md` | **PASS** | Accurate permission/API guarantees (`1.0.x`). Minor note: line 9 directs reports to GitHub Security Advisories or Mahdi without an email address, so private Security Advisories should be enabled on GitHub after creation. |
| `PRIVACY.md` | **PASS** | Accurate zero-telemetry, zero-network, production logging disabled, debug-only local log path, and HKCU Run key disclosure. |
| `CONTRIBUTING.md` | **PASS** | Consistent design philosophy and `.NET 10` build/publish guidance (`dotnet publish -c Release -r win-x64 --self-contained true` uses `.csproj` defaults for single-file). |
| `CODE-SIGNING-POLICY.md` | **PASS** | Explicitly marks signing **Planned / Pending Approval**, outlines HSM key safety and SHA256 verification. |

### Proposed GitHub Repository Metadata (For Future Creation)
- **Description:** `Lightweight Windows utility that automatically switches laptop displays between Extend (AC power) and PC screen only (battery).`
- **Topics:** `windows`, `dotnet`, `csharp`, `display-switcher`, `power-management`, `laptop`, `windows-utility`, `open-source`

---

## 11. Issues Found

1. **YAML parser validation unavailable locally (`NOT VERIFIED`)**: No YAML parser or `actionlint` binary is installed locally. First validation by GitHub Actions will verify workflow YAML syntax.
2. **`workflow_dispatch` on branch ref in `release` job (Minor / Non-Unsafe)**: In `.github\workflows\build-and-sign.yml` lines 123 & 146, triggering `workflow_dispatch` on branch `main` (without a `v*` tag ref or `tag_name` input) will fail cleanly at `softprops/action-gh-release@v2` because GitHub Releases require a tag. Does **not** create an unsafe release.
3. **Action pinning via major tags vs commit SHAs (Advisory)**: `.github\workflows\build-and-sign.yml` uses `@v4` / `@v2` tags rather than immutable commit SHAs. Hardening to commit SHAs is recommended before SignPath signing approval.
4. **Minor formatting & metadata notes (Non-blocking)**:
   - `AutoDisplaySwitcher.csproj` line 32 has 6-space indentation instead of 4-space.
   - `SECURITY.md` line 9 relies on GitHub Security Advisories (no fallback email listed).
   - `Program.cs` preserves unreferenced helper class `DisplayDetector` (lines 165–211) from original source; switching behavior is unaffected.
5. **Audit report file in working tree**: Saving `PHASE_2_PRE_PUBLICATION_VERIFICATION_REPORT.md` in the repository root creates an untracked file in `git status`. Decide before pushing whether to commit it or move/exclude it.

---

## 12. Corrections Made

**None.** No source files, workflows, documentation files, Git commits, backups, release binaries, or Windows security settings were modified during this verification phase.

---

## 13. Remaining Blockers

- **GitHub Source Publication Blockers**: **None** (except deciding how to handle this newly created untracked report file prior to `git push`, and verifying workflow YAML on GitHub Actions).
- **Signed Production Binary Distribution Blockers**:
  1. Public GitHub repository does not exist yet (`mahdi-014/Auto-Display-Switcher` not yet created/pushed).
  2. SignPath Foundation application and approval are pending.
  3. GitHub Environment (`release-approval`) protection rules and post-approval SignPath workflow steps/secrets (`SIGNPATH_API_TOKEN` and org/project/policy variables) are not yet configured.
  4. Unsigned `AutoDisplaySwitcher.exe` cannot execute under active Smart App Control / Windows Code Integrity enforcement until properly code-signed.

---

## 14. GitHub Publication Readiness

- **Source Repository Publication Readiness:** **READY WITH LIMITATIONS** — Safe to publish as open-source `.NET 10` source code + CI/CD workflow.
- **Signed Release Distribution Readiness:** **NOT READY** — Blocked pending GitHub publication, SignPath Foundation approval, and signed CI/CD workflow activation.

---

## 15. Exact Recommended Next Steps

1. Decide whether to move `PHASE_2_PRE_PUBLICATION_VERIFICATION_REPORT.md` outside the repository (e.g., to `D:\AutoDisplaySwitcher-Dev\`) or keep/commit it if you want audit records in the repo.
2. When ready to publish (on your explicit instruction), create `https://github.com/mahdi-014/Auto-Display-Switcher.git`, add `origin`, and push `main`.
3. Verify the initial `Build and Release` GitHub Actions run passes YAML parsing and produces `AutoDisplaySwitcher-win-x64-unsigned` artifacts cleanly.
4. In GitHub repository settings: enable private **Security Advisories**, create the `release-approval` **Environment** with required reviewer approval, and optionally pin GitHub Action versions to commit SHAs.
5. Submit the open-source code signing application to the **SignPath Foundation** referencing the public GitHub repository and MIT `LICENSE`.
6. Only after SignPath approval, wire the official SignPath signing workflow stage and test a signed release artifact against Smart App Control.
