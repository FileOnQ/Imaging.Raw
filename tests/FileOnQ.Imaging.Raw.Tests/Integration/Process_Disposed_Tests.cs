using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture("Images\\PANA2417.RW2")]
	[Category(Constants.Category.Integration)]
	public class Process_Disposed_Tests
	{
		readonly string input;
		const string errorMessage = "The ProcessedImage must be used prior to disposing of the object.\r\nObject name: 'ProcessedImage'.";
		const string objectName = "ProcessedImage";
		public Process_Disposed_Tests(string path)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public void Disposed_ProccessedImage_Bits_Test()
		{
			ProcessedImage processedImage;
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				processedImage = raw.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = processedImage.Bits;
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
			ProcessedImage processedImage;
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				processedImage = raw.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = processedImage.Buffer;
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
			ProcessedImage processedImage;
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				processedImage = raw.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = processedImage.Colors;
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
			ProcessedImage processedImage;
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				processedImage = raw.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = processedImage.Height;
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
			ProcessedImage processedImage;
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				processedImage = raw.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = processedImage.Width;
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
			ProcessedImage processedImage;
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				processedImage = raw.AsProcessedImage();
			}

			RawImageDisposedException? exception = null;
			try
			{
				_ = processedImage.ImageFormat;
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
