
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>

#include "ImagingGpu.h"

cudaError_t proccessBitmapWithCuda(unsigned char* bitmap, unsigned char* data, unsigned int bitmapSize, unsigned int size, unsigned int width, unsigned height, int* error);

//https://developer.nvidia.com/blog/even-easier-introduction-cuda/

__global__ void process_bitmap_kernel(unsigned char* bitmap, unsigned char* data, int pixels, int imageStride, int offset)
{
	// This just works, do not touch
	int index = blockIdx.x * blockDim.x + threadIdx.x;
	int gpuStride = blockDim.x * gridDim.x;

	// As we iterate through pixel space which is different than buffer space (3 bytes for 1 pixel).
	// We need to ensure when i == number of pixels we set the last offset items in the bitmap to 0
	for (int i = index; i <= pixels; i+= gpuStride)
	{
		// Calculate the input pointer position
		int position = index * 3;
	
		// Calculate the row on the output bitmap pointer
		// Consider
		//  position = 20226
		//  imageStride = 20226
		//  20226 / 20226 = 1, but we want it to equal 0 or we will be off by 1
		// We need to subtract 1 to ensure we are on the correct row
		int row = (position - 1) / imageStride;
	
		// calculate the position of the output bitmap pointer
		int bitmapPosition = position + (row * offset);

		// At the end of every row, calcuated by the input pointer,
		// we need to set the offset to 0 or empty data.
		if (position > 0 && position % imageStride == 0)
		{
			for (int j = 0; j < offset; j++)
			{
				bitmap[bitmapPosition] =  0;

				// Update the bitmap position as we added 2 bytes per row
				bitmapPosition++;
			}
		}

		// Converting a PPM bitmap to a BMP bitmap flips
		// the red and blue value
		bitmap[bitmapPosition + 2] = data[position];
		bitmap[bitmapPosition + 1] = data[position + 1];
		bitmap[bitmapPosition] = data[position + 2];
	}
}

unsigned char* process_bitmap(unsigned char* data, int size, int width, int height, int* length, int* error)
{
	int offset = height * (width % 4);
	int bitmapSize = size + offset;
	*length = bitmapSize;
	unsigned char* bitmap = new unsigned char[bitmapSize];

	cudaError_t cudaStatus = proccessBitmapWithCuda(bitmap, data, bitmapSize, size, width, height, error);
	if (cudaStatus != cudaSuccess) {
		bitmap[0] = 1;
		fprintf(stderr, cudaGetErrorString(cudaStatus));
	}

	return bitmap;
}

// Error codes
// -1 = No CUDA-Capable GPU
// -2 = Unable to allocate GPU buffers
// -3 = Unable to copy host memory to device memory
// -4 = Unable to launch CUDA kernel
// -5 = Error while running CUDA kernel
// -6 = Unable to copy device memory to host memory
cudaError_t proccessBitmapWithCuda(unsigned char* bitmap, unsigned char* data, unsigned int bitmapSize, unsigned int size, unsigned int width, unsigned int height, int* error)
{
	unsigned char* dev_bitmap = 0;
	unsigned char* dev_data = 0;
	cudaError_t cudaStatus;

	// Choose which GPU to run on, change this on a multi-GPU system.
	cudaStatus = cudaSetDevice(0);
	if (cudaStatus != cudaSuccess) {
		*error = -1;
		goto Error;
	}

	// Allocate GPU buffers for intpu data and output bitmap
	cudaStatus = cudaMalloc((void**)&dev_bitmap, bitmapSize * sizeof(unsigned char));
	if (cudaStatus != cudaSuccess) {
		*error = -2;
		goto Error;
	}

	cudaStatus = cudaMalloc((void**)&dev_data, size * sizeof(unsigned char));
	if (cudaStatus != cudaSuccess) {
		*error = -2;
		goto Error;
	}

	// Copy input image from host memory to GPU buffers.
	cudaStatus = cudaMemcpy(dev_bitmap, bitmap, bitmapSize * sizeof(unsigned char), cudaMemcpyHostToDevice);
	if (cudaStatus != cudaSuccess) {
		*error = -3;
		goto Error;
	}

	cudaStatus = cudaMemcpy(dev_data, data, size * sizeof(unsigned char), cudaMemcpyHostToDevice);
	if (cudaStatus != cudaSuccess) {
		*error = -3;
		goto Error;
	}

	// TODO - determine best block size and thread count. passing size is too large of a value

	
	int pixels = size / 3;
	int blockSize = 256;
	int numberOfBlocks = (pixels + blockSize - 1) / blockSize;

	int offset = width % 4;
	int stride = width * 3;
	
	// Launch a kernel on the GPU with one thread for each element.
	process_bitmap_kernel<<<numberOfBlocks, blockSize>>>(dev_bitmap, dev_data, pixels, stride, offset);

	// Check for any errors launching the kernel
	cudaStatus = cudaGetLastError();
	if (cudaStatus != cudaSuccess) {
		*error = -4;
		fprintf(stderr, "addKernel launch failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}

	// cudaDeviceSynchronize waits for the kernel to finish, and returns
	// any errors encountered during the launch.
	cudaStatus = cudaDeviceSynchronize();
	if (cudaStatus != cudaSuccess) {
		*error = -5;
		goto Error;
	}

	// Copy output vector from GPU buffer to host memory.
	cudaStatus = cudaMemcpy(bitmap, dev_bitmap, bitmapSize * sizeof(unsigned char), cudaMemcpyDeviceToHost);
	if (cudaStatus != cudaSuccess) {
		*error = -6;
		goto Error;
	}

Error:
	cudaFree(dev_bitmap);
	cudaFree(dev_data);

	return cudaStatus;
}

bool is_cuda_capable()
{
	int c = 0;
	int* count = &c;
	cudaError_t status = cudaGetDeviceCount(count);
	if (status != cudaSuccess)
		return false;
	
	return *count > 0;
}