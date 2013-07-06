using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class FreeColumn : Column
    {
        public override void InputValues(LayerTypes layer, FieldTypes field, int[] diceNumbers)
        {
            Layers[layer].InputValues(field, diceNumbers);

            if (field == FieldTypes.ONE)
            {
                Layers[LayerTypes.MIDDLE].InputValues(field, diceNumbers);
            }
        }
    }
}
