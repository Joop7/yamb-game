using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class SkalaField : Field
    {
        private const int SMALLEST_POSSIBLE_NUMBER = 1;
        private const int BIGET_POSSIBLE_NUMBER = 6;
        private const int SMALL_POINTS = 35;
        private const int BIG_POINTS = 45;

        public override void SetValue(int[] diceNumbers)
        {
            List<int> skalaNumbers = new List<int>();
            bool isSkala = true;

            if (diceNumbers.Contains(SMALLEST_POSSIBLE_NUMBER) && diceNumbers.Contains(BIGET_POSSIBLE_NUMBER))
            {
                isSkala = false;
            }
            else
            {
                foreach (int number in diceNumbers)
                {
                    if (skalaNumbers.Contains(number))
                    {
                        isSkala = false;
                        break;
                    }
                    else
                    {
                        skalaNumbers.Add(number);
                    }
                }
            }

            if (isSkala)
            {
                Value = GetSkalaPoints(skalaNumbers);
            }

            IsFilled = true;
        }

        private int GetSkalaPoints(List<int> skalaNumbers)
        {
            if (skalaNumbers.Contains(SMALLEST_POSSIBLE_NUMBER))
            {
                return SMALL_POINTS;
            }
            else
            {
                return BIG_POINTS;
            }
        }
    }
}
