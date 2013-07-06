using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public static class LayerFactory
    {
        public static Layer GetLayer(LayerTypes layerType)
        {
            switch (layerType)
            {
                case LayerTypes.FIRST:
                    return new FirstLayer();
                case LayerTypes.MIDDLE:
                    return new MiddleLayer();
                case LayerTypes.LAST:
                    return new LastLayer();
                default:
                    throw new YambException("");
            }
        }
    }
}
