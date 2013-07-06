using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public abstract class Field
    {
        public int Value { get; protected set; }
        public bool IsFilled { get; protected set; }

        public Field()
        {
            Value = 0;
            IsFilled = false;
        }

        public abstract void SetValue(int[] inCubeNumbers);
    }
}
