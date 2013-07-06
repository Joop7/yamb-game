using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class MiddleLayer : Layer
    {
        public MiddleLayer()
        {
            Fields.Add(FieldTypes.ONE, FieldFactory.GetField(FieldTypes.ONE));
            Fields.Add(FieldTypes.MAX, FieldFactory.GetField(FieldTypes.MAX));
            Fields.Add(FieldTypes.MIN, FieldFactory.GetField(FieldTypes.MIN));
        }

        public override int GetLayerPoints()
        {
            int layerPoints = 0;
            if (Fields[FieldTypes.ONE].IsFilled && Fields[FieldTypes.MAX].IsFilled && Fields[FieldTypes.MIN].IsFilled)
            {
                int numberOfOnes = Fields[FieldTypes.ONE].Value;
                int maximum = Fields[FieldTypes.MAX].Value;
                int minimum = Fields[FieldTypes.MIN].Value;

                layerPoints = (maximum - minimum) * numberOfOnes;
            }

            return layerPoints;
        }
    }
}
