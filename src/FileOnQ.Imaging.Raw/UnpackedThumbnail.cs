using System;

namespace FileOnQ.Imaging.Raw
{
	unsafe class UnpackedThumbnail : UnpackedImage
	{
		public UnpackedThumbnail(IntPtr libraw) : base(libraw) { }

		protected override void LoadImage()
		{
			if ((IntPtr)Image == IntPtr.Zero)
			{
				var error = Raw.LibRaw.Error.Success;
				Image = Raw.LibRaw.MakeMemoryThumbnail(LibRaw, ref error);
				if (error != Raw.LibRaw.Error.Success)
					throw new RawImageException<LibRaw.Error>(error);
			}
		}
	}
}
