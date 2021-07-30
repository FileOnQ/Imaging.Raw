using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture("Images\\sample1.cr2")]
	[TestFixture("Images\\@signatureeditsco(1).dng")]
	[TestFixture("Images\\@signatureeditsco.dng")]
	[TestFixture("Images\\canon_eos_r_01.cr3")]
	[TestFixture("Images\\Christian - .unique.depth.dng", ImageFormat.Bitmap)] // this file might have a bitmap instead of a jpeg
	[TestFixture("Images\\DSC_0118.nef")]
	[TestFixture("Images\\DSC02783.ARW")]
	[TestFixture("Images\\PANA2417.RW2")]
	[TestFixture("Images\\PANA8392.RW2")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw")]
	[TestFixture("Images\\signature edits APC_00171.dng")]
	[TestFixture("Images\\signature edits free raws P1015526.dng")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF")]
	[Category(Constants.Category.Integration)]
	public class Thumbnail_WriteToFile_Tests
	{
		readonly string input;
		readonly string output;
		readonly string expectedThumbnail;

		public Thumbnail_WriteToFile_Tests(string path) : this(path, ImageFormat.Jpeg) { }
		public Thumbnail_WriteToFile_Tests(string path, ImageFormat imageFormat)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
			
			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			var format = imageFormat == ImageFormat.Jpeg ? "jpeg" : "ppm";

			output = Path.Combine(directory, $"{filename}.thumb.{format}");
			expectedThumbnail = Path.Combine(directory, $"{filename}.thumb.{format}");
		}

		[OneTimeSetUp]
		public void Execute()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			{ 
				thumbnail.Write(output);
			}
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			if (File.Exists(output))
				File.Delete(output);
		}
		
		[Test]
		public void ThumbnailWrite_FileExists_Test() =>
			Assert.IsTrue(File.Exists(output));

		[Test]
		public void ThumbnailWrite_MatchBytes_Test()
		{
			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedThumbnail));
			var actualBuffer = new Span<byte>(File.ReadAllBytes(output));

			Assert.IsTrue(actualBuffer.Length > 0);
			Assert.AreEqual(expectedBuffer.Length, actualBuffer.Length);

			bool isValid = true;
			for (int index = 0; index < expectedBuffer.Length; index++)
			{
				if (expectedBuffer[index] != actualBuffer[index])
				{
					isValid = false;
					break;
				}
			}

			Assert.IsTrue(isValid, "Expected buffer does not match actual buffer, files are different");
		}
	}
}