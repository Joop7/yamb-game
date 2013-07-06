using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public static class FieldFactory
    {
        public static Field GetField(FieldTypes fieldType)
        {
            switch (fieldType)
            {
                case FieldTypes.ONE:
                    return new NumberField((int)fieldType);
                case FieldTypes.TWO:
                    return new NumberField((int)fieldType);
                case FieldTypes.THREE:
                    return new NumberField((int)fieldType);
                case FieldTypes.FOUR:
                    return new NumberField((int)fieldType);
                case FieldTypes.FIVE:
                    return new NumberField((int)fieldType);
                case FieldTypes.SIX:
                    return new NumberField((int)fieldType);
                case FieldTypes.MIN:
                    return new MiddleLayerField();
                case FieldTypes.MAX:
                    return new MiddleLayerField();
                case FieldTypes.SKALA:
                    return new SkalaField();
                case FieldTypes.FULL:
                    return new FullField();
                default:
                    return MoreOfAKindFieldFactory.GetField(fieldType);
            }
        }
    }
}
