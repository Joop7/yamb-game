using Microsoft.VisualStudio.TestTools.UnitTesting;

using Yamb.Model;

namespace Yamb.Test
{
    [TestClass]
    public class YambTableTest
    {
        private static YambTable _table;
        
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            _table = YambTable.GetInstance();
        }

        [TestMethod]
        public void Test_InputValueInDownColumn()
        {
            int[] dices = {1, 2, 1, 1, 5};

            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.ONE, dices, DiceThrow.FIRST);

            Assert.AreEqual(3, _table.GetFieldValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.ONE));
        }

        [TestMethod]
        [ExpectedException(typeof(InaccessibleFieldException))]
        public void Test_IllegalInputValueInDownColumn()
        {
            int[] dices = {6, 1, 2, 6, 6};

            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.SIX, dices, DiceThrow.SECOND);
        }

        [TestMethod]
        [ExpectedException(typeof(InaccessibleFieldException))]
        public void Test_InputValueInFilledField()
        {
            int[] dices = { 1, 1, 2, 6, 1 };

            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.ONE, dices, DiceThrow.THIRD);
        }

        [TestMethod]
        public void Test_InputValueInUpColumn()
        {
            int[] dices = { 3, 2, 4, 1, 6 };

            _table.InputValue(ColumnTypes.UP, LayerTypes.LAST, FieldTypes.YAMB, dices, DiceThrow.FIRST);

            Assert.AreEqual(0, _table.GetFieldValue(ColumnTypes.UP, LayerTypes.LAST, FieldTypes.YAMB));
        }

        [TestMethod]
        [ExpectedException(typeof(InaccessibleFieldException))]
        public void Test_IllegalInputValueInUpColumn()
        {
            int[] dices = { 6, 6, 2, 6, 2 };

            _table.InputValue(ColumnTypes.UP, LayerTypes.LAST, FieldTypes.FULL, dices, DiceThrow.SECOND);
        }

        [TestMethod]
        public void Test_InputValueInAnnounceColumn()
        {
            int[] dices = { 3, 1, 4, 2, 3 };

            _table.InputValue(ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.TRIS, dices, DiceThrow.SECOND);

            dices[2] = 3;

            _table.InputValue(ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.TRIS, dices, DiceThrow.THIRD);

            Assert.AreEqual(19, _table.GetFieldValue(ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.TRIS));
        }

        [TestMethod]
        [ExpectedException(typeof(InaccessibleFieldException))]
        public void Test_IllegalInputValueAfterAnnouncement()
        {
            int[] dices = { 2, 1, 3, 4, 4 };

            _table.InputValue(ColumnTypes.ANNOUNCEMENT, LayerTypes.FIRST, FieldTypes.FOUR, dices, DiceThrow.SECOND);

            _table.InputValue(ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.FOUR, dices, DiceThrow.SECOND);
        }

        [TestMethod]
        public void Test_InputValueInSkalaFreeColumn()
        {
            YambTable.ResetInstance();
            _table = YambTable.GetInstance();

            int[] dices = { 2, 1, 3, 5, 4 };
            _table.InputValue(ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.SKALA, dices, DiceThrow.FIRST);

            Assert.AreEqual(35, _table.GetFieldValue(ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.SKALA));
        }

        [TestMethod]
        public void Test_InputTrisWhenDicesShowPokerFreeColumn()
        {
            int[] dices = { 2, 2, 2, 3, 2 };
            _table.InputValue(ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.TRIS, dices, DiceThrow.FIRST);

            Assert.AreEqual(16, _table.GetFieldValue(ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.TRIS));
        }

        [TestMethod]
        public void Test_InputValueInMaxFreeColumn()
        {
            int[] dices = { 5, 6, 6, 4, 6 };
            _table.InputValue(ColumnTypes.FREE, LayerTypes.MIDDLE, FieldTypes.MAX, dices, DiceThrow.FIRST);

            Assert.AreEqual(27, _table.GetFieldValue(ColumnTypes.FREE, LayerTypes.MIDDLE, FieldTypes.MAX));
        }

        [TestMethod]
        public void Test_GetMiddleLayerPointsFreeColumn()
        {
            YambTable.ResetInstance();
            _table = YambTable.GetInstance();

            int[] dices = { 5, 6, 6, 4, 6 };
            _table.InputValue(ColumnTypes.FREE, LayerTypes.MIDDLE, FieldTypes.MAX, dices, DiceThrow.FIRST);
            
            dices[0] = 1;
            dices[1] = 2;
            dices[2] = 1;
            dices[3] = 3;
            dices[4] = 1;
            _table.InputValue(ColumnTypes.FREE, LayerTypes.MIDDLE, FieldTypes.MIN, dices, DiceThrow.FIRST);

            Assert.AreEqual(0, _table.GetLayerPointsByColumn(ColumnTypes.FREE, LayerTypes.MIDDLE));

            dices[0] = 1;
            dices[1] = 1;
            dices[2] = 6;
            dices[3] = 1;
            dices[4] = 1;
            _table.InputValue(ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.ONE, dices, DiceThrow.FIRST);

            Assert.AreEqual(76, _table.GetLayerPointsByColumn(ColumnTypes.FREE, LayerTypes.MIDDLE));
        }

        [TestMethod]
        public void Test_GetTotalPoints()
        {
            YambTable.ResetInstance();
            _table = YambTable.GetInstance();

            int[] dices = { 5, 6, 6, 4, 6 };
            _table.InputValue(ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.SIX, dices, DiceThrow.FIRST);

            dices[0] = 4;
            dices[1] = 1;
            dices[2] = 6;
            dices[3] = 1;
            dices[4] = 3;
            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.ONE, dices, DiceThrow.FIRST);

            dices[0] = 5;
            dices[1] = 5;
            dices[2] = 6;
            dices[3] = 5;
            dices[4] = 5;
            _table.InputValue(ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.POKER, dices, DiceThrow.SECOND);
            _table.InputValue(ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.POKER, dices, DiceThrow.THIRD);

            dices[0] = 4;
            dices[1] = 4;
            dices[2] = 4;
            dices[3] = 4;
            dices[4] = 4;
            _table.InputValue(ColumnTypes.UP, LayerTypes.LAST, FieldTypes.YAMB, dices, DiceThrow.FIRST);



            Assert.AreEqual(150, _table.GetTotalPoints());
        }

        [TestMethod]
        public void Test_FirstLayerBonusPoints()
        {
            YambTable.ResetInstance();
            _table = YambTable.GetInstance();

            int[] dices = { 1, 6, 1, 4, 1 };
            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.ONE, dices, DiceThrow.FIRST);

            dices[0] = 2;
            dices[1] = 2;
            dices[2] = 2;
            dices[3] = 1;
            dices[4] = 3;
            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.TWO, dices, DiceThrow.FIRST);

            dices[0] = 3;
            dices[1] = 3;
            dices[2] = 6;
            dices[3] = 5;
            dices[4] = 3;
            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.THREE, dices, DiceThrow.SECOND);
           
            dices[0] = 4;
            dices[1] = 4;
            dices[2] = 3;
            dices[3] = 4;
            dices[4] = 4;
            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.FOUR, dices, DiceThrow.FIRST);

            dices[0] = 5;
            dices[1] = 5;
            dices[2] = 3;
            dices[3] = 5;
            dices[4] = 4;
            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.FIVE, dices, DiceThrow.FIRST);

            Assert.AreEqual(49, _table.GetLayerPointsByColumn(ColumnTypes.DOWN, LayerTypes.FIRST));

            dices[0] = 6;
            dices[1] = 5;
            dices[2] = 6;
            dices[3] = 5;
            dices[4] = 4;
            _table.InputValue(ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.SIX, dices, DiceThrow.FIRST);

            Assert.AreEqual(91, _table.GetLayerPointsByColumn(ColumnTypes.DOWN, LayerTypes.FIRST));
        }
    }
}
