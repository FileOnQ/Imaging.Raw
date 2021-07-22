namespace FileOnQ.Imaging.Raw
{
	public partial class LibRaw
	{
		public enum LibRawError
        {
        	Success = 0,
        	UnspecifiedError = -1,
        	FileUnsupported = -2,
        	RequestForNonExistentImage = -3,
        	OutOfOrderCall = -4,
        	NoThumbnail = -5,
        	UnsupportedThumbnail = -6,
        	InputClosed = -7,
        	NotImplemented = -8,
        	InsufficientMemory = -100007,
        	DataError = -100008,
        	IOError = -100009,
        	CancelledByCallback = -100010,
        	BadCrop = -100011,
        	TooBig = -100012,
        	MemoryPoolOverflow = -100013
        }
	}
}