using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EigthQueens
{
    public class Subject
    {
        public List<double> SeriesValues { get; set; }
        public double FitnessValue { get; set; }
        double DValue { get; set; }
        bool FunctionNumber { get; set; }
        int BrokenRules { get; set; }


        public Subject(double dValue, bool function)
        {
            SeriesValues = new List<double>();
            if (function)
            {
                DValue = dValue;
            }
            else
            {
                DValue = 7;
            }
            FunctionNumber = function;
            BrokenRules = 0;
        }

        public void CalculateFitnessValue()
        {
            if (!SeriesValues.Any())
            {
                Console.WriteLine("Empty list");
                return;
            }
            if (FunctionNumber)
            {
                FirstFunction();
            }
            else
            {
                SecondFunction();
            }
        }

        // f (x) = 10D + Σ to D from i=1(x²i − 10cos(2πxi))
        void FirstFunction()
        {
            double result;
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

        //f ( x ) = 〖(x_1  - 10)〗^2  + 5〖(x_2  - 12)〗^2  + x_3^4  + 3〖(x_4  - 11)〗^2  + 10x_5^6+ 7x_6^2  + x_7^4  - 4x_6 x_7  - 10x_6- 8x_7

        void SecondFunction()
        {

        }


        void CountBrokenRules()
        {

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
