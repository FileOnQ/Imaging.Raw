using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	[SimpleJob(RuntimeMoniker.Net48, launchCount: 50, invocationCount: 500)]
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 50, invocationCount: 500)]
	//[SimpleJob(RunStrategy.ColdStart, launchCount: 10, invocationCount: 100)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	public class LibRaw
	{
		readonly string librawInput = @"";
		//readonly string librawInput = @"";
		
		[Benchmark(Description = "Buffers.MemoryCopy")]
		public byte[] LibRawThumbnail_MemoryCopy()
		{
			using (var rawImage = new RawImage(librawInput))
			{
				rawImage.UnpackThumbnail();
				return rawImage.GetThumbnailAsByteArray(0);
			}
		}
		
		[Benchmark(Description = "Marshal.Copy")]
		public byte[] LibRawThumbnail_MarshalCopy()
		{
			using (var rawImage = new RawImage(librawInput))
			{
				rawImage.UnpackThumbnail();
				return rawImage.GetThumbnailAsByteArray(1);
			}
		}
		
		// [Benchmark(Description = "Unsafe.CopyBlock")]
		// public byte[] LibRawThumbnail_CopyBlock()
		// {
		// 	using (var rawImage = new RawImage(librawInput))
		// 	{
		// 		rawImage.UnpackThumbnail();
		// 		return rawImage.GetThumbnailAsByteArray(2);
		// 	}
		// }
		//
		// [Benchmark(Description = "Unsafe.CopyBlockUnaligned")]
		// public byte[] LibRawThumbnail_CopyBlockUnaligned()
		// {
		// 	using (var rawImage = new RawImage(librawInput))
		// 	{
		// 		rawImage.UnpackThumbnail();
		// 		return rawImage.GetThumbnailAsByteArray(3);
		// 	}
		// }
		
		[Benchmark(Description = "Pointer Address Assignment")]
		public byte[] LibRawThumbnail_PointerMath()
		{
			using (var rawImage = new RawImage(librawInput))
			{
				rawImage.UnpackThumbnail();
				return rawImage.GetThumbnailAsByteArray(4);
			}
		}
		
		[Benchmark(Description = "Span<T>")]
		public Span<byte> LibRawThumbnail_SpanOfT()
		{
			using (var rawImage = new RawImage(librawInput))
			{
				rawImage.UnpackThumbnail();
				return rawImage.GetThumbnailAsSpan();
			}
		}
	}
	
	class Program
	{
		static void Main(string[] args) => BenchmarkRunner.Run<LibRaw>();
	}
}