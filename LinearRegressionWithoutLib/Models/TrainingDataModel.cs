using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionWithoutLib.Models
{
    public class TrainingDataModel
    {
        public DateTime Month { get; set; }
        public double HDDWithBaseTemp { get; set; }
        public double CDDWithBaseTemp { get; set; }
        public double EnergyConsumptionkWh { get; set; }
        public double NumberOfDayPerMnt { get; set; }
        public double HDDPerDay { get; set; }
        public double CDDPerDay { get; set; }
        public double kWhPerDay { get; set; }
    }
}
