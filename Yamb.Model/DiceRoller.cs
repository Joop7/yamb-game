using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class DiceRoller
    {
        public int[] Dices { get; private set; }
        private Random _rand;

        public DiceRoller()
        {
            Dices = new int[Constants.NUMBER_OF_DICES];
            _rand = new Random();
        }

        public int[] RollDices(bool[] selectedDices)
        {
            int index = 0;

            foreach (bool roll in selectedDices)
            {
                if (roll)
                {
                    try
                    {
                        Dices[index] = _rand.Next(1, Constants.NUMBER_OF_DICE_SIDES + 1);
                    }
                    catch (NullReferenceException)
                    {
                        throw new YambException("Ne postoji toliko kockica.");
                    }
                }
                index++;
            }
            return Dices;
        }
    }
}
