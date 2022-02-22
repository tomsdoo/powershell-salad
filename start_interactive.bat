@echo off
set currd=%~dp0
set currd=%currd:"=%
set psf=%currd%bootstrap\initialize_psenv.ps1
start powershell -noexit %psf% %currd%
