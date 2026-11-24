@echo off
setlocal

set "APP=%~dp0AutoDisplaySwitcher.exe"
set "RUNKEY=HKCU\Software\Microsoft\Windows\CurrentVersion\Run"

if /i "%~1"=="/uninstall" goto uninstall
if /i "%~1"=="/run" goto run

if not exist "%APP%" (
    echo ERROR: AutoDisplaySwitcher.exe not found.
    pause
    exit /b 1
)

reg add "%RUNKEY%" /v "AutoDisplaySwitcher" /t REG_SZ /d "\"%APP%\"" /f >nul

if errorlevel 1 (
    echo ERROR: Could not register Windows startup.
    pause
    exit /b 1
)

start "" "%APP%"

echo.
echo Auto Display Switcher installed successfully.
echo It will start automatically when you sign in to Windows.
echo.
exit /b 0

:run
if not exist "%APP%" (
    echo ERROR: AutoDisplaySwitcher.exe not found.
    pause
    exit /b 1
)
start "" "%APP%"
exit /b 0

:uninstall
reg delete "%RUNKEY%" /v "AutoDisplaySwitcher" /f >nul 2>&1
taskkill /IM AutoDisplaySwitcher.exe /F >nul 2>&1

echo.
echo Auto Display Switcher startup registration removed.
echo.
exit /b 0
