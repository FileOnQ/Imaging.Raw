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
			if (error != LibRaw.LibRawError.Success)
				throw new RawImageException(error);
		}

		public Span<byte> GetSpan()
		{
			var error = LibRaw.LibRawError.Success;
			var thumbnail = LibRaw.libraw_dcraw_make_mem_thumb(libraw, ref error);
			if (error != LibRaw.LibRawError.Success)
				throw new RawImageException(error);

			// get the memory address of the data buffer.
			var address = (IntPtr)thumbnail + Marshal.OffsetOf(typeof(LibRaw.LibRawProcessedImage), "Data").ToInt32();
			return new Span<byte>(address.ToPointer(), (int)thumbnail->DataSize);
		}

		public byte[] CopyToByteArray() => GetSpan().ToArray();
	}
}