# Phase 4A — GitHub Email Privacy Remediation Report

**Project:** Auto Display Switcher  
**Local Project:** `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher`  
**Branch:** `main`  
**Remote:** `https://github.com/mahdi-014/Auto-Display-Switcher.git` *(not pushed)*  
**Date:** 2026-11-25

---

## 1. Executive Summary

To resolve GitHub's `GH007` email-privacy rejection without weakening GitHub privacy settings or exposing a personal email address in public history, repository-local Git identity was configured to the GitHub `noreply` address and the three unpublished commits on `main` were cleanly rewritten locally.

No application source files, release binaries, backups, or Windows security settings were altered, and nothing was pushed to GitHub.

---

## 2. Original vs Rewritten Commit Hashes

Every commit preserved its exact tree hash, commit message, file contents, file modes, timestamps, and parent-child order:

| Order | Original Commit Hash | Rewritten Commit Hash | Tree Hash (Unchanged) | Commit Subject |
| :---: | :--- | :--- | :--- | :--- |
| 1 | `2f5bb2031187e534c59dd82e3b5b9738261ac14b` | `1063574ef616eb8c78ecbb0641eedd3b09893665` | `b42961abd4098d7288d9f3d64668197621b5bf90` | `Initial release: Auto Display Switcher v1.0.0 source and CI/CD` |
| 2 | `ea4cc02a4f72edda59ab38d598b48e90aad87507` | `2226c2c3e64e74098bcd9a11b6bcff298eb39be9` | `1b424672486cfb216a8d80932f735f6862bbc4dc` | `chore(release): prepare Auto Display Switcher for GitHub publication` |
| 3 | `7b29b3fd3fed9239c390287eb1445a31a99a09c5` | `70c722e370110a739df4b4551123f37bb68534a7` | `b344b136e06c59b85332b2720f481ffb58a341a0` | `docs(release): add Phase 3 publication preparation report` |

*(Note: This report is committed as commit 4 on `main` using the same noreply identity.)*

---

## 3. Repository-Local Git Identity

Configured strictly at repository scope (`--local`); global Git identity was not modified:
- `git config --local user.name`: `Mahdi`
- `git config --local user.email`: `289809368+mahdi-014@users.noreply.github.com`

---

## 4. Backup and Bundle Verification

Created prior to rewriting history and preserved alongside all earlier backups:
- **Pre-Rewrite Full Backup (including `.git`):** `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-Backup-20261125-132605` (`Exists=True`)
- **Pre-Rewrite Git Bundle:** `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-pre-email-rewrite-20261125-132605.bundle` (`git bundle verify` passed: contains `7b29b3fd3fed9239c390287eb1445a31a99a09c5 refs/heads/main`, exit `0`)
- **Preserved Earlier Backups & Bundles:**
  - `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-Backup-20261124-105151` (`Exists=True`)
  - `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-Backup-20261125-130712` (`Exists=True`)
  - `D:\AutoDisplaySwitcher-Dev\AutoDisplaySwitcher-pre-remediation-20261125-130712.bundle` (`Exists=True`)

---

## 5. Email Privacy Verification

- Removed temporary `refs/original/refs/heads/main` reference left by `git filter-branch`, expired reflogs (`git reflog expire --expire=now --all`), and ran `git gc --prune=now`.
- Verified `git show-ref` lists only `refs/heads/main`.
- Verified **every** commit across `git log --all` uses:
  - `Author: Mahdi <289809368+mahdi-014@users.noreply.github.com>`
  - `Committer: Mahdi <289809368+mahdi-014@users.noreply.github.com>`
- Verified **0 occurrences** of the prior personal email address in `git log --all` commit metadata and **0 occurrences** in tracked files via `git grep`.

---

## 6. File Integrity Verification

- **Tracked Files:** 14 files before this report commit (including `PHASE_2_PRE_PUBLICATION_VERIFICATION_REPORT.md` and `PHASE_3_GITHUB_PUBLICATION_PREPARATION_REPORT.md`); 15 files including this report.
- **No Binaries or Secrets in Git History:** `git log --all --stat -- "*.exe" "*.dll" "*.pdb" "*.zip"` returned empty output.
- **`Final-Release` Integrity (Unchanged, Untracked):**
  - `Final-Release\AutoDisplaySwitcher.exe` (`37,603,734` bytes): `3D359426E2F4D835B3B5EE8F551834CF58B84DCB7773B35A12E49460C9029816` (`NotSigned`)
  - `Final-Release\AutoDisplaySwitcher.bat` (`960` bytes): `B8437182525BA4E45812D21EF5756D356380D2E870CD9FF410A5EC8164C8C7A9`

---

## 7. Git Status & Remote Status

- **Working Tree Status:** Clean after committing this report (`On branch main`, `nothing to commit, working tree clean`).
- **Remote Configuration:**
  - `origin` -> `https://github.com/mahdi-014/Auto-Display-Switcher.git` (`fetch` and `push`)
- **Remote State:** Empty (`git ls-remote origin` returns zero refs; nothing was pushed in this phase).
- **Next Step:** Awaiting explicit user instruction to execute `git push -u origin main`.
