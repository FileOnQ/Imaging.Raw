``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2061 (1809/October2018Update/Redstone5)
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=5.0.102
  [Host]     : .NET 5.0.2 (5.0.220.61120), X64 RyuJIT
  Job-ZSBEID : .NET 5.0.2 (5.0.220.61120), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|              Method |       Mean |     Error |    StdDev |     Median |     Gen 0 |     Gen 1 |     Gen 2 |    Allocated | Allocated native memory | Native memory leak |
|-------------------- |-----------:|----------:|----------:|-----------:|----------:|----------:|----------:|-------------:|------------------------:|-------------------:|
|   ThumbnailAndWrite |   7.021 ms | 0.1707 ms | 0.4844 ms |   6.887 ms |         - |         - |         - |         80 B |             6,919,055 B |                  - |
| ThumbnailIntoMemory |  12.519 ms | 0.4049 ms | 1.1938 ms |  12.636 ms |         - |         - |         - |  5,683,904 B |            12,597,362 B |                  - |
| ThumbnailIntoBitmap | 278.110 ms | 5.4961 ms | 7.1465 ms | 277.418 ms | 1000.0000 | 1000.0000 | 1000.0000 | 11,367,272 B |            12,818,936 B |              160 B |
