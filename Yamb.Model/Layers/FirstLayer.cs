using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class FirstLayer : Layer
    {
        private const int FIRST_LAYER_SIZE = 6;
        private const int BONUS_POINTS = 30;
        private const int BONUS_POINTS_CONDITION = 60;

        public FirstLayer()
        {
            for (int index = 1; index <= FIRST_LAYER_SIZE; index++)
            {
                Fields.Add((FieldTypes)index, FieldFactory.GetField((FieldTypes)index));
            }
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

            if (layerPoints >= BONUS_POINTS_CONDITION)
            {
                layerPoints += BONUS_POINTS;
            }

            return layerPoints;
        }
    }
}
