# FileOnQ.Imaging.Raw

**This project is a work in progress all code is currently experimental**

A C#/.NET wrapper around [LibRaw](https://github.com/LibRaw/LibRaw) to simplify processing raw images and retrieving thumbnails.

<!-- Add all badges here such as CI Build, wiki, etc. -->
[![Build](https://github.com/FileOnQ/Imaging.Raw/actions/workflows/package_main.yml/badge.svg?branch=main)](https://github.com/FileOnQ/Imaging.Raw/actions/workflows/package_main.yml)
[![NuGet](https://img.shields.io/badge/NuGet-FileOnQ.Imaging.Raw-blue.svg)](https://www.nuget.org/packages/FileOnQ.Imaging.Raw)

## Contributing

⭐ Pull Requests and Issues are always welcomed ⭐

* [Contributing](CONTRIBUTING.md)
* [Developer Guide](DEVELOPER_GUIDE.md)
* [Building](BUILDING.md)
* [CI Builds](CI_BUILDS.md)

## Setup

Install the NuGet package into your target head and any shared projects.

[![NuGet](https://img.shields.io/badge/NuGet-FileOnQ.Imaging.Raw-blue.svg)](https://www.nuget.org/packages/FileOnQ.Imaging.Raw)

*The NuGet package must be included in the entry point or target head, otherwise the native assemblies won't be copied over to the bin directory correctly.*

### Windows

Any Windows device running an application using this library requires the Visual C++ Redistributable package installed. See the [Microsoft Visual C++ Redistributable Latest Supported Downloads](https://docs.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist). You will need to install the correct CPU Architecture(s) that your application supports.

* [vc_redist.arm64.exe](https://aka.ms/vs/17/release/vc_redist.arm64.exe)
* [vc_redist.x86.exe](https://aka.ms/vs/17/release/vc_redist.x86.exe)
* [vc_redist.x64.exe](https://aka.ms/vs/17/release/vc_redist.x64.exe)

## Supported Target Frameworks

FileOnQ.Imaging.Raw is available for use in the following target frameworks

| Platform         | Supported | Version                 |
|------------------|-----------|-------------------------|
| net48            | ✅        | 1.0.0                   |
| net5.0           | ✅        | 1.0.0                   |
| net6.0           | ✅        | 1.0.0                   |
| Xamarin.iOS      | ❌        | Planned                 |
| Xamarin.Mac      | ❌        | Planned                 |
| MonoAndroid      | ❌        | Planned                 |

## Supported Runtime Identifiers

FileOnQ.Imaging.Heif is available for use in the following runtime identifiers

| Platform         | Supported | Version                 |
|------------------|-----------|-------------------------|
| win-x86          | ✅        | 1.0.0                   |
| win-x64          | ✅        | 1.0.0                   |
| win-ARM64        | ❌        | Planned                 |
| osx-x64          | ❌        | Planned                 |
| linux-x64        | ❌        | Planned                 |

## Usage

Saves a raw image as a ppm

```C#
using (var image = new RawImage("image.RW2"))
using (var raw = image.UnpackRaw())
{
    raw.Process(new DcrawProcessor());
    raw.Write("output.ppm");
}
```

Saves the embedded thumbnail as a ppm

```C#
using (var image = new RawImage("image.RW2"))
using (var thumbnail = image.UnpackThumbnail())
{
    thumbnail.Process(new DcrawProcessor());
    thumbnail.Write("output.ppm");
}
```

## Documentation

We don't have a wiki or full API documentation. If you are interested in helping, create an issue so we can discuss.

## Dependencies

* C++ Library: [LibRaw](https://github.com/LibRaw/LibRaw)

## Created By FileOnQ

This library was created by FileOnQ and donated to the open source community.
