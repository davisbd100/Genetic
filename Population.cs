using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EigthQueens
{
    public class Population
    {
        //Configuration Values
        double MutationProbability { get; set; }
        double AlphaValue { get; set; }
        int PopulationSize { get; set; }
        int MaxEval { get; set; }
        int MaxGenerations { get; set; }
        int DValue { get; set; }
        double MinorRange { get; set; }
        double MaximumRange { get; set; }

        //Count Values
        public int CurrentEvaluation { get; set; } = 0;
        public int CurrentGeneration { get; set; } = 1;

        //Storage Structures
        List<Subject> Subjects { get; set; }
        public List<GenerationData> Generations { get; set; }

        //Miscelanous Values
        static readonly Random random = new Random();

        public Population(int populationSize, int dValue, double minorRange, double maxRange, int maxEval, double mutationProbability, int maxGenerations, double alphaValue)
        {
            PopulationSize = populationSize;
            MaxEval = maxEval;
            MutationProbability = mutationProbability;
            DValue = dValue;
            MinorRange = minorRange;
            MaximumRange = maxRange;
            MaxGenerations = maxGenerations;
            AlphaValue = alphaValue;

            Subjects = new List<Subject>();
            Generations = new List<GenerationData>();
            GenerateRandomPopulation();
            Console.WriteLine();
        }

        void GenerateRandomPopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Subject tempSubject = new Subject(DValue);
                tempSubject.SeriesValues = RandomGenerator();
                tempSubject.CalculateFitnessValue();
                Subjects.Add(tempSubject);
                CurrentEvaluation++;
            }
            OrderList();
            CurrentEvaluation = PopulationSize;
        }

        void OrderList()
        {
            Subjects = Subjects.OrderBy(a => a.FitnessValue).ToList();
        }
        List<double> RandomGenerator()
        {
            List<double> result = new List<double>();
            for (int i = 0; i < DValue; i++)
            {
                double asignValue = random.NextDouble() * (MaximumRange - MinorRange) + MinorRange;
                asignValue = Math.Round(asignValue, 3);
                result.Add(asignValue);
            }

            return result;
        }

        List<Subject> SpinRouletteParent(int parentsNumber)
        {
            List<Subject> result = new List<Subject>();
            double upperLimit = SummAllSubjects();

            for (int i = 0; i < parentsNumber; i++)
            {
                double spinValue = Math.Round(random.NextDouble() * (upperLimit - 0.001) + 0.001, 3);
                double acumSumm = 0;
                int rouletteSelector = 0;
                int selectedValue = 0;
                while (acumSumm < spinValue)
                {
                    acumSumm += Subjects[rouletteSelector].FitnessValue;
                    if (rouletteSelector < Subjects.Count - 1)
                    {
                        selectedValue = rouletteSelector;
                        rouletteSelector++;
                    }
                    else
                    {
                        selectedValue = rouletteSelector;
                        rouletteSelector = 0;
                    }
                }
                result.Add(Subjects[selectedValue]);
            }

            return result;
        }
        double SummAllSubjects()
        {
            double result = 0;
            foreach (var item in Subjects)
            {
                result += item.FitnessValue;
            }
            return result;
        }

        void ReplaceWorst(Subject Child)
        {
            Subjects.Remove(Subjects.Last());
            Subjects.Add(Child);
            OrderList();
        }

        void Reproduction()
        {
            List<Subject> parents = SpinRouletteParent(2);

            List<Subject> childs = Crossover(parents[0], parents[1]);

            foreach (var item in childs)
            {
                Subject child = Mutate(item);

                ReplaceWorst(child);
            }
            
        }

        List<Subject> Crossover(Subject parentA, Subject parentB)
        {
            List<Subject> childs = new List<Subject>();
            Subject childA = new Subject(DValue);
            Subject childB = new Subject(DValue);

            for (int i = 0; i < parentA.SeriesValues.Count; i++)
            {
                double CMax;
                double CMin;
                if (parentA.SeriesValues[i] > parentB.SeriesValues[i])
                {
                    CMax = parentA.SeriesValues[i];
                    CMin = parentB.SeriesValues[i];
                }
                else
                {
                    CMax = parentB.SeriesValues[i];
                    CMin = parentA.SeriesValues[i];
                }
                double l = CMax - CMin;
                double MinValue = CMin - (l * AlphaValue);
                double MaxValue = CMax + (l * AlphaValue);

                if (MinValue < MinorRange)
                {
                    MinValue = MinorRange;
                }

                if (MaxValue > MaximumRange)
                {
                    MaxValue = MaximumRange;
                }

                childA.SeriesValues.Add(Math.Round(random.NextDouble() * (MaxValue - MinValue) + MinValue, 3));
                childB.SeriesValues.Add(Math.Round(random.NextDouble() * (MaxValue - MinValue) + MinValue, 3));
            }
            childs.Add(childA);
            childs.Add(childB);
            foreach (var item in childs)
            {
                item.CalculateFitnessValue();
                CurrentEvaluation++;
            }
            return childs;
        }

        Subject Mutate(Subject Child)
        {
            for (int i = 0; i < Child.SeriesValues.Count; i++)
            {
                if (random.NextDouble() <= MutationProbability)
                {
                    Child.SeriesValues[i] = Math.Round(random.NextDouble() * (MaximumRange - MinorRange) + MinorRange, 3); ;
                }
            }
            Child.CalculateFitnessValue();
            return Child;
        }

        public Subject ObtainBestSubject()
        {
            Subject subject = Subjects.First();
            return subject;
        }

        public void StartEvolutionProcess()
        {
            while (CurrentEvaluation < MaxEval && CurrentGeneration < MaxGenerations)
            {
                GenerationData generation = new GenerationData(CurrentGeneration, SummAllSubjects(), Subjects);
                Generations.Add(generation);
                CurrentGeneration++;

                Reproduction();
            }
        }
    }
}
