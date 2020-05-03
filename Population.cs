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

        public Population(int populationSize, int dValue, double minorRange, double maxRange, int maxEval, double mutationProbability)
        {
            PopulationSize = populationSize;
            MaxEval = maxEval;
            MutationProbability = mutationProbability;
            DValue = dValue;
            MinorRange = minorRange;
            MaximumRange = maxRange;


            Subjects = new List<Subject>();
            Generations = new List<GenerationData>();
            GenerateRandomPopulation();
        }

        void GenerateRandomPopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Subject tempSubject = new Subject(BoardSize, MaxValue);
                tempSubject.Board = RandomGenerator();
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
        int[] RandomGenerator()
        {
            int[] result = new int[BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                result[i] = i;
            }
            result = result.OrderBy(x => random.Next()).ToArray();
            return result;
        }

        Subject GetRandomParent()
        {
            return Subjects[random.Next(0, PopulationSize)];
        }

        Subject GetChild(Subject ParentA, Subject ParentB)
        {
            Subject child = new Subject(BoardSize, MaxValue);
            for (int i = 0; i < BoardSize; i++)
            {
                if (i < 4)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        if (!child.Board.Contains(ParentA.Board[j]))
                        {
                            child.Board[i] = ParentA.Board[j];
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        if (!child.Board.Contains(ParentB.Board[j]))
                        {
                            child.Board[i] = ParentB.Board[j];
                            break;
                        }
                    }
                }
            }
            CurrentEvaluation++;
            return child;
        }
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

        void Reproduction()
        {
            List<Subject> parents = new List<Subject>();
            for (int i = 0; i < Parents; i++)
            {
                parents.Add(GetRandomParent());
            }
            parents = parents.OrderBy(a => a.FitnessValue).ToList();

            Subject childA = GetChild(parents[0], parents[1]);
            Subject childB = GetChild(parents[1], parents[0]);
            if (random.NextDouble() <= MutationProbability)
            {
                childA = Mutate(childA);
            }

            if (random.NextDouble() <= MutationProbability)
            {
                childB = Mutate(childB);
            }

            ReplaceWorst(childA, childB);
        }

        Subject Mutate(Subject Child)
        {
            int changeValueA = random.Next(0, BoardSize); 
            int changeValueB = random.Next(0, BoardSize);
            int tempValue = Child.Board[changeValueA];
            while (changeValueA == changeValueB)
            {
                changeValueB = random.Next(0, BoardSize);
            }
            Child.Board[changeValueA] = Child.Board[changeValueB];
            Child.Board[changeValueB] = tempValue;

            return Child;
        }

        public Subject ObtainBestSubject()
        {
            Subject subject = Generations.Last().BetterSubject;
            return subject;
        }

        public void StartEvolutionProcess()
        {
            int generationCont = 0;
            while (CurrentEvaluation < MaxEval)
            {
                GenerationData generation = new GenerationData(generationCont + 1, Subjects);
                Generations.Add(generation);
                generationCont++;
                if (generation.BetterSubject.FitnessValue == MaxValue)
                {
                    Console.WriteLine("Found");
                    break;
                }

                Reproduction();
            }
        }
    }
}
