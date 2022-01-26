``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5)
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-LQHYRX : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-DTVSNQ : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-BUVHVD : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|              Method |        Job |            Runtime |      Mean |    Error |   StdDev |     Gen 0 |     Gen 1 |     Gen 2 |    Allocated | Allocated native memory | Native memory leak |
|-------------------- |----------- |------------------- |----------:|---------:|---------:|----------:|----------:|----------:|-------------:|------------------------:|-------------------:|
|   ThumbnailAndWrite | Job-LQHYRX |           .NET 5.0 |  12.33 ms | 0.242 ms | 0.637 ms |         - |         - |         - |        624 B |            12,602,272 B |                  - |
| ThumbnailIntoMemory | Job-LQHYRX |           .NET 5.0 |  11.50 ms | 0.241 ms | 0.700 ms |         - |         - |         - |  5,683,936 B |            12,597,362 B |                  - |
| ThumbnailIntoBitmap | Job-LQHYRX |           .NET 5.0 | 321.95 ms | 6.413 ms | 8.110 ms | 1000.0000 | 1000.0000 | 1000.0000 | 11,367,304 B |            12,818,936 B |              160 B |
|   ThumbnailAndWrite | Job-DTVSNQ |           .NET 6.0 |  11.50 ms | 0.342 ms | 1.002 ms |         - |         - |         - |      1,104 B |            12,602,272 B |                  - |
| ThumbnailIntoMemory | Job-DTVSNQ |           .NET 6.0 |  11.64 ms | 0.350 ms | 0.998 ms |         - |         - |         - |  5,684,416 B |            12,597,362 B |                  - |
| ThumbnailIntoBitmap | Job-DTVSNQ |           .NET 6.0 | 331.23 ms | 6.332 ms | 6.219 ms | 1000.0000 | 1000.0000 | 1000.0000 | 11,368,944 B |            12,819,096 B |              320 B |
|   ThumbnailAndWrite | Job-BUVHVD | .NET Framework 4.8 |  11.73 ms | 0.234 ms | 0.546 ms |         - |         - |         - |      8,192 B |            12,602,264 B |                  - |
| ThumbnailIntoMemory | Job-BUVHVD | .NET Framework 4.8 |  10.99 ms | 0.228 ms | 0.658 ms |         - |         - |         - |  5,691,416 B |            12,597,360 B |                  - |
| ThumbnailIntoBitmap | Job-BUVHVD | .NET Framework 4.8 | 342.81 ms | 5.177 ms | 5.085 ms | 1000.0000 | 1000.0000 | 1000.0000 | 17,117,760 B |            12,818,990 B |              216 B |
