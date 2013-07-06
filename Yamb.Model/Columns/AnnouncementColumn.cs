using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class AnnouncementColumn : Column
    {
        private bool _isAnnounced = false;
        private FieldTypes _announcedField;

        public override void InputValues(LayerTypes layer, FieldTypes field, int[] diceNumbers)
        {
            if (_isAnnounced)
            {
                if (field == _announcedField)
                {
                    Layers[layer].InputValues(field, diceNumbers);

                    if (field == FieldTypes.ONE)
                    {
                        Layers[LayerTypes.MIDDLE].InputValues(field, diceNumbers);
                    }
                    _isAnnounced = false;
                }
                else
                {
                    throw new InaccessibleFieldException("Posted unavailable field.");
                }
            }
            else if (Layers[layer].IsFieldFilled(field))
            {
                throw new FieldIsFilledException("The field is already filled.");
            }
            else
            {
                _isAnnounced = true;
                _announcedField = field;
            }
        }
    }
}
