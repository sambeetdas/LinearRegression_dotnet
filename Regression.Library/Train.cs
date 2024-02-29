using Regression.Library.Managers;
using Regression.Library.Models;

namespace Regression.Library
{
    public class Train
    {
        public async Task<EvaluationModel> Fit(MatrixModel matrix)
        {
            MatrixManager matrixManager = new MatrixManager();
            EvaluationManager evaluationManager = new EvaluationManager();
            EvaluationModel evaluationModel = new EvaluationModel();

            double[] slopes = matrixManager.EvaluateSlopes(matrix);

            evaluationModel.Equation = await evaluationManager.GenerateEquation(slopes);

            evaluationModel.RSquare = await evaluationManager.CalculateRSquared(matrix, slopes);

            return evaluationModel;
        }
    }
}
