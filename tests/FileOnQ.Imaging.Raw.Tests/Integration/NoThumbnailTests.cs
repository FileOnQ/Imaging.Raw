using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture("Images/IMG_20201121_104443.dng")]
	[Category(Constants.Category.Integration)]
	public class NoThumbnailTests
	{
		readonly string input;

		public NoThumbnailTests(string path)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public void ThumbnailWrite_NoThumbnail_Test()
		{
			var exception = Assert.Throws<RawImageException<LibRaw.Error>>(() =>
			{
				using (var image = new RawImage(input))
				using (var thumbnail = image.UnpackThumbnail())
				{
					thumbnail.Write("/path/to/output");
				}
			});

			Assert.AreEqual(LibRaw.Error.NoThumbnail, exception?.Error);
		}

		[Test]
		public void ThumbnailAsProcessedImage_NoThumbnail_Test()
		{
			var exception = Assert.Throws<RawImageException<LibRaw.Error>>(() =>
			{
				using (var image = new RawImage(input))
				using (var thumbnail = image.UnpackThumbnail())
				{
					thumbnail.AsProcessedImage();
				}
			});

			Assert.AreEqual(LibRaw.Error.NoThumbnail, exception?.Error);
		}
	}
}
