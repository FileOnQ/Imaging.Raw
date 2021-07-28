
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>

#include "ImagingGpu.h"

cudaError_t proccessBitmapWithCuda(unsigned char* bitmap, unsigned char* data, unsigned int bitmapSize, unsigned int size, unsigned int width, unsigned height, int* error);

//https://developer.nvidia.com/blog/even-easier-introduction-cuda/

__global__ void process_bitmap_kernel(unsigned char* bitmap, unsigned char* data, int pixels, int imageStride, int offset)
{
	int index = blockIdx.x * blockDim.x + threadIdx.x;
	int gpuStride = blockDim.x * gridDim.x;

	int position = index * 3;

	int row = position / imageStride;
	int bitmapPosition = (index * 3) + (row * offset);

	for (int i = index; i < pixels; i+= gpuStride)
	{
		if (position > 0 && position % imageStride == 0)
		{
			for (int j = 0; j < offset; j++)
			{
				bitmap[bitmapPosition] = 0;
				bitmapPosition++;
			}
		}

		bitmap[bitmapPosition + 2] = data[position];
		bitmap[bitmapPosition + 1] = data[position + 1];
		bitmap[bitmapPosition] = data[position + 2];
		
		position += 3;
		bitmapPosition += 3;
	}
}

// used for debugging, we should move this into a standalone console app
//int main()
//{
//	unsigned char* data = new unsigned char[9];
//	data[0] = 1;
//	data[1] = 2;
//	data[2] = 3;
//	data[3] = 4;
//	data[4] = 5;
//	data[5] = 6;
//	data[6] = 7;
//	data[7] = 8;
//	data[8] = 9;
//
//	unsigned char* bitmap = process_bitmap(data, 9);
//
//	/*int* a = new int[2];
//	int* b = new int[2];
//	int* c = new int[2];
//	a[0] = 5;
//	b[0] = 3;
//	a[1] = 2;
//	b[1] = 2;
//
//	addWithCuda(c, a, b, 2);*/
//	return 0;
//}

unsigned char* process_bitmap(unsigned char* data, int size, int width, int height, int* error)
{
	int offset = height * (width % 4);
	int bitmapSize = size + offset;
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