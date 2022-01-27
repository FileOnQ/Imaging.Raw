``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5)
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-JGPFPG : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-OTDNUU : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-IYYIBY : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|              Method |        Job |            Runtime |       Mean |     Error |     StdDev |     Median |     Gen 0 |     Gen 1 |     Gen 2 |    Allocated | Allocated native memory | Native memory leak |
|-------------------- |----------- |------------------- |-----------:|----------:|-----------:|-----------:|----------:|----------:|----------:|-------------:|------------------------:|-------------------:|
|   ThumbnailAndWrite | Job-JGPFPG |           .NET 5.0 |  10.044 ms | 0.4451 ms |  1.2842 ms |   9.705 ms |         - |         - |         - |        624 B |            12,602,272 B |                  - |
| ThumbnailIntoMemory | Job-JGPFPG |           .NET 5.0 |   8.911 ms | 0.1891 ms |  0.5425 ms |   8.931 ms |         - |         - |         - |  5,683,936 B |            12,597,362 B |                  - |
| ThumbnailIntoBitmap | Job-JGPFPG |           .NET 5.0 | 287.015 ms | 7.0047 ms | 20.6535 ms | 290.483 ms | 1000.0000 | 1000.0000 | 1000.0000 | 11,367,304 B |            12,818,936 B |              160 B |
|   ThumbnailAndWrite | Job-OTDNUU |           .NET 6.0 |  11.265 ms | 0.2609 ms |  0.7650 ms |  10.999 ms |         - |         - |         - |      1,104 B |            12,602,272 B |                  - |
| ThumbnailIntoMemory | Job-OTDNUU |           .NET 6.0 |   9.546 ms | 0.2076 ms |  0.5888 ms |   9.418 ms |         - |         - |         - |  5,684,416 B |            12,597,362 B |                  - |
| ThumbnailIntoBitmap | Job-OTDNUU |           .NET 6.0 | 259.933 ms | 4.5432 ms |  4.0275 ms | 260.480 ms | 1000.0000 | 1000.0000 | 1000.0000 | 11,369,016 B |            12,819,096 B |              320 B |
|   ThumbnailAndWrite | Job-IYYIBY | .NET Framework 4.8 |  10.413 ms | 0.1998 ms |  0.3703 ms |  10.376 ms |         - |         - |         - |      8,192 B |            12,602,264 B |                  - |
| ThumbnailIntoMemory | Job-IYYIBY | .NET Framework 4.8 |  10.193 ms | 0.4410 ms |  1.2864 ms |  10.004 ms |         - |         - |         - |  5,691,416 B |            12,597,360 B |                  - |
| ThumbnailIntoBitmap | Job-IYYIBY | .NET Framework 4.8 | 311.730 ms | 6.2026 ms |  9.0916 ms | 313.472 ms | 1000.0000 | 1000.0000 | 1000.0000 | 17,117,760 B |            12,818,990 B |              216 B |
