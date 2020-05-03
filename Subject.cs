using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EigthQueens
{
    public class Subject
    {
        public int[] Board { get; set; }
        public int FitnessValue { get; set; }
        int MaxValue { get; set; }


        public Subject(int size, int maxValue)
        {
            Board = new int[size];
            MaxValue = maxValue;
        }

        public void CalculateFitnessValue()
        {
			int collisions = 0;
			for (int i = 0; i < Board.Length; i++)
			{
				int actualRow = Board[i];
				for (int j = i + 1; j < Board.Length; j++)
				{
                    if (Math.Abs(j - i) == Math.Abs(Board[j] - actualRow))
                    {
                        collisions++;
                    }
				}
			}

			FitnessValue = MaxValue - collisions;
		}

        override
        public String ToString()
        {
            String array = "";
            for (int i = 0; i < Board.Length; i++)
            {
                array += "|" + Board[i];
            }
            return FitnessValue.ToString() + " : " + array;
        }
    }
}
