
// ParallelSPSS.Form1
extern "C" __global__  void addVector( float* a, int aLen0,  float* b, int bLen0,  float* c, int cLen0, int N);

// ParallelSPSS.Form1
extern "C" __global__  void addVector( float* a, int aLen0,  float* b, int bLen0,  float* c, int cLen0, int N)
{
	for (int i = threadIdx.x + blockIdx.x * blockDim.x; i < N; i += gridDim.x)
	{
		c[(i)] = a[(i)] + b[(i)];
	}
}
