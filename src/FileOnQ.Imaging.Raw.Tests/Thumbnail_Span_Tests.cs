using System;
using System.IO;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("images/sample1.cr2")]
	[TestFixture("images/@signatureeditsco(1).dng")]
	[TestFixture("images/@signatureeditsco.dng")]
	[TestFixture("images/canon_eos_r_01.cr3")]
	[TestFixture("images/Christian - .unique.depth.dng", ImageFormat.Bitmap)]
	[TestFixture("images/DSC_0118.nef")]
	[TestFixture("images/DSC02783.ARW")]
	[TestFixture("images/PANA2417.RW2")]
	[TestFixture("images/PANA8392.RW2")]
	[TestFixture("images/photo by @Dupe.png--@Emily.rosegold.arw")]
	[TestFixture("images/signature edits APC_00171.dng")]
	[TestFixture("images/signature edits free raws P1015526.dng")]
	[TestFixture("images/signature edits free raws_DSC7082.NEF")]
	[TestFixture("images/signatureeditsfreerawphoto.NEF")]
	public class Thumbnail_Span_Tests
	{
		readonly string input;
		readonly string expectedThumbnail;

		public Thumbnail_Span_Tests(string path) : this(path, ImageFormat.Jpeg) { }
		public Thumbnail_Span_Tests(string path, ImageFormat imageFormat)
		{
			input = path;
			
			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			var format = imageFormat == ImageFormat.Jpeg ? "jpeg" : "ppm";
			expectedThumbnail = Path.Combine(directory, $"{filename}.thumb.{format}");
		}

		[Test]
		public void ThumbnailSpan_Test()
		{
			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedThumbnail));

			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				var image = thumbnail.AsProcessedImage();
				var actualBuffer = image.Buffer;

				// NOTE - 7/23/2021 - @ahoefling
				// If the image is a bitmap, remove the header from the expected file.
				// It is considered normal behavior for the buffer to omit the bitmap
				// header.
				if (image.ImageFormat == ImageFormat.Bitmap)
					expectedBuffer = expectedBuffer.Slice(15, actualBuffer.Length);

				Assert.IsTrue(actualBuffer.Length > 0);
				Assert.AreEqual(expectedBuffer.Length, actualBuffer.Length);

				// NOTE - 7/23/2021 - @ahoefling
				// This is a slow operation, there may be span specific APIs to speed this up
				for (int index = 0; index < expectedBuffer.Length; index++)
					Assert.AreEqual(expectedBuffer[index], actualBuffer[index]);
			}
		}
	}
}