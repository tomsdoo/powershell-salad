@echo off
set currd=%~dp0
set currd=%currd:"=%
set psf=%currd%bootstrap\initialize_psenv.ps1
start C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe -noexit %psf% %currd%
