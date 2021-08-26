``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.1165 (21H1/May2021Update)
AMD Ryzen 9 3950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-PEJUGP : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|               Method |    Mean |    Error |   StdDev | Ratio |     Gen 0 |     Gen 1 |     Gen 2 | Allocated | Allocated native memory | Native memory leak |
|--------------------- |--------:|---------:|---------:|------:|----------:|----------:|----------:|----------:|------------------------:|-------------------:|
| ProcessIntoBitmapCpu | 7.092 s | 0.0149 s | 0.0132 s |  1.00 | 1000.0000 | 1000.0000 | 1000.0000 |    293 MB |                  792 MB |                  - |
| ProcessIntoBitmapGpu | 7.041 s | 0.0180 s | 0.0168 s |  0.99 | 1000.0000 | 1000.0000 | 1000.0000 |    293 MB |                  938 MB |               0 MB |
