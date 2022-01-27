``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5), VM=Hyper-V
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
  [Host] : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|              Method |        Job |            Runtime | Mean | Error |
|-------------------- |----------- |------------------- |-----:|------:|
|   ThumbnailAndWrite | Job-ULYOBS |           .NET 5.0 |   NA |    NA |
| ThumbnailIntoMemory | Job-ULYOBS |           .NET 5.0 |   NA |    NA |
| ThumbnailIntoBitmap | Job-ULYOBS |           .NET 5.0 |   NA |    NA |
|   ThumbnailAndWrite | Job-EYIFXY |           .NET 6.0 |   NA |    NA |
| ThumbnailIntoMemory | Job-EYIFXY |           .NET 6.0 |   NA |    NA |
| ThumbnailIntoBitmap | Job-EYIFXY |           .NET 6.0 |   NA |    NA |
|   ThumbnailAndWrite | Job-GHSIOX | .NET Framework 4.8 |   NA |    NA |
| ThumbnailIntoMemory | Job-GHSIOX | .NET Framework 4.8 |   NA |    NA |
| ThumbnailIntoBitmap | Job-GHSIOX | .NET Framework 4.8 |   NA |    NA |

Benchmarks with issues:
  Thumbnail.ThumbnailAndWrite: Job-ULYOBS(Runtime=.NET 5.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailIntoMemory: Job-ULYOBS(Runtime=.NET 5.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailIntoBitmap: Job-ULYOBS(Runtime=.NET 5.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailAndWrite: Job-EYIFXY(Runtime=.NET 6.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailIntoMemory: Job-EYIFXY(Runtime=.NET 6.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailIntoBitmap: Job-EYIFXY(Runtime=.NET 6.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailAndWrite: Job-GHSIOX(Runtime=.NET Framework 4.8, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailIntoMemory: Job-GHSIOX(Runtime=.NET Framework 4.8, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  Thumbnail.ThumbnailIntoBitmap: Job-GHSIOX(Runtime=.NET Framework 4.8, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
