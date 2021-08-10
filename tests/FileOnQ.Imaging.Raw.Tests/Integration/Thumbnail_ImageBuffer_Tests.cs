using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw.Tests.Utilities;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture(TestData.RawImage1)]
	[TestFixture(TestData.RawImage2)]
	[TestFixture(TestData.RawImage3)]
	[TestFixture(TestData.RawImage4)]
	[TestFixture(TestData.RawImage5)]
	[TestFixture(TestData.RawImage6)]
	[TestFixture(TestData.RawImage7)]
	[TestFixture(TestData.RawImage8)]
	[TestFixture(TestData.RawImage9)]
	[TestFixture(TestData.RawImage10)]
	[TestFixture(TestData.RawImage11)]
	[TestFixture(TestData.RawImage12)]
	[TestFixture(TestData.RawImage13)]
	[TestFixture(TestData.RawImage14)]
	[Category(Constants.Category.Integration)]
	public class Thumbnail_ImageBuffer_Tests
	{
		readonly string input;
		readonly string hash;

		public Thumbnail_ImageBuffer_Tests(string path)
		{
			this.hash = TestData.Integration.ThumbnailAsImageBuffer.HashCodes[path];

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public void ThumbnailImageBuffer_Test()
		{
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				thumbnail.Process(new ThumbnailProcessor());
				var image = thumbnail.AsProcessedImage();

				AssertUtilities.IsHashEqual(hash, image.Buffer);
			}
		}
	}
}