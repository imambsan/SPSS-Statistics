# SPSS-Statistics

SPSS-Statistics merupakaN aplikasi yang dapat menghitung data-data secara sekuensial dan komputasi paralel. Aplikasi ini dibuat dengan bahasa pemrograman C# dan tersedia banyak fungsi-fungsi statistik sederhana dan komplek yang terdiri dari :

Perhitungan modus parallel menggunakan [System.Threading.Tasks.Parallel class](https://msdn.microsoft.com/en-us/library/system.threading.tasks.parallel%28v=vs.100%29.aspx) dengan mengimplementasikan konsep yang ada pada link berikut : https://msdn.microsoft.com/library/dd460693%28v=vs.100%29.aspx. Sedangkan untuk menggunakan packages Math.Net dapat dilihat dokumentasinya di link http://numerics.mathdotnet.com/DescriptiveStatistics.html. Berikut potongan kode program menggunakan Math.Net :
  > var statistic = new DescriptiveStatistics(b);
  var kurtosis = (float)statistic.Kurtosis;
  results.Add(kurtosis);

# Packages
Adapun library dan packages yang digunakan dalam pengembangan aplikasi ini terdiri dari :
  * Reogrid - .Net Spreadsheet Component v0.9.1.0
  * [Math.Net Numerics v3.4.0.0](http://mathnetnumerics.codeplex.com/releases/view/101319)
  * Json.Net v7.0.0
  * CUDAfy.Net v1.28.5482.15045
  * NET Framework 4.0
  
# Software & Hardware
1. Software
   - Windows 10 x64 bit
   - Visual Studio 2012
   - [CUDA Toolkit v7.0](https://developer.nvidia.com/cuda-downloads)
2. Hardware
   - Nvidia Graphic Card - Geforce GT520M
   
Versi Cudafy sangat berpengaruh dengan versi CUDA Toolkit. Lihat link [Codeplex ini](https://cudafy.codeplex.com/) untuk melihat ketentuannya. Supaya mendukung Cuda Toolkit, sebaiknya menginstall Microsoft Visual Studio dibawah 2015. 

# Demo Program
Berikut hasil kompilasi/berkas debug: [Download File](https://drive.google.com/file/d/0B1JElvplCywcV3hUdDlzME5LQ2c/view?usp=sharing)
