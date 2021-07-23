using System;
using System.IO;

namespace FileOnQ.Imaging.Raw.ConsoleApp
{
	static class Program
    {
		static readonly string testImage = @"D:\FileOnQ.Imaging.Raw\images\sample1.cr2";
		static readonly string outputImage = @"D:\FileOnQ.Imaging.Raw\images\sample1.thumb.jpeg";

		static void Main(string[] args)
        {
			Console.WriteLine($"Testing raw image file {testImage}");

			try
			{
				using (IRawImage image = new RawImage(testImage))
				{
					Console.WriteLine("Image open successfully!");

					var thumbnail = image.UnpackThumbnail();
					Console.WriteLine("Thumbnail Unpacked");

					thumbnail.Write(outputImage);
					//File.WriteAllBytes(outputImage, thumbnail.GetSpan().ToArray());
					Console.WriteLine($"Thumbnail written to location: {outputImage}");
				}
			}
			catch (RawImageException ex)
			{
				Console.WriteLine($"An error occurred! {ex.Error}");
			}

			Console.WriteLine("Raw image disposed, all native memory freed");
        }
    }
}
