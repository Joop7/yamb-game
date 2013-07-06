using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public abstract class Layer
    {
        public Dictionary<FieldTypes, Field> Fields { get; protected set; }

        public Layer()
        {
            Fields = new Dictionary<FieldTypes, Field>();
        }

        public abstract int GetLayerPoints();

        public int GetFieldValue(FieldTypes field)
        {
            if (IsFieldFilled(field))
            {
                return Fields[field].Value;
            }
            else
            {
                throw new FieldIsEmptyException("Polje je prazno.");
            }
        }

        public void InputValues(FieldTypes field, int[] diceNumbers)
        {
            if (IsFieldFilled(field))
            {
                throw new FieldIsFilledException("Polje je već ispunjeno.");
            }
            else
            {
                Fields[field].SetValue(diceNumbers);
            }
        }

        public bool IsFieldFilled(FieldTypes field)
        {
            return Fields[field].IsFilled;
        }

        public bool IsLayerFull()
        {
            foreach (KeyValuePair<FieldTypes, Field> field in Fields)
            {
                if (!field.Value.IsFilled)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
