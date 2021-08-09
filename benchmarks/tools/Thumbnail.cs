using System.Drawing;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	// comparer can only handle 1 job at a time
	//[SimpleJob(RuntimeMoniker.Net48, launchCount: 1, invocationCount: 1)]
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 1, invocationCount: 1)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	[JsonExporterAttribute.Full]
	public class Thumbnail
	{
		readonly string filePath;
		readonly string output;
		public Thumbnail()
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			filePath = Path.Combine(assemblyDirectory, Program.Input);
			if (!File.Exists(filePath))
				throw new FileNotFoundException(filePath);

			output = Path.Combine(assemblyDirectory, "output.jpeg");
		}

		[Benchmark]
		public void ThumbnailAndWrite()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ThumbnailProcessor());
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public byte[] ThumbnailIntoMemory()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ThumbnailProcessor());
				using (var processedImage = thumb.AsProcessedImage())
					return processedImage.Buffer;
			}
		}

		[Benchmark]
		public Size ThumbnailIntoBitmap()
		{
			var size = Size.Empty;
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ThumbnailProcessor());
				using (var processedImage = thumb.AsProcessedImage())
				using (var bitmap = processedImage.AsBitmap())
				{
					size = bitmap.Size;
				}
			}

			return size;
		}
	}
}
