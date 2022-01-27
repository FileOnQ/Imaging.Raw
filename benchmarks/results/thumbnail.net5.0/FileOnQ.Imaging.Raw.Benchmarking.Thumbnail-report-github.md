``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-EUBMYS : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|              Method |      Mean |    Error |   StdDev | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |    Allocated |
|-------------------- |----------:|---------:|---------:|------------------------:|-------------------:|----------:|----------:|----------:|-------------:|
|   ThumbnailAndWrite |  11.14 ms | 0.192 ms | 0.170 ms |            12,602,272 B |                  - |         - |         - |         - |        624 B |
| ThumbnailIntoMemory |  10.09 ms | 0.200 ms | 0.527 ms |            12,597,362 B |                  - |         - |         - |         - |  5,683,936 B |
| ThumbnailIntoBitmap | 265.03 ms | 1.481 ms | 1.385 ms |            12,818,960 B |              184 B | 1000.0000 | 1000.0000 | 1000.0000 | 11,367,720 B |
