using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 1, invocationCount: 1)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	[JsonExporterAttribute.Full]
	public class DcrawProcess
	{
		readonly string filePath;
		readonly string output;
		public DcrawProcess()
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			filePath = Path.Combine(assemblyDirectory, Program.Input);
			if (!File.Exists(filePath))
				throw new FileNotFoundException(filePath);

			output = Path.Combine(assemblyDirectory, "output.ppm");
		}

		[Benchmark]
		public void ProccessAndWrite()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				raw.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public byte[] ProccessIntoMemory()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				using (var processedImage = raw.AsProcessedImage())
					return processedImage.Buffer;
			}
		}

		[Benchmark]
		public Size ProcessIntoBitmap()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				using (var processedImage = raw.AsProcessedImage())
				using (var bitmap = processedImage.AsBitmap())
				{
					return bitmap.Size;
				}
			}
		}
	}
}
