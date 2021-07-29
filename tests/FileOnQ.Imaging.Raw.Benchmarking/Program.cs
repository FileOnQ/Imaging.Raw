using System;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	class Program
	{
		static readonly string input = "Images/sample1.cr2";
		static void Main(string[] args)
		{
			AsBitmapRunner();
		}

		static void AsBitmapRunner()
		{
			Console.WriteLine("AsBitmap benchmark starting");
			Console.WriteLine("Generating raw ppm file to use in benchmark, this operation may take sometime . . .");

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			var filePath = Path.Combine(assemblyDirectory, "generated-data-file");

			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				var processedImage = raw.AsProcessedImage();

				File.WriteAllBytes(filePath, processedImage.Buffer.ToArray());
			}

			Console.WriteLine("Input data generated, starting benchmark!");
			BenchmarkRunner.Run<AsBitmap>();
		}
	}
}