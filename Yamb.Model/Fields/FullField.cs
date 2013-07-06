using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class FullField : Field
    {
        private const int BONUS_POINTS = 30;
        private const int PAIR = 2;
        private const int TRIS = 3;

        public override void SetValue(int[] diceNumbers)
        {
            int[] numberCounter = new int[Constants.NUMBER_OF_DICE_SIDES];
            int pairNumber = 0;
            int trisNumber = 0;

            bool hasPair = false;
            bool hasTris = false;

            foreach (int number in diceNumbers)
            {
                numberCounter[number - 1] += 1;
            }

            for (int index = 0; index < numberCounter.Count(); index++)
            {
                if (numberCounter[index] >= TRIS && trisNumber < index + 1)
                {
                    if (trisNumber > pairNumber)
                    {
                        pairNumber = index + 1;
                        hasPair = true;
                    }

                    trisNumber = index + 1;
                    hasTris = true;
                }
                else if (numberCounter[index] >= PAIR && pairNumber < index + 1)
                {
                    pairNumber = index + 1;
                    hasPair = true;
                }
            }

            if (hasPair && hasTris)
            {
                Value = (pairNumber * PAIR) + (trisNumber * TRIS) + BONUS_POINTS;
            }

            IsFilled = true;
        }
    }
}
