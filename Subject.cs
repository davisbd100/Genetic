using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EigthQueens
{
    public class Subject
    {
        public List<double> SeriesValues  { get; set; }
        public double FitnessValue { get; set; }
        double DValue { get; set; }


        public Subject(double dValue)
        {
            SeriesValues = new List<double>();
            DValue = dValue;
        }

        // f (x) = 10D + Σ to D from i=1(x²i − 10cos(2πxi))
        public void CalculateFitnessValue()
        {
            double result;
            if (!SeriesValues.Any())
            {
                Console.WriteLine("Empty list");
                return;
            }
            double summationAcum = 0;
            foreach (var item in SeriesValues)
            {
                double firstValue = 2 * Math.PI * item; //(2πxi)
                double secondValue = Math.Cos(firstValue); // cos(2πxi)
                double thirdValue = 10 * secondValue; // 10cos(2πxi)
                double fourthValue = Math.Pow(item, 2) - thirdValue; // (x²i − 10cos(2πxi)
                summationAcum += fourthValue;
            }
            result = (10 * DValue) - summationAcum; // 10D + Σ to D from i=1(x²i − 10cos(2πxi))

            FitnessValue = Math.Round(result, 3);
		}

        override
        public String ToString()
        {
            String array = "";
            for (int i = 0; i < SeriesValues.Count; i++)
            {
                array += "|" + SeriesValues[i];
            }
            return FitnessValue.ToString() + " : " + array;
        }
    }
}
