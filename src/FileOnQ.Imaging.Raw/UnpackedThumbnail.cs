using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	unsafe class UnpackedThumbnail : IUnpackedImage
	{
		IntPtr libraw;
		LibRaw.ProcessedImage* thumbnail;
		IImageProcessor processor;

		public UnpackedThumbnail(IntPtr libraw)
		{
			this.libraw = libraw;
		}

		public void Process(IImageProcessor newProcessor)
		{
			if (processor != null)
			{
				processor.Dispose();
				processor = null;
			}

			processor = newProcessor;
			LoadThumbnail();
			processor.Process(new RawImageData
			{
				LibRawData = libraw,
				Bits = thumbnail->Bits,
				Colors = thumbnail->Colors,
				Height = thumbnail->Height,
				Width = thumbnail->Width,
				Buffer = new Span<byte>((void*)GetBufferMemoryAddress(), (int)thumbnail->DataSize)
			});
		}

		void LoadThumbnail()
		{
			if ((IntPtr)thumbnail == IntPtr.Zero)
			{
				var error = LibRaw.Error.Success;
				thumbnail = LibRaw.MakeMemoryThumbnail(libraw, ref error);
				if (error != LibRaw.Error.Success)
					throw new RawImageException<LibRaw.Error>(error);
			}
		}

		IntPtr GetBufferMemoryAddress()
		{
			// get the memory address of the data buffer.
			var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
			return (IntPtr)thumbnail + memoryOffset;
		}

		public void Write(string file)
		{
			if (processor == null)
				throw new NullReferenceException("Call Process(IImageProcessor) first");

			// REVIEW - 8/7/2021 - @ahoefling - this isn't needed when libraw is writing to disk, maybe there is a way to check if we are using libraw vs memory
			LoadThumbnail();
			processor.Write(new RawImageData
			{
				LibRawData = libraw,
				Bits = thumbnail->Bits,
				Colors = thumbnail->Colors,
				Height = thumbnail->Height,
				Width = thumbnail->Width,
				Buffer = new Span<byte>((void*)GetBufferMemoryAddress(), (int)thumbnail->DataSize)

			}, file);

			//var error = LibRaw.ThumbnailWriter(libraw, file);
			//if (error != LibRaw.Error.Success)
			//	throw new RawImageException<LibRaw.Error>(error);
		}

		public ProcessedImage AsProcessedImage()
		{
			LoadThumbnail();
			return processor.AsProcessedImage(new RawImageData
			{
				LibRawData = libraw,
				Bits = thumbnail->Bits,
				Colors = thumbnail->Colors,
				Height = thumbnail->Height,
				Width = thumbnail->Width,
				Buffer = new Span<byte>((void*)GetBufferMemoryAddress(), (int)thumbnail->DataSize)
			});

			//// get the memory address of the data buffer.
			//var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
			//var address = (IntPtr)thumbnail + memoryOffset;

			//// copy values from native memory to managed memory
			//var buffer = new byte[thumbnail->DataSize];
			//var ptr = (byte*)address;
			//for (int position = 0; position < thumbnail->DataSize; position++)
			//	buffer[position] = ptr[position];

			//return new ProcessedImage
			//{
			//	ImageFormat = (ImageFormat)thumbnail->Type,
			//	Buffer = buffer,
			//	Height = thumbnail->Height,
			//	Width = thumbnail->Width,
			//	Colors = thumbnail->Colors,
			//	Bits = thumbnail->Bits
			//};
		}

		~UnpackedThumbnail() => Dispose(false);

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

			if (thumbnail != null)
			{
				LibRaw.ClearMemory(thumbnail);
				thumbnail = (LibRaw.ProcessedImage*)IntPtr.Zero;
			}

			if (libraw == IntPtr.Zero)
			{
				// clear the pointer, but don't clear the memory. Let the pointer owner clear the memory
				libraw = IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}