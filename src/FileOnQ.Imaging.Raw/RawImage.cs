using System;

namespace FileOnQ.Imaging.Raw
{
	public unsafe class RawImage : IRawImage
	{
		string file;
		IntPtr libraw;
		
		// TODO - 7/25/2021 - @ahoefling
		// Add exception tests if file does not exist. If we
		// don't manage this in the C# code the problem will
		// persist in weird ways down stream such as OutOfOrderCall
		public RawImage(string file)
		{
			this.file = file;
			libraw = LibRaw.Initialize(0);
			if (libraw == IntPtr.Zero)
			{
				throw new Exception("Unable to initialize raw image");
			}

			var error = LibRaw.OpenFile(libraw, file);
			if (error != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(error);
		}

		public IUnpackedImage UnpackThumbnail()
		{
			var errorCode = LibRaw.UnpackThumbnail(libraw);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);

			return new UnpackedImage(libraw);
		}

		public IUnpackedImage UnpackRaw()
		{
			var errorCode = LibRaw.Unpack(libraw);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);

			return new UnpackedImage(libraw);
		}

		~RawImage() => Dispose(false);

		bool isDisposed;
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (isDisposed)
				return;

			if (disposing)
			{
				// free managed resources
			}
			
			if (libraw != IntPtr.Zero)
			{
				LibRaw.Recycle(libraw);
				LibRaw.Close(libraw);
				libraw = IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}
