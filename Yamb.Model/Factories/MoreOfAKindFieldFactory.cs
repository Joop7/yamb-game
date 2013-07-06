using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public static class MoreOfAKindFieldFactory
    {
        public static MoreOfAKindField GetField(FieldTypes fieldType)
        {
            switch (fieldType)
            {
                case FieldTypes.TRIS:
                    return new TrisField();
                case FieldTypes.POKER:
                    return new PokerField();
                case FieldTypes.YAMB:
                    return new YambField();
                default:
                    throw new NonExistingFieldException("");
            }
        }
    }
}
