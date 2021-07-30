using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture("Images\\sample1.cr2")]
	[Category(Constants.Category.Integration)]
	public class Thumbnail_Disposed_Tests
	{
		readonly string input;
		const string errorMessage = "The ProcessedImage must be used prior to disposing of the object.\r\nObject name: 'ProcessedImage'.";
		const string objectName = "ProcessedImage";
		public Thumbnail_Disposed_Tests(string path)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public void Disposed_ProccessedImage_Bits_Test()
		{
			ProcessedImage image;
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				image = thumbnail.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = image.Bits;
			}
			catch (RawImageDisposedException ex)
			{
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.AreEqual(errorMessage, exception?.Message);
			Assert.AreEqual(objectName, exception?.ObjectName);
		}

		[Test]
		public void Disposed_ProccessedImage_Buffer_Test()
		{
			ProcessedImage image;
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				image = thumbnail.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = image.Buffer;
			}
			catch (RawImageDisposedException ex)
			{
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.AreEqual(errorMessage, exception?.Message);
			Assert.AreEqual(objectName, exception?.ObjectName);
		}

		[Test]
		public void Disposed_ProccessedImage_Colors_Test()
		{
			ProcessedImage image;
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				image = thumbnail.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = image.Colors;
			}
			catch (RawImageDisposedException ex)
			{
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.AreEqual(errorMessage, exception?.Message);
			Assert.AreEqual(objectName, exception?.ObjectName);
		}

		[Test]
		public void Disposed_ProccessedImage_Height_Test()
		{
			ProcessedImage image;
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				image = thumbnail.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = image.Height;
			}
			catch (RawImageDisposedException ex)
			{
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.AreEqual(errorMessage, exception?.Message);
			Assert.AreEqual(objectName, exception?.ObjectName);
		}

		[Test]
		public void Disposed_ProccessedImage_Width_Test()
		{
			ProcessedImage image;
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				image = thumbnail.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = image.Width;
			}
			catch (RawImageDisposedException ex)
			{
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.AreEqual(errorMessage, exception?.Message);
			Assert.AreEqual(objectName, exception?.ObjectName);
		}

		[Test]
		public void Disposed_ProccessedImage_ImageFormat_Test()
		{
			ProcessedImage image;
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				image = thumbnail.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = image.ImageFormat;
			}
			catch (RawImageDisposedException ex)
			{
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.AreEqual(errorMessage, exception?.Message);
			Assert.AreEqual(objectName, exception?.ObjectName);
		}
	}
}
