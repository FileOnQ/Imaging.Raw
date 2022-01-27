``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5)
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-USFLIU : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-ZUTUJS : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-ATZPAP : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|             Method |        Job |            Runtime |    Mean |   Error |  StdDev |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated | Allocated native memory | Native memory leak |
|------------------- |----------- |------------------- |--------:|--------:|--------:|----------:|----------:|----------:|-----------:|------------------------:|-------------------:|
|   ProccessAndWrite | Job-USFLIU |           .NET 5.0 | 12.58 s | 0.063 s | 0.059 s |         - |         - |         - |       2 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-USFLIU |           .NET 5.0 | 12.23 s | 0.075 s | 0.070 s |         - |         - |         - | 149,879 KB |              810,769 KB |               0 KB |
|  ProcessIntoBitmap | Job-USFLIU |           .NET 5.0 | 12.65 s | 0.063 s | 0.059 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |              810,770 KB |                  - |
|   ProccessAndWrite | Job-ZUTUJS |           .NET 6.0 | 12.74 s | 0.040 s | 0.031 s |         - |         - |         - |       2 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-ZUTUJS |           .NET 6.0 | 12.62 s | 0.109 s | 0.102 s |         - |         - |         - | 149,887 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-ZUTUJS |           .NET 6.0 | 12.73 s | 0.054 s | 0.050 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,765 KB |              810,770 KB |                  - |
|   ProccessAndWrite | Job-ATZPAP | .NET Framework 4.8 | 12.81 s | 0.099 s | 0.092 s |         - |         - |         - |       8 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-ATZPAP | .NET Framework 4.8 | 12.27 s | 0.049 s | 0.046 s |         - |         - |         - | 149,885 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-ATZPAP | .NET Framework 4.8 | 12.82 s | 0.081 s | 0.076 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,763 KB |              810,770 KB |                  - |
