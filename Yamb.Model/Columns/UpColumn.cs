using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class UpColumn : Column
    {
        private FieldTypes _fieldPointer;

        public UpColumn()
        {
            _fieldPointer = FieldTypes.YAMB;
        }

        public override void InputValues(LayerTypes layer, FieldTypes field, int[] diceNumbers)
        {
            if (field == _fieldPointer && !IsColumnFull())
            {
                Layers[layer].InputValues(field, diceNumbers);

                if (field == FieldTypes.ONE)
                {/*ako je unos jedinica, upisuju se još i u pomoćni spremnik
                  * središnjeg sloja stupca, jer je potrebno kod računanja bodova*/
                    
                    Layers[LayerTypes.MIDDLE].InputValues(field, diceNumbers);
                }

                if (_fieldPointer != FieldTypes.ONE)
                {/*ako nije došlo do kraja stupca pomjeri pokazivač na sljedeće polje*/
                    
                    int newFieldPointer = (int)_fieldPointer - 1;
                    _fieldPointer = (FieldTypes)newFieldPointer;
                }
            }
            else
            {
                throw new InaccessibleFieldException("Illegal field is selected or the column is already filled.");
            }
        }
    }
}
