``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2061 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8171M CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=5.0.102
  [Host]     : .NET 5.0.2 (5.0.220.61120), X64 RyuJIT
  Job-JKMUPJ : .NET 5.0.2 (5.0.220.61120), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |    Mean |   Error |  StdDev |     Gen 0 |     Gen 1 |     Gen 2 |     Allocated | Allocated native memory | Native memory leak |
|------------------- |--------:|--------:|--------:|----------:|----------:|----------:|--------------:|------------------------:|-------------------:|
|   ProccessAndWrite | 13.71 s | 0.226 s | 0.212 s |         - |         - |         - |         136 B |           523,310,031 B |                  - |
| ProccessIntoMemory | 14.08 s | 0.171 s | 0.160 s |         - |         - |         - | 153,475,616 B |           676,752,948 B |                  - |
|  ProcessIntoBitmap | 14.49 s | 0.249 s | 0.233 s | 1000.0000 | 1000.0000 | 1000.0000 | 306,949,408 B |           676,754,892 B |                  - |
