using Regression.Library.Models;

namespace Regression.Library.Managers
{
    internal class MatrixManager
    {        
        internal double[] EvaluateSlopes(MatrixModel matrix)
        {
            int numRows = matrix.X.GetLength(0);
            int numCols = matrix.X.GetLength(1);

            // Add a column of 1s to the matrix for the intercept
            double[,] XWithIntercept = AddInterceptColumn(matrix.X, numRows, numCols);

            // Transpose of X
            double[,] XTranspose = TransposeMatrix(XWithIntercept, numRows, numCols + 1);

            // XTranspose times XWithIntercept
            double[,] product1 = MultiplyMatrices(XTranspose, XWithIntercept, numCols + 1, numRows, numCols + 1);

            // Inverse of Matrix
            double[,] inverseProduct1 = InvertMatrix(product1, numCols + 1);

            // XTranspose times Y
            double[] product2 = MultiplyMatrixVector(XTranspose, matrix.Y, numCols + 1, numRows);

            // Multiply inverseProduct1 by product2
            double[] slopes = MultiplyMatrixVector(inverseProduct1, product2, numCols + 1, numCols + 1);

            return slopes;
        }

        private double[,] AddInterceptColumn(double[,] matrix, int numRows, int numCols)
        {
            double[,] result = new double[numRows, numCols + 1];
            for (int i = 0; i < numRows; i++)
            {
                result[i, 0] = 1;
                for (int j = 0; j < numCols; j++)
                {
                    result[i, j + 1] = matrix[i, j];
                }
            }
            return result;
        }

        private double[,] TransposeMatrix(double[,] matrix, int numRows, int numCols)
        {
            double[,] result = new double[numCols, numRows];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }

        private double[,] MultiplyMatrices(double[,] matrix1, double[,] matrix2, int numRows1, int numCols1, int numCols2)
        {
            double[,] result = new double[numRows1, numCols2];
            for (int i = 0; i < numRows1; i++)
            {
                for (int j = 0; j < numCols2; j++)
                {
                    for (int k = 0; k < numCols1; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return result;
        }

        private double[] MultiplyMatrixVector(double[,] matrix, double[] vector, int numRows, int numCols)
        {
            double[] result = new double[numRows];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }
            return result;
        }

        private double[,] InvertMatrix(double[,] matrix, int size)
        {
            double[,] result = new double[size, size * 2];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = matrix[i, j];
                    result[i, j + size] = i == j ? 1 : 0;
                }
            }

            // Gaussian elimination
            for (int i = 0; i < size; i++)
            {
                // Pivot for the column
                double pivot = result[i, i];
                if (pivot == 0)
                {
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                }

                for (int j = 0; j < size * 2; j++)
                {
                    result[i, j] /= pivot;
                }

                // Eliminate other rows
                for (int k = 0; k < size; k++)
                {
                    if (k != i)
                    {
                        double factor = result[k, i];
                        for (int j = 0; j < size * 2; j++)
                        {
                            result[k, j] -= factor * result[i, j];
                        }
                    }
                }
            }

            // Extract the inverse matrix
            double[,] inverse = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    inverse[i, j] = result[i, j + size];
                }
            }

            return inverse;
        }
    }
}
