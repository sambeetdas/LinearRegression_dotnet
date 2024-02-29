using LinearRegressionWithoutLib;
using LinearRegressionWithoutLib.Helpers;
using LinearRegressionWithoutLib.Models;
using System;

LinearRegression lregression = new LinearRegression();

TrainingModel trainingModel = new TrainingModel();

trainingModel.TrainingData = Util.ReadCsvFile<TrainingDataModel>("C:\\data.csv");

await lregression.Execute(trainingModel);

Console.ReadKey();