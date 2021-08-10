using System;

namespace FileOnQ.Imaging.Raw
{
	/// <summary>
	/// Raw image data used during the `Process()` API
	/// of a <see cref="IImageProcessor"/> implementation.
	/// </summary>
	/// <remarks>
	/// This is a ref struct and can only live on the stack
	/// and never the heap. This is by design as it uses
	/// native memory pointers.
	/// </remarks>
	public ref struct RawImageData
	{
		/// <summary>
		/// Gets or sets the pointer to the LibRaw data structure.
		/// </summary>
		public IntPtr LibRawData { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="ImageType"/>.
		/// </summary>
		public ImageType ImageType { get; set; }
		
		/// <summary>
		/// Gets or sets the total number of bits per color.
		/// </summary>
		public int Bits { get; set; }
		
		/// <summary>
		/// Gets or sets the total number of colors per pixel.
		/// </summary>
		public int Colors { get; set; }
		
		/// <summary>
		/// Gets or sets the image height in pixels.
		/// </summary>
		public int Height { get; set; }
		
		/// <summary>
		/// Gets or sets the image width in pixels.
		/// </summary>
		public int Width { get; set; }
		
		/// <summary>
		/// Gets or sets the image buffer.
		/// </summary>
		public ReadOnlySpan<byte> Buffer { get; set; }
	}
}
