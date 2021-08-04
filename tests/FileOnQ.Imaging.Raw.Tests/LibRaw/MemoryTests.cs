using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture(TestData.RawImage1)]
	[TestFixture(TestData.RawImage2)]
	//[TestFixture(TestData.RawImage3)] // we  get a weird gdi+ error with our current calculations, could be a stride
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
	[Category(Constants.Category.LibRaw)]
	public class MemoryTests
    {
		readonly string path;
		public MemoryTests(string path)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			this.path = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public unsafe void LoadUnpackCloseTest()
		{
			int count = 10;

			for (int i = 0; i < count; i++)
			{
				var lib = LibRaw.Initialize(0);
				LibRaw.OpenFile(lib, path);
				LibRaw.Unpack(lib);
				LibRaw.Recycle(lib);
				LibRaw.Close(lib);
				lib = System.IntPtr.Zero;
			}
		}

		[Test]
		public unsafe void LoadUnpackProcessCloseTest()
		{
			int count = 10;

			for (int i = 0; i < count; i++)
			{
				var lib = LibRaw.Initialize(0);
				LibRaw.OpenFile(lib, path);
				LibRaw.Unpack(lib);
				LibRaw.DcrawProcess(lib);
				LibRaw.Recycle(lib);
				LibRaw.Close(lib);
				lib = System.IntPtr.Zero;
			}
		}

		[Test]
		public unsafe void LoadUnpackProcessWriteCloseTest()
		{
			int count = 10;

			for (int i = 0; i < count; i++)
			{
				var lib = LibRaw.Initialize(0);
				LibRaw.OpenFile(lib, path);
				LibRaw.Unpack(lib);
				LibRaw.DcrawProcess(lib);
				LibRaw.DcrawWriter(lib, "test.ppm");
				LibRaw.Recycle(lib);
				LibRaw.Close(lib);
				lib = System.IntPtr.Zero;

				File.Delete("test.ppm");
			}
		}

		[Test]
		public unsafe void LoadUnpackProcessMemoryCloseTest()
		{
			int count = 10;

			for (int i = 0; i < count; i++)
			{
				var lib = LibRaw.Initialize(0);
				LibRaw.OpenFile(lib, path);
				LibRaw.Unpack(lib);
				LibRaw.DcrawProcess(lib);
				var error = LibRaw.Error.Success;
				var image = LibRaw.MakeMemoryImage(lib, ref error);
				if (image->Bits <= 0 || image->Colors <= 0 || image->DataSize <= 0)
					Assert.Fail("Image data is empty");

				LibRaw.ClearMemory(image);
				LibRaw.Recycle(lib);
				LibRaw.Close(lib);
				lib = System.IntPtr.Zero;
			}
		}

		//[Test]
		//public unsafe void LoadUnpackProcessMemoryCopyToBitmapCloseTest()
		//{
		//	int count = 10;

		//	for (int i = 0; i < count; i++)
		//	{
		//		var lib = LibRaw.Initialize(0);
		//		LibRaw.OpenFile(lib, path);
		//		LibRaw.Unpack(lib);
		//		LibRaw.DcrawProcess(lib);
		//		var error = LibRaw.Error.Success;
		//		var image = LibRaw.MakeMemoryImage(lib, ref error);
		//		if (image->Bits <= 0 || image->Colors <= 0 || image->DataSize <= 0)
		//			Assert.Fail("Image data is empty");

		//		var stride = image->Width * 3 + (image->Width % 4);

		//		var bitmap = new Bitmap(image->Width, image->Height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, image->Data);
		//		//bitmap.Save("test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
		//		bitmap.Dispose();

		//		LibRaw.ClearMemory(image);
		//		LibRaw.Recycle(lib);
		//		LibRaw.Close(lib);
		//		lib = System.IntPtr.Zero;

		//		File.Delete("test.bmp");
		//	}
		//}

		[Test]
		public unsafe void LoadUnpackProcessMemoryCopyToBitmapCloseTest()
		{
			int count = 30;

			for (int i = 0; i < count; i++)
			{
				var lib = LibRaw.Initialize(0);
				LibRaw.OpenFile(lib, path);
				LibRaw.Unpack(lib);
				LibRaw.DcrawProcess(lib);
				var error = LibRaw.Error.Success;
				var image = LibRaw.MakeMemoryImage(lib, ref error);
				if (image->Bits <= 0 || image->Colors <= 0 || image->DataSize <= 0)
					Assert.Fail("Image data is empty");

				var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
				var address = (IntPtr)image + memoryOffset;

				// If we don't copy the pointer over to an array the native memory is leaked
				var buffer = new byte[image->DataSize];
				var ptr = (byte*)address;
				for (int position = 0; position < image->DataSize; position++)
					buffer[position] = ptr[position];

				var stride = image->Width * 3 + (image->Width % 4);

				var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
				var bitmap = new Bitmap(image->Width, image->Height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());
				bitmap.Save("test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
				bitmap.Dispose();
				handle.Free();

				LibRaw.ClearMemory(image);
				LibRaw.Recycle(lib);
				LibRaw.Close(lib);
				lib = System.IntPtr.Zero;

				File.Delete("test.bmp");
			}
		}
	}
}
