@echo off
title Building LibRaw
set arch=%1
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\VC\Auxiliary\Build\vcvarsall.bat" %arch%
cd ../LibRaw

git clean -xd -f lib -f bin -f object

if "%arch%" == "x64" (
	nmake -f makefile_x64.msvc
)

if "%arch%" == "x86" (
	nmake -f makefile_x86.msvc
)

cd ../build