using System;
using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw;

namespace ConsoleApp
{
	static class Program
    {
		static void Main(string[] args)
        {
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			var testImage = Path.Combine(assemblyDirectory, "sample1.cr2");
			var output = "output.ppm";

			Console.WriteLine($"Testing raw image file {testImage}");

			try
			{
				using (var image = new RawImage(testImage))
				using (var thumbnail = image.UnpackRaw())
				{
					Console.WriteLine("Image open successfully!");
					Console.WriteLine("Thumbnail Unpacked");

					thumbnail.Write(output);
					Console.WriteLine($"Thumbnail written to location: {output}");
				}
			}
			catch (RawImageException<LibRaw.Error> ex)
			{
				Console.WriteLine($"An error occurred! {ex.Error}");
			}

			Console.WriteLine("Raw image disposed, all native memory freed");
        }
    }
}
