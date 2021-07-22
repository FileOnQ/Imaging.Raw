using System;

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

		//var thumbnail = LibRaw.libraw_dcraw_make_mem_thumb(libraw, ref errorCode);
		public Span<byte> GetSpan() => throw new NotImplementedException();

		public byte[] CopyToByteArray() => throw new NotImplementedException();
	}
}