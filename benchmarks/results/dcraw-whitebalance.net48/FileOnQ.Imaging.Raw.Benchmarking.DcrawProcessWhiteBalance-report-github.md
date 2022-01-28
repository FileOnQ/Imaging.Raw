``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5), VM=Hyper-V
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT
  Job-GHSIOX : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

Runtime=.NET Framework 4.8  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |    Mean |   Error |  StdDev | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |     Allocated |
|------------------- |--------:|--------:|--------:|------------------------:|-------------------:|----------:|----------:|----------:|--------------:|
|   ProccessAndWrite | 10.90 s | 0.214 s | 0.210 s |           830,257,503 B |                  - |         - |         - |         - |             - |
| ProccessIntoMemory | 10.83 s | 0.140 s | 0.131 s |           830,226,694 B |                  - |         - |         - |         - | 153,482,264 B |
|  ProcessIntoBitmap | 10.87 s | 0.213 s | 0.363 s |           830,228,638 B |                  - | 1000.0000 | 1000.0000 | 1000.0000 | 306,957,376 B |
