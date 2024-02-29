using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearRegressionWithoutLib.Helpers;

namespace LinearRegressionWithoutLib.Models
{
    public class TrainingModel
    {
        public List<TrainingDataModel> TrainingData { get; set; }


        public List<string> SelectedFeatures = new List<string>()
        {
            FeatureConstant.X1,
            FeatureConstant.X2
        };
    }
}
