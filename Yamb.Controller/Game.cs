using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Yamb.Model;

namespace Yamb.Controller
{
    public class Game
    {
        private const int MAXIMUM_THROWS = 3;

        public string Player { get; private set; }

        private bool[] _selectedDices;
        private DiceRoller _diceRoller;

        private bool _announced;
        private bool _autoValueInput;
        private FieldTypes _announcedField;

        private DiceThrow _currentDiceThrow;
        private bool _turnEnded;

        private string[] _msg;

        public Game(string inPlayer)
        {
            _selectedDices = new bool[Constants.NUMBER_OF_DICES];
            _diceRoller = new DiceRoller();

            _currentDiceThrow = DiceThrow.FIRST;
            _turnEnded = false;

            Player = inPlayer;
            UnselectDice();

            _msg = new string[]
                {
                    "Throw the dice! Throw the dice! Throw the dice!",
                    "You threw three times. Enter the points!",
                    "You must announce entry!",
                    "Enter the points!!!",
                    "Roll dice first!!!",
                    "You've selected an improper field!!"
                };
        }

        private void UnselectDice()
        {
            for (int index = 0; index < Constants.NUMBER_OF_DICES; index++)
            {
                _selectedDices[index] = true;
            }
        }

        public void RollDices(IViewTableForm frmTableDisplay)
        {
            YambTable.GetInstance().AddObserver(frmTableDisplay);

            if (_turnEnded)
            {
                frmTableDisplay.DisplayMsg(Color.Purple, _msg[1]);
            }
            else if (OnlyAnnouncementLeft() && !_announced && _currentDiceThrow == DiceThrow.SECOND)
            {
                frmTableDisplay.DisplayMsg(Color.Red, _msg[2]);
            }
            else
            {
                frmTableDisplay.DisplayMsg(Color.ForestGreen, "");

                _diceRoller.RollDices(_selectedDices);

                frmTableDisplay.SimulateRollingDice(_selectedDices);
                frmTableDisplay.DisplayGivenDices(_diceRoller.Dices, _selectedDices);
               
                int throwsLeft = frmTableDisplay.DecreasDisplayDiceThrow();
                
                bool displayInputValueMsg = CheckForAutoInput(frmTableDisplay);

                if (throwsLeft == 0 && displayInputValueMsg)
                {
                    frmTableDisplay.DisplayMsg(Color.ForestGreen, _msg[3]);
                }
                else
                {
                    frmTableDisplay.DisplayMsg(Color.ForestGreen, _msg[0]);
                }
            }
        }
       
        #region help functions

        private bool CheckForAutoInput(IViewTableForm frmTableDisplay)
        {
            //Ako je najavljeno i bačeno je 3 puta, automatski se upiuje vrijednost
            DiceThrow diceThrow = IncreaseThrowCount();
            if (diceThrow == DiceThrow.FIRST && _autoValueInput)
            {
                EnterDiceValue(frmTableDisplay, null, ColumnTypes.ANNOUNCEMENT, GetFieldsLayer(_announcedField), _announcedField);
                return false;
            }

            return true;
        }

        private LayerTypes GetFieldsLayer(FieldTypes field)
        {
            int fieldNumberValue = (int)field;
            if (fieldNumberValue >= 1 && fieldNumberValue <= 6)
            {
                return LayerTypes.FIRST;
            }
            else if (fieldNumberValue == 7 || fieldNumberValue == 8)
            {
                return LayerTypes.MIDDLE;
            }
            else
            {
                return LayerTypes.LAST;
            }
        }

        #endregion

        public void SelectDice(IViewTableForm frmTableDisplay, int diceIndex)
        {
            if (_currentDiceThrow != DiceThrow.FIRST)
            {
                _selectedDices[diceIndex - 1] = !_selectedDices[diceIndex - 1];
                if (_selectedDices[diceIndex - 1])
                {
                    frmTableDisplay.DisplayDiceSelection(diceIndex, Color.Transparent);
                }
                else
                {
                    frmTableDisplay.DisplayDiceSelection(diceIndex, Color.SteelBlue);
                }
            }
        }

        public bool EnterDiceValue(IViewTableForm frmTableDisplay, IConfirmForm frmConfirm, ColumnTypes column, LayerTypes layer, FieldTypes field)
        {
            if (_currentDiceThrow == DiceThrow.FIRST && !_turnEnded)
            {
                frmTableDisplay.DisplayMsg(Color.Red, _msg[4]);
                return false;
            }
            else
            {
                if (ValidEntering(frmConfirm, column))
                {
                    try
                    {
                        YambTable.GetInstance().InputValue(column, layer, field, _diceRoller.Dices, _currentDiceThrow);

                        if (column == ColumnTypes.ANNOUNCEMENT)
                        {
                            AnnouncementActions(frmTableDisplay, field);
                        }
                        else
                        {
                            NextTurn();
                            frmTableDisplay.EndOfTurnActions();
                        }

                        if (YambTable.GetInstance().IsTableFull())
                        {
                            frmTableDisplay.GameFinished();
                        }
                    }
                    catch (YambException)
                    {
                        frmTableDisplay.DisplayMsg(Color.Red, _msg[5]);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #region private functions

        private bool ValidEntering(IConfirmForm frmConfirm, ColumnTypes column)
        {
            if (_currentDiceThrow == DiceThrow.FIRST && column == ColumnTypes.ANNOUNCEMENT)
            {
                return true;
            }
            else
            {
                string msg = GetMessage(column);
                return frmConfirm.ShowMsg(msg);
            }
        }

        private string GetMessage(ColumnTypes column)
        {
            if (column == ColumnTypes.ANNOUNCEMENT && _currentDiceThrow == DiceThrow.SECOND && !_announced)
            {
                return "Announce entry?";
            }
            else
            {
                return "Enter the points?";
            }
        }

        private void AnnouncementActions(IViewTableForm frmTableDisplay, FieldTypes field)
        {
            if (_announced)
            {
                _announced = false;
                _autoValueInput = false;
                NextTurn();
                frmTableDisplay.EndOfTurnActions();
            }
            else
            {
                _announcedField = field;
                _announced = true;
                _autoValueInput = true;
            }

            frmTableDisplay.AnnouncementDisplayActions(_announced);
        }

        private DiceThrow IncreaseThrowCount()
        {
            int throwCount = (int)_currentDiceThrow;
            if (throwCount == MAXIMUM_THROWS)
            {
                _currentDiceThrow = DiceThrow.FIRST;
                _turnEnded = true;
            }
            else
            {
                throwCount++;
                _currentDiceThrow = (DiceThrow)throwCount;
            }

            return _currentDiceThrow;

        }

        private void NextTurn()
        {
            _currentDiceThrow = DiceThrow.FIRST;
            _turnEnded = false;
            UnselectDice();
        }

        private bool OnlyAnnouncementLeft()
        {
            return YambTable.GetInstance().OnlyAnnouncementFieldsLeft();
        }

        #endregion
    }
}
