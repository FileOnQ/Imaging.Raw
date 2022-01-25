using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 1, invocationCount: 1)]
	[SimpleJob(RuntimeMoniker.Net48, launchCount: 1, invocationCount: 1)]
	[SimpleJob(RuntimeMoniker.Net60, launchCount: 1, invocationCount: 1)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	public class AsBitmap
	{
		readonly byte[] buffer;
		readonly int width;
		readonly int height;
		readonly int stride;
		GCHandle handle;
		IntPtr pointer;

		public AsBitmap()
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			var filePath = Path.Combine(assemblyDirectory, "..\\..\\..\\..\\generated-data-file");
			if (!File.Exists(filePath))
				throw new FileNotFoundException(filePath);

			var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			buffer = File.ReadAllBytes(Path.Combine(directory, filePath));
			
			// These files are hard-coded for the generated image. If the generated image changes the benchmark will fail
			width = 8688;
			height = 5792;
			stride = width * 3 + (width % 4);
		}

		[Benchmark(Baseline = true)]
		public Size ConvertPpmToBmp_CpuPtr()
		{
			var size = Size.Empty;
			using (var bitmap = AsBitmapCpuPtr())
				size = bitmap.Size;

			if (handle != default)
				handle.Free();

			return size;
		}

		[Benchmark]
		public Size ConvertPpmToBmp_Gpu()
		{
			var size = Size.Empty;
			using (var bitmap = AsBitmapGpu())
				size = bitmap.Size;

			if (pointer != IntPtr.Zero)
				Cuda.FreeMemory(pointer);

			return size;
		}

		Bitmap AsBitmapCpuPtr()
		{
			var offset = width % 4;
			var strideWithoutOffset = width * 3;
			var stride = strideWithoutOffset + offset;

			var additionalBytes = offset * height; // We should subtract offset to ensure we remove trailing bytes // Additional - this matches the native bitmap result
			var bitmapBuffer = new byte[buffer.Length + additionalBytes];

			var bitmapPosition = 0;
			for (int position = 0; position < buffer.Length; position += 3)
			{
				if (position > 0 && position % strideWithoutOffset == 0)
				{
					for (int j = 0; j < offset; j++)
					{
						bitmapBuffer[bitmapPosition] = 0;
						bitmapPosition++;
					}
				}

				bitmapBuffer[bitmapPosition + 2] = buffer[position];
				bitmapBuffer[bitmapPosition + 1] = buffer[position + 1];
				bitmapBuffer[bitmapPosition] = buffer[position + 2];

				bitmapPosition += 3;
			}

			handle = GCHandle.Alloc(bitmapBuffer, GCHandleType.Pinned);
			return new Bitmap(width, height,
				stride,
				System.Drawing.Imaging.PixelFormat.Format24bppRgb,
				handle.AddrOfPinnedObject());
		}

		Bitmap AsBitmapGpu()
		{
			var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			var error = Cuda.Error.Success;
			int length = 0;
			pointer = Cuda.ProcessBitmap(handle.AddrOfPinnedObject(), buffer.Length, width, height, ref length, ref error);

			var stride = width * 3 + (width % 4);
			return new Bitmap(width, height,
				stride,
				System.Drawing.Imaging.PixelFormat.Format24bppRgb,
				pointer);
		}
	}
}
