using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class NumberField : Field
    {
        private int _numberOfTheField;

        public NumberField(int inNumberOfTheField)
        {
            _numberOfTheField = inNumberOfTheField;
        }

        public override void SetValue(int[] diceNumbers)
        {
            foreach (int number in diceNumbers)
            {
                if (number == _numberOfTheField)
                {
                    Value += number;
                }
            }

            IsFilled = true;
        }
    }
}
