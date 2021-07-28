#pragma once

#ifdef IMAGING_RAW_API
#define IMAGING_RAW_API __declspec(dllexport)
#else
#define IMAGING_RAW_API __declspec(dllimport)
#endif

extern "C" IMAGING_RAW_API unsigned char* process_bitmap(unsigned char* data, int size, int width, int height, int* errorCode);