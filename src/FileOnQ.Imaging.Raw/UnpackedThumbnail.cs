using System;

namespace FileOnQ.Imaging.Raw
{
	/// <inheritdoc cref="IUnpackedImage"/>
	unsafe class UnpackedThumbnail : UnpackedImage
	{
		/// <summary>
		/// Instantiates the default instance of
		/// <see cref="UnpackedThumbnail"/>.
		/// </summary>
		/// <param name="libraw">
		/// The pointer to the LibRaw data structure.
		/// </param>
		public UnpackedThumbnail(IntPtr libraw) : base(libraw) { }

		/// <inheritdoc cref="UnpackedImage"/>
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
