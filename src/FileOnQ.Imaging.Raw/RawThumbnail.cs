using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	unsafe class RawThumbnail : IRawThumbnail
	{
		readonly IntPtr libraw;

		public RawThumbnail(IntPtr libraw)
		{
			this.libraw = libraw;
		}

		public void Write(string file)
		{
			var error = LibRaw.libraw_dcraw_thumb_writer(libraw, file);
			if (error != LibRaw.Error.Success)
				throw new RawImageException(error);
		}

		public Span<byte> GetSpan()
		{
			var error = LibRaw.Error.Success;
			var thumbnail = LibRaw.libraw_dcraw_make_mem_thumb(libraw, ref error);
			if (error != LibRaw.Error.Success)
				throw new RawImageException(error);

			// get the memory address of the data buffer.
			var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
			var address = (IntPtr) thumbnail + memoryOffset;
			return new Span<byte>(address.ToPointer(), (int)thumbnail->DataSize);
		}

		public byte[] CopyToByteArray() => GetSpan().ToArray();
	}
}