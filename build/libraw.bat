@echo off
title Building LibRaw
set arch=%1
for /f "usebackq tokens=1* delims=" %%x in (`vswhere -find **\vcvarsall.bat`) do set vcvar="%%~x"

if not defined vcvar (
	echo "unable to find 'vcvarsall.bat'. visual studio c++ package needs to be installed"
	exit /b 2
)

call %vcvar% %arch%
cd ../LibRaw

git clean -xd -f lib -f bin -f object

if "%arch%" == "x64" (
	nmake -f makefile_x64.msvc
)

if "%arch%" == "x86" (
	nmake -f makefile_x86.msvc
)

cd ../build
