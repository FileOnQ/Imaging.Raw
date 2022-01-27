``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5)
Intel Xeon CPU E5-2673 v3 2.40GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-GHACPE : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-GQUBLI : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-VVECKY : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|             Method |        Job |            Runtime |    Mean |   Error |  StdDev |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated | Allocated native memory | Native memory leak |
|------------------- |----------- |------------------- |--------:|--------:|--------:|----------:|----------:|----------:|-----------:|------------------------:|-------------------:|
|   ProccessAndWrite | Job-GHACPE |           .NET 5.0 | 13.08 s | 0.175 s | 0.163 s |         - |         - |         - |       2 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-GHACPE |           .NET 5.0 | 12.78 s | 0.053 s | 0.049 s |         - |         - |         - | 149,882 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-GHACPE |           .NET 5.0 | 13.37 s | 0.130 s | 0.115 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |              810,770 KB |                  - |
|   ProccessAndWrite | Job-GQUBLI |           .NET 6.0 | 13.19 s | 0.110 s | 0.103 s |         - |         - |         - |       2 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-GQUBLI |           .NET 6.0 | 12.85 s | 0.064 s | 0.057 s |         - |         - |         - | 149,887 KB |              810,772 KB |               3 KB |
|  ProcessIntoBitmap | Job-GQUBLI |           .NET 6.0 | 13.36 s | 0.065 s | 0.058 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,765 KB |              810,774 KB |               3 KB |
|   ProccessAndWrite | Job-VVECKY | .NET Framework 4.8 | 13.18 s | 0.100 s | 0.084 s |         - |         - |         - |       8 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-VVECKY | .NET Framework 4.8 | 13.34 s | 0.228 s | 0.213 s |         - |         - |         - | 149,885 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-VVECKY | .NET Framework 4.8 | 13.38 s | 0.144 s | 0.134 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,763 KB |              810,770 KB |                  - |
