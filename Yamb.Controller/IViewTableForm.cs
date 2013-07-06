using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Yamb.Util;

namespace Yamb.Controller
{
    public interface IViewTableForm : IObserver
    {
        void DisplayMsg(Color txtColor, string msg);

        void DisplayGivenDices(int[] dices, bool[] selectedDices);

        int DecreasDisplayDiceThrow();

        void DisplayDiceSelection(int diceIndex, Color color);

        void AnnouncementDisplayActions(bool announced);

        void EndOfTurnActions();

        void GameFinished();

        void SimulateRollingDice(bool[] selectedDices);
    }
}
