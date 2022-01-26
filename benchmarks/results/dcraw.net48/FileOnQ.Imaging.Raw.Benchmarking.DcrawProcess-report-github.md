``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5), VM=Hyper-V
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
  [Host] : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|             Method |        Job |            Runtime | Mean | Error |
|------------------- |----------- |------------------- |-----:|------:|
|   ProccessAndWrite | Job-ULYOBS |           .NET 5.0 |   NA |    NA |
| ProccessIntoMemory | Job-ULYOBS |           .NET 5.0 |   NA |    NA |
|  ProcessIntoBitmap | Job-ULYOBS |           .NET 5.0 |   NA |    NA |
|   ProccessAndWrite | Job-EYIFXY |           .NET 6.0 |   NA |    NA |
| ProccessIntoMemory | Job-EYIFXY |           .NET 6.0 |   NA |    NA |
|  ProcessIntoBitmap | Job-EYIFXY |           .NET 6.0 |   NA |    NA |
|   ProccessAndWrite | Job-GHSIOX | .NET Framework 4.8 |   NA |    NA |
| ProccessIntoMemory | Job-GHSIOX | .NET Framework 4.8 |   NA |    NA |
|  ProcessIntoBitmap | Job-GHSIOX | .NET Framework 4.8 |   NA |    NA |

Benchmarks with issues:
  DcrawProcess.ProccessAndWrite: Job-ULYOBS(Runtime=.NET 5.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProccessIntoMemory: Job-ULYOBS(Runtime=.NET 5.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProcessIntoBitmap: Job-ULYOBS(Runtime=.NET 5.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProccessAndWrite: Job-EYIFXY(Runtime=.NET 6.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProccessIntoMemory: Job-EYIFXY(Runtime=.NET 6.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProcessIntoBitmap: Job-EYIFXY(Runtime=.NET 6.0, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProccessAndWrite: Job-GHSIOX(Runtime=.NET Framework 4.8, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProccessIntoMemory: Job-GHSIOX(Runtime=.NET Framework 4.8, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
  DcrawProcess.ProcessIntoBitmap: Job-GHSIOX(Runtime=.NET Framework 4.8, InvocationCount=1, LaunchCount=1, UnrollFactor=1)
