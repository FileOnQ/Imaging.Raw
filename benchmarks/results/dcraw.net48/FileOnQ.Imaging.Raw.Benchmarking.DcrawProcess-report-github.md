``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5), VM=Hyper-V
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT
  Job-GHSIOX : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

Runtime=.NET Framework 4.8  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |    Mean |   Error |  StdDev | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |     Allocated |
|------------------- |--------:|--------:|--------:|------------------------:|-------------------:|----------:|----------:|----------:|--------------:|
|   ProccessAndWrite | 12.36 s | 0.160 s | 0.150 s |           830,257,503 B |                  - |         - |         - |         - |             - |
| ProccessIntoMemory | 12.14 s | 0.234 s | 0.260 s |           830,226,694 B |                  - |         - |         - |         - | 153,482,264 B |
|  ProcessIntoBitmap | 12.67 s | 0.224 s | 0.209 s |           830,228,638 B |                  - | 1000.0000 | 1000.0000 | 1000.0000 | 306,957,376 B |
