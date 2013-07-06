using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    [Serializable]
    public abstract class Column
    {
        public Dictionary<LayerTypes, Layer> Layers { get; protected set; }

        public Column()
        {
            Layers = new Dictionary<LayerTypes, Layer>();
            Layers.Add(LayerTypes.FIRST, LayerFactory.GetLayer(LayerTypes.FIRST));
            Layers.Add(LayerTypes.MIDDLE, LayerFactory.GetLayer(LayerTypes.MIDDLE));
            Layers.Add(LayerTypes.LAST, LayerFactory.GetLayer(LayerTypes.LAST));
        }

        public int GetLayerPoints(LayerTypes layer)
        {
            return Layers[layer].GetLayerPoints();
        }

        public int GetFieldValue(LayerTypes layer, FieldTypes field)
        {
            return Layers[layer].GetFieldValue(field);
        }

        public abstract void InputValues(LayerTypes layer, FieldTypes field, int[] diceNumbers);

        public bool IsColumnFull()
        {
            foreach (KeyValuePair<LayerTypes, Layer> layer in Layers)
            {
                if (!layer.Value.IsLayerFull())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
