using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public abstract class MoreOfAKindField : Field
    {
        protected abstract int BONUS_POINTS {get; set;}
        protected abstract int NUMBER_OF_REQUIRED_DICES { get; set; }

        public override void SetValue(int[] diceNumbers)
        {
            int[] numberCounter = new int[Constants.NUMBER_OF_DICE_SIDES];
            int kindOfNumber = 0;

            foreach (int number in diceNumbers)
            {
                numberCounter[number - 1] += 1;
            }

            for (int index = 0; index < numberCounter.Count(); index++)
            {
                if (numberCounter[index] >= NUMBER_OF_REQUIRED_DICES && numberCounter[index] > kindOfNumber)
                {
                    kindOfNumber = index + 1;
                }
            }

            if (kindOfNumber != 0)
            {
                Value = (NUMBER_OF_REQUIRED_DICES * kindOfNumber) + BONUS_POINTS;
            }

            IsFilled = true;
        }
    }
}
