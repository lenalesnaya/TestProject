using System.Threading.Tasks;

namespace TestProject.Model
{
    public static class MatrixCalculations
    {
        public static double[,]? Multiply(double[,] firstMatrix, double[,] secondMatrix)
        {
            if (firstMatrix == null || firstMatrix.Length < 1 || secondMatrix == null || secondMatrix.Length < 1)
                return null;

            if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
                return null;

            var result = new double[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];

            Parallel.For(0, firstMatrix.GetLength(0), i =>
            {
                for (int j = 0; j < secondMatrix.GetLength(1); ++j)
                    for (int k = 0; k < firstMatrix.GetLength(1); ++k)
                        result[i,j] += firstMatrix[i,k] * secondMatrix[k,j];
            });

            return result;
        }

        public static int[,]? Multiply(int[,] firstMatrix, int[,] secondMatrix)
        {
            if (firstMatrix == null || firstMatrix.Length < 1 || secondMatrix == null || secondMatrix.Length < 1)
                return null;

            if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
                return null;

            var result = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];

            Parallel.For(0, firstMatrix.GetLength(0), i =>
            {
                for (int j = 0; j < secondMatrix.GetLength(1); ++j)
                    for (int k = 0; k < firstMatrix.GetLength(1); ++k)
                        result[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
            });

            return result;
        }
    }
}