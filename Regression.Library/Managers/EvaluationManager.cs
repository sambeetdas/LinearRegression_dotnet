using Regression.Library.Models;
using System.Text;

namespace Regression.Library.Managers
{
    internal class EvaluationManager
    {
        internal async Task<double> CalculateRSquared(MatrixModel matrix, double[] slopes)
        {
            double[] YPredicted = Predict(matrix.X, slopes);

            return await CalculateRSquared(matrix.Y, YPredicted);
        }

        private double[] Predict(double[,] X, double[] slopes)
        {
            int numRows = X.GetLength(0);
            int numCols = X.GetLength(1);

            double[] predictions = new double[numRows];
            for (int i = 0; i < numRows; i++)
            {
                double sum = slopes[0];
                for (int j = 1; j < numCols; j++)
                {
                    sum += slopes[j] * X[i, j];
                }
                predictions[i] = sum;
            }

            return predictions;
        }
        private double Mean(double[] values)
        {
            double sum = 0;
            foreach (double value in values)
            {
                sum += value;
            }
            return sum / values.Length;
        }

        private async Task<double> CalculateRSquared(double[] actualValues, double[] predictedValues)
        {
            if (actualValues.Length != predictedValues.Length || actualValues.Length < 2)
            {
                throw new ArgumentException("Input arrays must have the same length, and the length must be at least 2.");
            }

            double meanActual = actualValues.Average();
            double sumOfSquaresTotal = actualValues.Sum(value => Math.Pow(value - meanActual, 2));
            double sumOfSquaresResidual = actualValues.Zip(predictedValues, (actual, predicted) => Math.Pow(actual - predicted, 2)).Sum();

            // Calculate R-squared
            double rSquared = 1 - (sumOfSquaresResidual / sumOfSquaresTotal);

            return rSquared;
        }

        internal async Task<String> GenerateEquation(double[] slopes)
        {
            StringBuilder equationBuilder = new StringBuilder();
            equationBuilder.Append($"{Constant.Y} = ");

            for (int i = 0; i < slopes.Length; i++)
            {
                if (i == 0)
                {
                    equationBuilder.Append(slopes[i]);
                }
                else
                {
                    equationBuilder.Append($" + {slopes[i]}*{Constant.X}");
                    if (slopes.Length > 2)
                    {
                        equationBuilder.Append($"{i}");
                    }
                    
                }
            }

            return equationBuilder.ToString();
        }
    }
}
