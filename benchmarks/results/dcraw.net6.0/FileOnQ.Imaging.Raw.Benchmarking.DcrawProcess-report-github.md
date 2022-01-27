``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5)
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-NVWGSN : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Runtime=.NET 6.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |    Mean |   Error |  StdDev | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|------------------- |--------:|--------:|--------:|------------------------:|-------------------:|----------:|----------:|----------:|-----------:|
|   ProccessAndWrite | 12.49 s | 0.239 s | 0.224 s |              810,802 KB |               3 KB |         - |         - |         - |       3 KB |
| ProccessIntoMemory | 12.23 s | 0.236 s | 0.220 s |              810,772 KB |               3 KB |         - |         - |         - | 149,887 KB |
|  ProcessIntoBitmap | 12.54 s | 0.228 s | 0.213 s |              810,770 KB |                  - | 1000.0000 | 1000.0000 | 1000.0000 | 299,764 KB |
