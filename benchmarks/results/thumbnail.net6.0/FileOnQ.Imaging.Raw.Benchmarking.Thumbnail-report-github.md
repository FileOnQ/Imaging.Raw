``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8370C CPU 2.80GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-RTTKFE : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Runtime=.NET 6.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|              Method |       Mean |     Error |    StdDev | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|-------------------- |-----------:|----------:|----------:|------------------------:|-------------------:|----------:|----------:|----------:|----------:|
|   ThumbnailAndWrite |  10.563 ms | 0.0820 ms | 0.0767 ms |               12,307 KB |                  - |         - |         - |         - |      1 KB |
| ThumbnailIntoMemory |   9.074 ms | 0.2609 ms | 0.7609 ms |               12,302 KB |                  - |         - |         - |         - |  5,551 KB |
| ThumbnailIntoBitmap | 279.354 ms | 3.2639 ms | 3.0530 ms |               12,519 KB |               0 KB | 1000.0000 | 1000.0000 | 1000.0000 | 11,103 KB |
