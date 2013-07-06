using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    class LastLayer : Layer
    {
        public LastLayer()
        {
            Fields.Add(FieldTypes.TRIS, FieldFactory.GetField(FieldTypes.TRIS));
            Fields.Add(FieldTypes.SKALA, FieldFactory.GetField(FieldTypes.SKALA));
            Fields.Add(FieldTypes.FULL, FieldFactory.GetField(FieldTypes.FULL));
            Fields.Add(FieldTypes.POKER, FieldFactory.GetField(FieldTypes.POKER));
            Fields.Add(FieldTypes.YAMB, FieldFactory.GetField(FieldTypes.YAMB));
        }

        public override int GetLayerPoints()
        {
            int layerPoints = 0;
            foreach (Field field in Fields.Values)
            {
                if (field.IsFilled)
                {
                    layerPoints += field.Value;
                }
            }

            return layerPoints;
        }
    }
}
