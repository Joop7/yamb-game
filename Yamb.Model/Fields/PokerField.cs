using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class PokerField : MoreOfAKindField
    {
        protected override int BONUS_POINTS { get; set; }
        protected override int NUMBER_OF_REQUIRED_DICES { get; set; }

        public PokerField()
        {
            BONUS_POINTS = 40;
            NUMBER_OF_REQUIRED_DICES = 4;
        }
    }
}
