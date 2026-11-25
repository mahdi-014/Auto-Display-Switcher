# Phase 4B — First GitHub Publication Report

**Project:** Auto Display Switcher  
**Local Repository:** `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher`  
**GitHub Repository:** `https://github.com/mahdi-014/Auto-Display-Switcher` (Public)  
**Remote URL:** `https://github.com/mahdi-014/Auto-Display-Switcher.git`  
**Publication Date & Time:** 2026-11-25 13:30 +06:00 (`2026-11-25T07:30:20Z`)

---

## 1. Publication Summary

The local `main` branch of **Auto Display Switcher** was successfully published to the empty public GitHub repository (`https://github.com/mahdi-014/Auto-Display-Switcher.git`) after passing all 10 mandatory pre-push checks.

- **Published Branch:** `main` (tracking `origin/main`)
- **Initial Published `HEAD` Hash:** `3c5733f2c5adaebc8568a65b045d8888169811ab` (`docs(release): add Phase 4A email privacy remediation report`)
- **Push Command & Result:**
  ```powershell
  git push -u origin main
  ```
  - Exit Code: `0`
  - Output: `* [new branch] main -> main` / `branch 'main' set up to track 'origin/main'.`
- **Repository-Local Identity Verified:** `Mahdi <289809368+mahdi-014@users.noreply.github.com>`

*(Note: After initial publication verification completed, this Phase 4B report was committed as a separate documentation commit and pushed to `origin/main`.)*

---

## 2. Commit History Verification

All four expected commits were verified locally in exact parent-child order before pushing, matched `git ls-remote --heads origin main` (`3c5733f2c5adaebc8568a65b045d8888169811ab`) after pushing, and were confirmed visible on `https://github.com/mahdi-014/Auto-Display-Switcher/commits/main`:

1. `1063574ef616eb8c78ecbb0641eedd3b09893665` — `Initial release: Auto Display Switcher v1.0.0 source and CI/CD`
2. `2226c2c3e64e74098bcd9a11b6bcff298eb39be9` — `chore(release): prepare Auto Display Switcher for GitHub publication`
3. `70c722e370110a739df4b4551123f37bb68534a7` — `docs(release): add Phase 3 publication preparation report`
4. `3c5733f2c5adaebc8568a65b045d8888169811ab` — `docs(release): add Phase 4A email privacy remediation report`

Every commit carries author and committer identity `Mahdi <289809368+mahdi-014@users.noreply.github.com>` with zero exposure of the prior personal email address.

---

## 3. GitHub Actions Verification

- **Run ID:** `45568650362` (Run #1, `Build and Release`, `.github/workflows/build-and-sign.yml`)
- **Run URL:** `https://github.com/mahdi-014/Auto-Display-Switcher/actions/runs/45568650362`
- **Trigger & Commit:** `push` to `refs/heads/main` (`3c5733f2c5adaebc8568a65b045d8888169811ab`) at `2026-11-25T07:30:20Z`
- **Overall Status & Conclusion:** `WORKFLOW_RUN_STATUS_COMPLETED` / `WORKFLOW_RUN_CONCLUSION_SUCCESS` (`54s`)
- **Jobs:**
  - `Build & Package Unsigned (win-x64)` (Job `139049589861`): Completed successfully in `51s` (checked out source, configured `.NET 10`, restored, built Release, published `win-x64` self-contained single-file output, validated files, generated SHA256 checksums, and uploaded `AutoDisplaySwitcher-win-x64-unsigned` artifact, `30.8 MB`).
  - `Publish Unsigned GitHub Release`: Skipped (`0s`) because `push` targeted branch `main` rather than a `v*` SemVer tag, confirming the Phase 3 release-tag guard functioned properly.

*(Verification note: Unauthenticated REST requests to `api.github.com` returned HTTP `403` due to shared-IP rate limiting; status, job timings, and artifact data were verified directly from the public GitHub Actions web pages and embedded SSR JSON payloads.)*

---

## 4. Working-Tree, Upstream, and File Visibility Status

- **Working Tree & Upstream:** `On branch main`, `Your branch is up to date with 'origin/main'.`, `nothing to commit, working tree clean`.
- **GitHub Web Availability Verified (`HTTP 200`):**
  - `https://github.com/mahdi-014/Auto-Display-Switcher`
  - `https://github.com/mahdi-014/Auto-Display-Switcher/tree/main`
  - `https://github.com/mahdi-014/Auto-Display-Switcher/commits/main`
  - `https://github.com/mahdi-014/Auto-Display-Switcher/actions`
  - `https://github.com/mahdi-014/Auto-Display-Switcher/releases` (confirmed empty — no releases created)
- **Remote Tags:** None (`git ls-remote --tags origin` empty).
- **Published Repository Files (`15` tracked files prior to this report; `16` including this report):**
  - `.github/workflows/build-and-sign.yml`, `.gitignore`, `AutoDisplaySwitcher.csproj`, `CHANGELOG.md`, `CODE-SIGNING-POLICY.md`, `CONTRIBUTING.md`, `Final-Release/AutoDisplaySwitcher.bat`, `LICENSE`, `PHASE_2_PRE_PUBLICATION_VERIFICATION_REPORT.md`, `PHASE_3_GITHUB_PUBLICATION_PREPARATION_REPORT.md`, `PHASE_4A_EMAIL_PRIVACY_REMEDIATION_REPORT.md`, `PRIVACY.md`, `Program.cs`, `README.md`, `SECURITY.md` (plus `PHASE_4B_FIRST_GITHUB_PUBLICATION_REPORT.md`).

---

## 5. Binary Exclusion & `Final-Release` Integrity Clarification

- **Compiled Binaries (`EXE` / `DLL` / `PDB`):** Verified **completely absent** from Git history (`git log --all --stat -- "*.exe" "*.dll" "*.pdb"` returned empty output). `Final-Release/AutoDisplaySwitcher.exe` was never committed or pushed and remains untracked locally via `.gitignore`.
- **Launcher Script (`Final-Release/AutoDisplaySwitcher.bat`):** By design across Phases 1–4A, `Final-Release/AutoDisplaySwitcher.bat` (the `960`-byte plain-text batch script) **is tracked** in Git (`git ls-files`) because `.github/workflows/build-and-sign.yml` copies it into the CI distribution archive, whereas the compiled executable `Final-Release/AutoDisplaySwitcher.exe` remains strictly outside Git history.
- **Preserved Local `Final-Release` Hashes (Unchanged):**
  - `Final-Release\AutoDisplaySwitcher.exe` (`37,603,734` bytes): `3D359426E2F4D835B3B5EE8F551834CF58B84DCB7773B35A12E49460C9029816` (`NotSigned`)
  - `Final-Release\AutoDisplaySwitcher.bat` (`960` bytes): `B8437182525BA4E45812D21EF5756D356380D2E870CD9FF410A5EC8164C8C7A9`

---

## 6. Warnings & Remaining Unresolved Items

1. **GitHub REST API Rate Limit (`HTTP 403`):** Unauthenticated calls to `api.github.com` hit the hourly IP rate limit; verification used Git and public GitHub HTTPS web/SSR responses instead.
2. **Code Signing Pending:** No code-signing certificate or SignPath Foundation approval exists yet; any built EXE remains unsigned and may be blocked by Windows Smart App Control / Code Integrity.
3. **External GitHub Settings Pending:** In GitHub repository settings, enable private **Security Advisories** and configure the `release-approval` **Environment** (with required reviewers) prior to tagging a release or onboarding with SignPath Foundation.
4. **No Release or Tag Created:** Stopped after source publication verification as instructed.
