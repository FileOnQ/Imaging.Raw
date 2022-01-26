# How to Compile

FileOnQ.Imaging.Raw uses a mix of .NET Framework, .NET, and native C/C++ projects. You will need to have additional build tools installed to properly build this locally. This readme serves as a guide for new developers to contribute.

## Visual Studio and .NET

This project uses a combination of Visual Studio and .NET CLI tools for building and developing.

* Visual Studio 2019 (16.10.3) or newer
  * Workload: Desktop development with C++
  * Component: Windows 10 SDK (10.0.18)
* .NET CLI 5.0.300 or newer

If you are using an older version of Visual Studio or .NET CLI you won't have access to the `_OR_GREATER` preprocessor directives. This will result in runtime issues when running the library locally.

## Build Dependencies

Grab the LibRaw dependencies by running the command `git submodule update --init --recursive` in the repo's root folder. You will also need to download and install [CUDA SDK 11.4](https://developer.nvidia.com/cuda-11-4-3-download-archive)

## Compile

First time compilation takes a while. Future builds won't take as long even if you do a clean and rebuild as vcpkg will cache binaries in your user directory.

By compiling the main project `FileOnQ.Imaging.Raw` you will be compiling all native dependencies. No need to run any additional compilation instructions.

### Visual Studio

Right click on `FileOnQ.Imaging.Raw` and select build

### CLI

```shell
dotnet build src/FileOnQ.Imaging.Raw/FileOnQ.Imaging.Raw.csproj
```

## Build Scripts and Structure

The entire build process is orchestrated from `FileOnQ.Imaging.Raw.csproj` which includes downloading and compiling third party libraries. During the build you'll need to clone several git repositories and compile them. This leverages a combination of dotnet build targets and bat files. The build targets can be found in the `src/FileOnQ.Imaging.Raw/Build` directory. The build scripts or batch files can be found in the `build` directory.

The build targets and scripts should work out of the box if the required dependencies above have been installed.

### Build Targets

TBD

### Build Scripts (bat)

TBD
