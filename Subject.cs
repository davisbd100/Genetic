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
        public int BrokenRules { get; set; }


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

        //f ( x ) = 〖(x_1  - 10)〗^2  + 5〖(x_2  - 12)〗^2  + x_3^4  + 3〖(x_4  - 11)〗^2  + 10x_5^6 + 7x_6^2  + x_7^4  - 4x_6 x_7  - 10x_6 - 8x_7

        void SecondFunction()
        {
            double firstValue = Math.Pow(SeriesValues[0] - 10, 2); //〖(x_1  - 10)〗^2
            double secondValue = (5 * Math.Pow(SeriesValues[1] - 12, 2)); //5〖(x_2  - 12)〗^2
            double thirdValue = Math.Pow(SeriesValues[2], 4); //x_3^4
            double fourthValue = (3 * Math.Pow(SeriesValues[3] - 11, 2)); //3〖(x_4  - 11)〗^2
            double fifthValue = (10 * Math.Pow(SeriesValues[4], 6)); //10x_5^6
            double sixthValue = (7 * Math.Pow(SeriesValues[5], 2)); //7x_6^2
            double seventhValue = Math.Pow(SeriesValues[6], 4); //x_7^4
            double eighthValue = (4 * SeriesValues[5] * SeriesValues[6]); //4x_6 x_7
            double ninthValue = (10 * SeriesValues[5]); //10x_6
            double tenthValue = (8 * SeriesValues[6]); //8x_7

            FitnessValue = firstValue + secondValue + thirdValue + fourthValue + fifthValue + sixthValue + seventhValue - eighthValue - ninthValue - tenthValue;
            CountBrokenRules();

        }


        void CountBrokenRules()
        {
            if (-127 + (2* Math.Pow(SeriesValues[0], 2)) + (3 * Math.Pow(SeriesValues[1], 4)) + (SeriesValues[2]) + (4 * Math.Pow(SeriesValues[3], 2)) + (5 * SeriesValues[4]) <= 0)
            {
                BrokenRules++;
            }
            if (-282 + (7 * SeriesValues[0]) + (3 * SeriesValues[1]) + (10 * Math.Pow(SeriesValues[2], 2)) + (SeriesValues[3]) - (SeriesValues[4]) <= 0)
            {
                BrokenRules++;
            }
            if (-196 + (23 * SeriesValues[0]) + (Math.Pow(SeriesValues[1], 2)) + (6 * Math.Pow(SeriesValues[5], 2)) - (8 * SeriesValues[6]) <= 0)
            {
                BrokenRules++;
            }
            if ((4 * Math.Pow(SeriesValues[0], 2)) + (Math.Pow(SeriesValues[1], 2)) - (3 * SeriesValues[0] * SeriesValues[1]) + (2 * Math.Pow(SeriesValues[2], 2)) + (5 * SeriesValues[5]) - (11 * SeriesValues[6]) <= 0)
            {
                BrokenRules++;
            }
        }

        override
        public String ToString()
        {
            String array = "";
            for (int i = 0; i < SeriesValues.Count; i++)
            {
                array += "|" + SeriesValues[i];
            }

            if (FunctionNumber)
            {
                return FitnessValue.ToString() + " : " + array;
            }
            else
            {
                return FitnessValue.ToString() + " : " + array + " Reglas rotas: " + BrokenRules;
            }
            
        }
    }
}
