``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2114 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8171M CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=5.0.302
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-DXVPCO : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |    Mean |   Error |  StdDev |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated | Allocated native memory | Native memory leak |
|------------------- |--------:|--------:|--------:|----------:|----------:|----------:|-----------:|------------------------:|-------------------:|
|   ProccessAndWrite | 13.09 s | 0.139 s | 0.130 s |         - |         - |         - |       1 KB |              810,799 KB |               0 KB |
| ProccessIntoMemory | 12.69 s | 0.093 s | 0.087 s |         - |         - |         - | 149,878 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | 13.03 s | 0.074 s | 0.066 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |              810,770 KB |               0 KB |
