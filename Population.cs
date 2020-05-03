using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EigthQueens
{
    public class Population
    {
        //Configuration Values
        double MutationProbability { get; set; }
        int PopulationSize { get; set; }
        int MaxEval { get; set; }
        int MaxGenerations { get; set; }
        int DValue { get; set; }
        double MinorRange { get; set; }
        double MaximumRange { get; set; }

        //Count Values
        public int CurrentEvaluation { get; set; }

        //Storage Structures
        List<Subject> Subjects { get; set; }
        public List<GenerationData> Generations { get; set; }

        //Miscelanous Values
        static readonly Random random = new Random();

        public Population(int populationSize, int dValue, double minorRange, double maxRange, int maxEval, double mutationProbability, int maxGenerations)
        {
            PopulationSize = populationSize;
            MaxEval = maxEval;
            MutationProbability = mutationProbability;
            DValue = dValue;
            MinorRange = minorRange;
            MaximumRange = maxRange;
            MaxGenerations = maxGenerations;

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

        Subject GetRandomParent()
        {
            return Subjects[random.Next(0, PopulationSize)];
        }

        //Subject GetChild(Subject ParentA, Subject ParentB)
        //{
        //    CurrentEvaluation++;

        //}
        void ReplaceWorst(Subject ChildA, Subject ChildB)
        {
            Subjects.Remove(Subjects.First());
            Subjects.Remove(Subjects.First());

            ChildA.CalculateFitnessValue();
            ChildB.CalculateFitnessValue();
            Subjects.Add(ChildA);
            Subjects.Add(ChildB);
            OrderList();
        }

        //void Reproduction()
        //{
        //    List<Subject> parents = new List<Subject>();

        //    if (random.NextDouble() <= MutationProbability)
        //    {
        //        childA = Mutate(childA);
        //    }

        //    if (random.NextDouble() <= MutationProbability)
        //    {
        //        childB = Mutate(childB);
        //    }

        //    ReplaceWorst(childA, childB);
        //}

        //Subject Mutate(Subject Child)
        //{
        //    int changeValueA = random.Next(0, BoardSize); 
        //    int changeValueB = random.Next(0, BoardSize);
        //    int tempValue = Child.Board[changeValueA];
        //    while (changeValueA == changeValueB)
        //    {
        //        changeValueB = random.Next(0, BoardSize);
        //    }
        //    Child.Board[changeValueA] = Child.Board[changeValueB];
        //    Child.Board[changeValueB] = tempValue;

        //    return Child;
        //}

        public Subject ObtainBestSubject()
        {
            Subject subject = Generations.Last().BetterSubject;
            return subject;
        }

        public void StartEvolutionProcess()
        {
            int generationCont = 0;
            while (CurrentEvaluation < MaxEval || generationCont < MaxGenerations)
            {
                GenerationData generation = new GenerationData(generationCont + 1, Subjects);
                Generations.Add(generation);
                generationCont++;

            }
        }
    }
}
