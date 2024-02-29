using LinearRegressionWithoutLib.Models;
using Regression.Library;
using System.Diagnostics;

namespace LinearRegressionWithoutLib
{
    public class LinearRegression
    {
        public async Task Execute(TrainingModel trainingModel)
        {
            Train train = new Train();

            var matrix = Util.ToMatrix(trainingModel);

            Stopwatch st = new Stopwatch();
            st.Start();

            var result = await train.Fit(matrix);

            st.Stop();
           
            Console.WriteLine($"Linear Regression Equation: {Util.ReplaceFeature(trainingModel, result.Equation)}");

            Console.WriteLine($"R-square: {result.RSquare}");

            Console.WriteLine($"Processed time is {st.Elapsed.Seconds} Seconds");
        }
    }
}
