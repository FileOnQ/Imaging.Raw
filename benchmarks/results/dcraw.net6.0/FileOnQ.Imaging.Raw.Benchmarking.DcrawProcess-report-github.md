``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5)
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-GCGFIT : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-TOLBEO : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-LZATWM : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|             Method |        Job |            Runtime |    Mean |   Error |  StdDev |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated | Allocated native memory | Native memory leak |
|------------------- |----------- |------------------- |--------:|--------:|--------:|----------:|----------:|----------:|-----------:|------------------------:|-------------------:|
|   ProccessAndWrite | Job-GCGFIT |           .NET 5.0 | 12.42 s | 0.248 s | 0.296 s |         - |         - |         - |       1 KB |              810,799 KB |               0 KB |
| ProccessIntoMemory | Job-GCGFIT |           .NET 5.0 | 12.18 s | 0.158 s | 0.148 s |         - |         - |         - | 149,879 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-GCGFIT |           .NET 5.0 | 11.74 s | 0.086 s | 0.076 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |              810,770 KB |                  - |
|   ProccessAndWrite | Job-TOLBEO |           .NET 6.0 | 11.36 s | 0.225 s | 0.210 s |         - |         - |         - |       2 KB |              810,802 KB |               3 KB |
| ProccessIntoMemory | Job-TOLBEO |           .NET 6.0 | 11.54 s | 0.219 s | 0.466 s |         - |         - |         - | 149,879 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-TOLBEO |           .NET 6.0 | 11.81 s | 0.221 s | 0.317 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |              810,770 KB |                  - |
|   ProccessAndWrite | Job-LZATWM | .NET Framework 4.8 | 12.32 s | 0.072 s | 0.060 s |         - |         - |         - |       8 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-LZATWM | .NET Framework 4.8 | 12.00 s | 0.141 s | 0.132 s |         - |         - |         - | 149,885 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-LZATWM | .NET Framework 4.8 | 11.75 s | 0.230 s | 0.391 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,763 KB |              810,770 KB |                  - |
