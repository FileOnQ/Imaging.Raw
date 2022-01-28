``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5), VM=Hyper-V
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT
  Job-GHSIOX : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

Runtime=.NET Framework 4.8  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|              Method |       Mean |     Error |     StdDev |     Median | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |    Allocated |
|-------------------- |-----------:|----------:|-----------:|-----------:|------------------------:|-------------------:|----------:|----------:|----------:|-------------:|
|   ThumbnailAndWrite |  11.741 ms | 0.2340 ms |  0.5086 ms |  11.652 ms |            12,601,608 B |                  - |         - |         - |         - |            - |
| ThumbnailIntoMemory |   9.161 ms | 0.1828 ms |  0.4553 ms |   9.011 ms |            12,597,040 B |                  - |         - |         - |         - |  5,691,416 B |
| ThumbnailIntoBitmap | 293.274 ms | 5.8564 ms | 15.1172 ms | 293.231 ms |            12,818,670 B |              216 B | 1000.0000 | 1000.0000 | 1000.0000 | 17,117,760 B |
