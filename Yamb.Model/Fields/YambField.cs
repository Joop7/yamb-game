using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class YambField : MoreOfAKindField
    {
        protected override int BONUS_POINTS { get; set; }
        protected override int NUMBER_OF_REQUIRED_DICES { get; set; }

        public YambField()
        {
            BONUS_POINTS = 50;
            NUMBER_OF_REQUIRED_DICES = 5;
        }
    }
}
