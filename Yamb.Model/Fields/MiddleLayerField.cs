using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class MiddleLayerField : Field
    {
        public override void SetValue(int[] diceNumbers)
        {
            foreach (int number in diceNumbers)
            {
                Value += number;
            }

            IsFilled = true;
        }
    }
}
