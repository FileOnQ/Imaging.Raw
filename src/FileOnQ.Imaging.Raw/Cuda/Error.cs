namespace FileOnQ.Imaging.Raw
{
	internal partial class Cuda
	{
		internal enum Error
		{
			Success = 0,
			NoCudaCapbleGpu = -1,
			UnableAllocateGpuBuffers = -2,
			UnableToCopyHostMemoryToDeviceMemory = -3,
			UnableToLaunchKernel = -4,
			ErrorInKernel = -5,
			UnableToCopyDeviceMemoryToHostMemory = -6
		}
	}
}
