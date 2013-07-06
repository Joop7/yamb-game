using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Yamb.Util;

namespace Yamb.Model
{
    public enum ColumnTypes
    {
        DOWN = 1,
        UP = 2,
        FREE = 3,
        ANNOUNCEMENT = 4
    }

    public enum LayerTypes
    {
        FIRST = 1,
        MIDDLE = 2,
        LAST = 3
    }

    public enum FieldTypes
    {
        ONE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
        MAX = 7,
        MIN = 8,
        TRIS = 9,
        SKALA = 10,
        FULL = 11,
        POKER = 12,
        YAMB = 13
    }

    public enum DiceThrow
    {
        FIRST = 1,
        SECOND = 2,
        THIRD = 3
    }

    public class YambTable : Subject
    {
        private bool _announced = false;

        private static YambTable _instance = null;

        public Dictionary<ColumnTypes, Column> Columns { get; private set; }

        private YambTable()
        {
            Columns = new Dictionary<ColumnTypes, Column>();
            Columns.Add(ColumnTypes.DOWN, ColumnFactory.GetColumn(ColumnTypes.DOWN));
            Columns.Add(ColumnTypes.UP, ColumnFactory.GetColumn(ColumnTypes.UP));
            Columns.Add(ColumnTypes.FREE, ColumnFactory.GetColumn(ColumnTypes.FREE));
            Columns.Add(ColumnTypes.ANNOUNCEMENT, ColumnFactory.GetColumn(ColumnTypes.ANNOUNCEMENT));
        }

        public static YambTable GetInstance()
        {
            if (_instance == null)
                _instance = new YambTable();

            return _instance;
        }

        public static void ResetInstance()
        {
            _instance = null;
        }

        public int GetLayerPointsByColumn(ColumnTypes column, LayerTypes layer)
        {
            return Columns[column].GetLayerPoints(layer);
        }

        public int GetTotalLayerPoints(LayerTypes layer)
        {
            int totalLayerPoints = 0;
            foreach (KeyValuePair<ColumnTypes, Column> column in Columns)
            {
                try
                {
                    totalLayerPoints += column.Value.GetLayerPoints(layer);
                }
                catch (MissingParametarsException)
                { }
            }
            return totalLayerPoints;
        }

        public int GetTotalPoints()
        {
            int totalPoints = 0;
            foreach (LayerTypes layer in Enum.GetValues(typeof(LayerTypes)))
            {
                totalPoints += GetTotalLayerPoints(layer);
            }
            return totalPoints;
        }

        public int GetFieldValue(ColumnTypes column, LayerTypes layer, FieldTypes field)
        {
            int value = 0;
            try
            {
                value = Columns[column].GetFieldValue(layer, field);
            }
            catch (FieldIsEmptyException)
            {
            }
            catch (KeyNotFoundException)
            {
                throw new NonExistingFieldException("Ne postoji takvo polje u odabranom sloju.");
            }


            return value;
        }

        public void InputValue(ColumnTypes column, LayerTypes layer, FieldTypes field, int[] diceNumbers, DiceThrow diceThrow)
        {
            if (column == ColumnTypes.ANNOUNCEMENT && !_announced)
            {
                if (diceThrow == DiceThrow.SECOND)
                {
                    Columns[column].InputValues(layer, field, diceNumbers);
                    _announced = true;
                }
                else
                {
                    throw new InaccessibleFieldException("Polje je nedostupno.");
                }
            }
            else if (_announced && column != ColumnTypes.ANNOUNCEMENT)
            {
                throw new InaccessibleFieldException("Polje je nedostupno.");
            }
            else
            {
                Columns[column].InputValues(layer, field, diceNumbers);
                _announced = false;
            }

            NotifyObservers();
        }

        public bool OnlyAnnouncementFieldsLeft()
        {
            if (Columns[ColumnTypes.UP].IsColumnFull() && Columns[ColumnTypes.FREE].IsColumnFull() &&
                Columns[ColumnTypes.DOWN].IsColumnFull() && !Columns[ColumnTypes.ANNOUNCEMENT].IsColumnFull())
            {
                return true;
            }
            return false;
        }

        public bool IsTableFull()
        {
            foreach (KeyValuePair<ColumnTypes, Column> column in Columns)
            {
                if (!column.Value.IsColumnFull())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
