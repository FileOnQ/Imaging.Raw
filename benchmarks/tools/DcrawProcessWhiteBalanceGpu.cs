using System.Drawing;
using System.IO;
using System.Reflection;
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
	[JsonExporterAttribute.Full]
	public class DcrawProcessWhiteBalanceGpu
	{
		readonly string filePath;
		readonly string output;
		public DcrawProcessWhiteBalanceGpu()
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			filePath = Path.Combine(assemblyDirectory, Program.Input);
			if (!File.Exists(filePath))
				throw new FileNotFoundException(filePath);

			output = Path.Combine(assemblyDirectory, "output.ppm");
		}

		[Benchmark(Baseline = true)]
		public Size ProcessIntoBitmapCpu()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new WhiteBalanceProcessor());
				using (var processedImage = raw.AsProcessedImage())
				using (var bitmap = processedImage.AsBitmap())
				{
					return bitmap.Size;
				}
			}
		}

		[Benchmark]
		public Size ProcessIntoBitmapGpu()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new WhiteBalanceProcessor());
				using (var processedImage = raw.AsProcessedImage())
				using (var bitmap = processedImage.AsBitmap(true))
				{
					return bitmap.Size;
				}
			}
		}

		class WhiteBalanceProcessor : DcrawProcessor
		{
			protected override void SetOutputParameters(DcrawOutputParameters parameters)
			{
				parameters.UseCameraWhiteBalance = 1;
			}
		}
	}
}
