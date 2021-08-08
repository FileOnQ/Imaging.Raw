``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.1110 (21H1/May2021Update)
AMD Ryzen 9 3950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-SSGNCM : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=10  LaunchCount=10  
UnrollFactor=1  

```
|                                      Method |     Mean |   Error |  StdDev |   Median | Ratio | RatioSD |
|-------------------------------------------- |---------:|--------:|--------:|---------:|------:|--------:|
|           ExtractThumbnailAndResize_Bicubic | 462.6 ms | 1.28 ms | 4.62 ms | 461.6 ms |  1.00 |    0.00 |
|               ExtractThumbnailAndResize_Box | 413.9 ms | 0.96 ms | 3.43 ms | 412.9 ms |  0.89 |    0.01 |
|        ExtractThumbnailAndResize_CatmullRom | 461.2 ms | 0.72 ms | 2.63 ms | 461.6 ms |  1.00 |    0.01 |
|           ExtractThumbnailAndResize_Hermite | 464.2 ms | 1.47 ms | 5.35 ms | 462.3 ms |  1.00 |    0.02 |
|          ExtractThumbnailAndResize_Lanczos2 | 463.9 ms | 1.65 ms | 5.97 ms | 462.8 ms |  1.00 |    0.02 |
|          ExtractThumbnailAndResize_Lanczos3 | 494.3 ms | 1.58 ms | 5.67 ms | 493.0 ms |  1.07 |    0.02 |
|          ExtractThumbnailAndResize_Lanczos5 | 560.8 ms | 1.54 ms | 5.61 ms | 559.6 ms |  1.21 |    0.02 |
|          ExtractThumbnailAndResize_Lanczos8 | 683.6 ms | 0.67 ms | 2.45 ms | 683.5 ms |  1.48 |    0.02 |
| ExtractThumbnailAndResize_MitchellNetravali | 462.5 ms | 1.07 ms | 3.89 ms | 461.9 ms |  1.00 |    0.02 |
|   ExtractThumbnailAndResize_NearestNeighbor | 386.5 ms | 0.87 ms | 3.16 ms | 385.7 ms |  0.84 |    0.01 |
|          ExtractThumbnailAndResize_Robidoux | 461.9 ms | 1.06 ms | 3.84 ms | 460.9 ms |  1.00 |    0.02 |
|     ExtractThumbnailAndResize_RobidouxSharp | 465.5 ms | 1.49 ms | 5.41 ms | 464.9 ms |  1.01 |    0.02 |
|            ExtractThumbnailAndResize_Spline | 468.3 ms | 1.55 ms | 5.61 ms | 466.9 ms |  1.01 |    0.01 |
|          ExtractThumbnailAndResize_Triangle | 433.1 ms | 1.53 ms | 5.54 ms | 431.9 ms |  0.94 |    0.01 |
|             ExtractThumbnailAndResize_Welch | 497.1 ms | 1.01 ms | 3.64 ms | 497.3 ms |  1.07 |    0.02 |
