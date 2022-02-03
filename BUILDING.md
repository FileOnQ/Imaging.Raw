# How to Compile

`FileOnQ.Imaging.Raw` uses a mix of .NET Framework, .NET, native C/C++ projects, and CUDA kernels for GPU processing. You will need to have additional build tools installed to properly build this locally. This readme serves as a guide for new developers to contribute.

## Visual Studio and .NET

This project uses a combination of Visual Studio and .NET CLI tools for building and developing.

* Visual Studio 2019 (16.10.3) or newer
  * Workload: Desktop development with C++
  * Component: Windows 10 SDK (10.0.18)
* .NET CLI 5.0.300 or newer

If you are using an older version of Visual Studio or .NET CLI you won't have access to the `_OR_GREATER` preprocessor directives. This will result in runtime issues when running the library locally.

## Build Dependencies

* LibRaw: add these dependencies by running the below command in the repo's root folder.
  
  ```shell
  git submodule update --init --recursive
  ```

  or clone them with the repo using

   ```shell
   git clone --recurse-submodules https://github.com/FileOnQ.Imaging.Raw.git
   ```

* CUDA: You will need to download and install [CUDA SDK 11.4](https://developer.nvidia.com/cuda-11-4-3-download-archive)

## Compile

First time compilation takes a while. Future builds won't take as long even if you do a clean and rebuild.

By compiling the main project `FileOnQ.Imaging.Raw` you will be compiling all native dependencies. No need to run any additional compilation instructions.

### Visual Studio

Right click on `FileOnQ.Imaging.Raw` and select build

### CLI

```shell
dotnet build src/FileOnQ.Imaging.Raw/FileOnQ.Imaging.Raw.csproj
```

### Troubleshooting

If after doing all of the above and compiling is causing errors. Try opening a developer command prompt as admin, navigate to the repo's root directory and run one of the following

```shell
msbuild /p:Platform="x64"
```

 ```shell
 msbuild /p:Platform="x86"
 ```

## Build Scripts and Structure

The entire build process is orchestrated from `FileOnQ.Imaging.Raw.csproj` which includes downloading and compiling third party libraries. During the build you'll need to clone several git repositories and compile them. This leverages a combination of dotnet build targets and bat files. The build targets can be found in the `src/FileOnQ.Imaging.Raw/Build` directory. The build scripts or batch files can be found in the `build` directory.

The build targets and scripts should work out of the box if the required dependencies above have been installed.

### Build Targets

The build targets located in the main project `FileOnQ.Imaging.Raw` are used to orchestrate builds for all supported platform architectures. They all follow a similar process

1. Clone repository for each supported platform architecture
2. Run build script (bat file)
3. Copy generated assemblies over to `src/FileOnQ.Imaging.Raw/runtimes/platform/native`

### Build Scripts (bat)

The build scripts load the Visual Studio Developer Command Prompt for the desired platform architecture and then run the build commands. This is really important when compiling native libraries for x86, x64, ARM, etc. Once the tools are loaded it compiles the code using the native toolchains.
