using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("images/IMG_20201121_104443.dng")]
	public class NoThumbnailTests
	{
		readonly string input;

		public NoThumbnailTests(string path) =>
			input = path;

		[Test]
		public void ThumbnailWrite_NoThumbnail_Test()
		{
			var exception = Assert.Throws<RawImageException>(() =>
			{
				using (var image = new RawImage(input))
				{
					var thumbnail = image.UnpackThumbnail();
					thumbnail.Write("/path/to/output");
				}
			});

			Assert.AreEqual(LibRaw.Error.NoThumbnail, exception?.Error);
		}

		[Test]
		public void ThumbnailGetSpan_NoThumbnail_Test()
		{
			var exception = Assert.Throws<RawImageException>(() =>
			{
				using (var image = new RawImage(input))
				{
					var thumbnail = image.UnpackThumbnail();
					thumbnail.GetSpan();
				}
			});

			Assert.AreEqual(LibRaw.Error.NoThumbnail, exception?.Error);
		}

		[Test]
		public void ThumbnailCopyToByteArray_NoThumbnail_Test()
		{
			var exception = Assert.Throws<RawImageException>(() =>
			{
				using (var image = new RawImage(input))
				{
					var thumbnail = image.UnpackThumbnail();
					thumbnail.GetSpan();
				}
			});

			Assert.AreEqual(LibRaw.Error.NoThumbnail, exception?.Error);
		}
	}
}
