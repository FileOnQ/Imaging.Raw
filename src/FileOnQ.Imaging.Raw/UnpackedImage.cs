using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	unsafe abstract class UnpackedImage : IUnpackedImage
	{
		public UnpackedImage(IntPtr libraw)
		{
			this.LibRaw = libraw;
		}

		protected IntPtr LibRaw { get; set; }
		protected LibRaw.ProcessedImage* Image { get; set; }
		protected IImageProcessor Processor { get; set; }

		public void Process(IImageProcessor newProcessor)
		{
			if (Processor != null)
			{
				Processor.Dispose();
				Processor = null;
			}

			Processor = newProcessor;
			LoadImage();
			Processor.Process(new RawImageData
			{
				LibRawData = LibRaw,
				Bits = Image->Bits,
				Colors = Image->Colors,
				Height = Image->Height,
				Width = Image->Width,
				Buffer = new Span<byte>((void*)GetBufferMemoryAddress(), (int)Image->DataSize)
			});
		}

		protected abstract void LoadImage();
		
		IntPtr GetBufferMemoryAddress()
		{
			// get the memory address of the data buffer.
			var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(Raw.LibRaw.ProcessedImage.Data)).ToInt32();
			return (IntPtr)Image + memoryOffset;
		}

		public void Write(string file)
		{
			if (Processor == null)
				throw new NullReferenceException("Call Process(IImageProcessor) first");

			// REVIEW - 8/7/2021 - @ahoefling - this isn't needed when libraw is writing to disk, maybe there is a way to check if we are using libraw vs memory
			LoadImage();
			Processor.Write(new RawImageData
			{
				LibRawData = LibRaw,
				Bits = Image->Bits,
				Colors = Image->Colors,
				Height = Image->Height,
				Width = Image->Width,
				Buffer = new Span<byte>((void*)GetBufferMemoryAddress(), (int)Image->DataSize)

			}, file);
		}

		public ProcessedImage AsProcessedImage()
		{
			LoadImage();
			return Processor.AsProcessedImage(new RawImageData
			{
				LibRawData = LibRaw,
				Bits = Image->Bits,
				Colors = Image->Colors,
				Height = Image->Height,
				Width = Image->Width,
				Buffer = new Span<byte>((void*)GetBufferMemoryAddress(), (int)Image->DataSize)
			});
		}

		~UnpackedImage() => Dispose(false);

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

			if (Image != null)
			{
				Raw.LibRaw.ClearMemory(Image);
				Image = (LibRaw.ProcessedImage*)IntPtr.Zero;
			}

			if (LibRaw == IntPtr.Zero)
			{
				// clear the pointer, but don't clear the memory. Let the pointer owner clear the memory
				LibRaw = IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}