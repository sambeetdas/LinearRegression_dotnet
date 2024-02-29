using LinearRegressionWithoutLib.Helpers;
using LinearRegressionWithoutLib.Models;
using Microsoft.VisualBasic.FileIO;
using Regression.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinearRegressionWithoutLib
{
    public static class Util
    {
        public static List<T> ReadCsvFile<T>(string filePath) where T : new()
        {
            List<T> data = new List<T>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Skip header if present
                if (!parser.EndOfData)
                {
                    parser.ReadFields();
                }

                while (!parser.EndOfData)
                {
                    // Read current line fields
                    string[] fields = parser.ReadFields();

                    if (fields != null)
                    {
                        T record = MapToModel<T>(fields);
                        data.Add(record);
                    }
                }
            }

            return data;
        }

        private static T MapToModel<T>(string[] fields) where T : new()
        {
            T record = new T();

            if (typeof(T) == typeof(TrainingDataModel))
            {
                TrainingDataModel myDataRecord = record as TrainingDataModel;

                if (DateTime.TryParse(fields[0], out DateTime Month))
                    myDataRecord.Month = Month;

                if (float.TryParse(fields[1], out float HDDWithBaseTemp))
                    myDataRecord.HDDWithBaseTemp = HDDWithBaseTemp;

                if (float.TryParse(fields[2], out float CDDWithBaseTemp))
                    myDataRecord.CDDWithBaseTemp = CDDWithBaseTemp;

                if (float.TryParse(fields[3], out float EnergyConsumptionkWh))
                    myDataRecord.EnergyConsumptionkWh = EnergyConsumptionkWh;

                if (float.TryParse(fields[4], out float NumberOfDayPerMnt))
                    myDataRecord.NumberOfDayPerMnt = NumberOfDayPerMnt;

                if (float.TryParse(fields[5], out float HDDPerDay))
                    myDataRecord.HDDPerDay = HDDPerDay;

                if (float.TryParse(fields[6], out float CDDPerDay))
                    myDataRecord.CDDPerDay = CDDPerDay;

                if (float.TryParse(fields[7], out float kWhPerDay))
                    myDataRecord.kWhPerDay = kWhPerDay;
            }

            return record;
        }

        public static object ExecuteObject<T>(T obj, string propertyName)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                object propertyValue = propertyInfo.GetValue(obj);

                return propertyValue;
            }
            return null;
        }

        public static MatrixModel ToMatrix(TrainingModel trainingModel)
        {
            MatrixModel matrix = new MatrixModel();
            try
            {
                int numRows = trainingModel.TrainingData.Count;
                int numCols = trainingModel.SelectedFeatures.Count;

                matrix.X = new double[numRows, numCols];
                matrix.Y = new double[numRows];

                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 0; j < trainingModel.SelectedFeatures.Count; j++)
                    {
                        matrix.X[i, j] = GetTrainingValue(trainingModel.TrainingData[i], trainingModel.SelectedFeatures[j]);
                    }

                    matrix.Y[i] = GetTrainingValue(trainingModel.TrainingData[i], FeatureConstant.Y);
                }
            }
            catch (Exception ex)
            {

            }
            return matrix;
        }

        private static double GetTrainingValue(TrainingDataModel obj, string key)
        {
            return Convert.ToDouble(Util.ExecuteObject(obj, key));
        }

        public static string ReplaceFeature(TrainingModel trainingModel, string equation)
        {
            FieldInfo[] fields = typeof(FeatureConstant).GetFields();


            foreach (var feature in trainingModel.SelectedFeatures)
            {
                var field = fields.FirstOrDefault(i => i.GetValue(null).ToString() == feature);
                if (field != null)
                {
                    if (trainingModel.SelectedFeatures.Count() > 1)
                        equation = equation.Replace(field.Name, feature);
                    else
                    {
                        string name = field.Name.Remove(field.Name.Length-1, 1);
                        equation = equation.Replace(name, feature);
                    }
                        
                }
                

            }


            return equation;
        }
    }
}
