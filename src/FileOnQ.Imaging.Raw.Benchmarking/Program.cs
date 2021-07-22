using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	[SimpleJob(RuntimeMoniker.Net48, launchCount: 5, invocationCount: 10)]
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 5, invocationCount: 10)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	public class LibRaw
	{
		readonly string librawInput = @"D:\FileOnQ.Imaging.Raw\images\sample1.cr2";
		
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
		public void LibRawThumbnail_SpanOfT()
		{
			//using (var rawImage = new RawImage(librawInput))
			//{
			//	rawImage.UnpackThumbnail();
			//	return rawImage.GetThumbnailAsSpan();
			//}

			using (var rawImage = new RawImage(librawInput))
			{
				rawImage.WriteTiff(@"D:\FileOnQ.Imaging.Raw\images\sample1.tiff");
				//rawImage.UnpackThumbnail();
				//return rawImage.GetThumbnailAsSpan();
			}
		}
	}
	
	class Program
	{
		static void Main(string[] args) => new LibRaw().LibRawThumbnail_SpanOfT(); //BenchmarkRunner.Run<LibRaw>();
	}
}