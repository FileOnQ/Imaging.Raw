using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("Images\\sample1.cr2")]
	[TestFixture("Images\\@signatureeditsco(1).dng")]
	[TestFixture("Images\\@signatureeditsco.dng")]
	[TestFixture("Images\\canon_eos_r_01.cr3")]
	[TestFixture("Images\\Christian - .unique.depth.dng", ImageFormat.Bitmap)]
	[TestFixture("Images\\DSC_0118.nef")]
	[TestFixture("Images\\DSC02783.ARW")]
	[TestFixture("Images\\PANA2417.RW2")]
	[TestFixture("Images\\PANA8392.RW2")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw")]
	[TestFixture("Images\\signature edits APC_00171.dng")]
	[TestFixture("Images\\signature edits free raws P1015526.dng")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF")]
	public class Thumbnail_AsBitmap_Tests
	{
		readonly string input;
		readonly string output;
		readonly string expectedThumbnail;

		public Thumbnail_AsBitmap_Tests(string path) : this(path, ImageFormat.Jpeg) { }
		public Thumbnail_AsBitmap_Tests(string path, ImageFormat imageFormat)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			input = Path.Combine(assemblyDirectory, path);

			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			expectedThumbnail = Path.Combine(directory, $"{filename}.thumb.bmp");
			output = $"{filename}.thumb.bmp";
		}

		[TearDown]
		public void TearDown()
		{
			//if (File.Exists(output))
			//	File.Delete(output);
		}

		[Test]
		public void ThumbnailAsBitmap_Test()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			using (var bitmap = thumbnail.AsBitmap())
			{
				bitmap.Save(output);
			}

			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedThumbnail));
			var actualBuffer = new Span<byte>(File.ReadAllBytes(output));

			Assert.IsTrue(actualBuffer.Length > 0);
			Assert.AreEqual(expectedBuffer.Length, actualBuffer.Length);

			// This is a slow operation, there may be span specific APIs to speed this up
			for (int index = 0; index < expectedBuffer.Length; index++)
				Assert.AreEqual(expectedBuffer[index], actualBuffer[index]);
		}

		[Test]
		public void ThumbnailAsBitmap_Gpu_Test()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			using (var bitmap = thumbnail.AsBitmap(true))
			{
				bitmap.Save(output);
			}

			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedThumbnail));
			var actualBuffer = new Span<byte>(File.ReadAllBytes(output));

			Assert.IsTrue(actualBuffer.Length > 0);
			Assert.AreEqual(expectedBuffer.Length, actualBuffer.Length);

			// This is a slow operation, there may be span specific APIs to speed this up
			for (int index = 0; index < expectedBuffer.Length; index++)
				Assert.AreEqual(expectedBuffer[index], actualBuffer[index]);
		}
	}
}
