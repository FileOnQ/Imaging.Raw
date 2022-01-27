``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8171M CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-OZTICK : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |    Mean |   Error |  StdDev | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|------------------- |--------:|--------:|--------:|------------------------:|-------------------:|----------:|----------:|----------:|-----------:|
|   ProccessAndWrite | 13.80 s | 0.066 s | 0.062 s |              810,799 KB |               0 KB |         - |         - |         - |       2 KB |
| ProccessIntoMemory | 13.47 s | 0.057 s | 0.054 s |              810,769 KB |                  - |         - |         - |         - | 149,879 KB |
|  ProcessIntoBitmap | 13.92 s | 0.072 s | 0.068 s |              810,770 KB |               0 KB | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |
