using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

using Yamb.Controller;
using Yamb.Model;
using Yamb.Util;

namespace Yamb.App
{
    public partial class frmYamb : Form, IViewTableForm
    {
        #region CONSTANTS

        const int MINIMUM_DICE_ROLLS = 15;
        const int MAXIMUM_DICE_ROLLS = 30;
        const int DICE_ROLL_TIME = 85;

        const int IMAGE_WIDTH = 25;
        const int IMAGE_HEIGHT = 25;

        #endregion

        #region PRIVATE ATRIBUTES

        Game game;

        PictureBox announcedField;
        ColumnTypes inputFiledsColumn;
        FieldTypes inputFieldType;

        Dictionary<ColumnTypes, Dictionary<FieldTypes, PictureBox>> allInputFields;
        Dictionary<ColumnTypes, Dictionary<LayerTypes, PictureBox>> pointFields;
        Dictionary<LayerTypes, PictureBox> totalPointFieldsByLayer;

        Graphics valueToImageWriter;
        Bitmap valueImage;
        Font font = new Font("Calibri", 9, FontStyle.Bold);
        Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0));
        Point point;

        #endregion

        #region INITIALIZATION

        public frmYamb()
        {
            InitializeComponent();
            
            allInputFields = new Dictionary<ColumnTypes, Dictionary<FieldTypes, PictureBox>>();
            pointFields = new Dictionary<ColumnTypes, Dictionary<LayerTypes, PictureBox>>();
            totalPointFieldsByLayer = new Dictionary<LayerTypes, PictureBox>();

            ThreadPool.SetMinThreads(Constants.NUMBER_OF_DICES, Constants.NUMBER_OF_DICES);
        }

        private void frmYamb_Load(object sender, EventArgs e)
        {
            InitializeDiceOutput();

            allInputFields.Add(ColumnTypes.DOWN, GetDownInputFileds());
            allInputFields.Add(ColumnTypes.UP, GetUpInputFileds());
            allInputFields.Add(ColumnTypes.FREE, GetFreeInputFileds());
            allInputFields.Add(ColumnTypes.ANNOUNCEMENT, GetAnnouncementInputFileds());

            pointFields.Add(ColumnTypes.DOWN, GetDownPointFieldsByLayer());
            pointFields.Add(ColumnTypes.UP, GetUpPointFieldsByLayer());
            pointFields.Add(ColumnTypes.FREE, GetFreePointFieldsByLayer());
            pointFields.Add(ColumnTypes.ANNOUNCEMENT, GetAnnouncedPointFieldsByLayer());

            totalPointFieldsByLayer.Add(LayerTypes.FIRST, picBox_FirstLayerPoints);
            totalPointFieldsByLayer.Add(LayerTypes.MIDDLE, picBox_MiddleLayerPoints);
            totalPointFieldsByLayer.Add(LayerTypes.LAST, picBox_LastLayerPoints);

            InitializePointOutput();
        }

        private void InitializeDiceOutput()
        {
            picBox_dice1.Image = Properties.Resources.dice0;
            picBox_dice2.Image = Properties.Resources.dice0;
            picBox_dice3.Image = Properties.Resources.dice0;
            picBox_dice4.Image = Properties.Resources.dice0;
            picBox_dice5.Image = Properties.Resources.dice0;
        }

        #region input fileds functions
        private Dictionary<FieldTypes, PictureBox> GetDownInputFileds()
        {
            Dictionary<FieldTypes, PictureBox> inputFields = new Dictionary<FieldTypes, PictureBox>();
            inputFields.Add(FieldTypes.ONE, picBox_row1Down);
            inputFields.Add(FieldTypes.TWO, picBox_row2Down);
            inputFields.Add(FieldTypes.THREE, picBox_row3Down);
            inputFields.Add(FieldTypes.FOUR, picBox_row4Down);
            inputFields.Add(FieldTypes.FIVE, picBox_row5Down);
            inputFields.Add(FieldTypes.SIX, picBox_row6Down);
            inputFields.Add(FieldTypes.MAX, picBox_rowMaxDown);
            inputFields.Add(FieldTypes.MIN, picBox_rowMinDown);
            inputFields.Add(FieldTypes.TRIS, picBox_rowTrisDown);
            inputFields.Add(FieldTypes.SKALA, picBox_rowSkalaDown);
            inputFields.Add(FieldTypes.FULL, picBox_rowFullDown);
            inputFields.Add(FieldTypes.POKER, picBox_rowPokerDown);
            inputFields.Add(FieldTypes.YAMB, picBox_rowYambDown);

            return inputFields;
        }

        private Dictionary<FieldTypes, PictureBox> GetUpInputFileds()
        {
            Dictionary<FieldTypes, PictureBox> inputFields = new Dictionary<FieldTypes, PictureBox>();
            inputFields.Add(FieldTypes.ONE, picBox_row1Up);
            inputFields.Add(FieldTypes.TWO, picBox_row2Up);
            inputFields.Add(FieldTypes.THREE, picBox_row3Up);
            inputFields.Add(FieldTypes.FOUR, picBox_row4Up);
            inputFields.Add(FieldTypes.FIVE, picBox_row5Up);
            inputFields.Add(FieldTypes.SIX, picBox_row6Up);
            inputFields.Add(FieldTypes.MAX, picBox_rowMaxUp);
            inputFields.Add(FieldTypes.MIN, picBox_rowMinUp);
            inputFields.Add(FieldTypes.TRIS, picBox_rowTrisUp);
            inputFields.Add(FieldTypes.SKALA, picBox_rowSkalaUp);
            inputFields.Add(FieldTypes.FULL, picBox_rowFullUp);
            inputFields.Add(FieldTypes.POKER, picBox_rowPokerUp);
            inputFields.Add(FieldTypes.YAMB, picBox_rowYambUp);

            return inputFields;
        }

        private Dictionary<FieldTypes, PictureBox> GetFreeInputFileds()
        {
            Dictionary<FieldTypes, PictureBox> inputFields = new Dictionary<FieldTypes, PictureBox>();
            inputFields.Add(FieldTypes.ONE, picBox_row1Free);
            inputFields.Add(FieldTypes.TWO, picBox_row2Free);
            inputFields.Add(FieldTypes.THREE, picBox_row3Free);
            inputFields.Add(FieldTypes.FOUR, picBox_row4Free);
            inputFields.Add(FieldTypes.FIVE, picBox_row5Free);
            inputFields.Add(FieldTypes.SIX, picBox_row6Free);
            inputFields.Add(FieldTypes.MAX, picBox_rowMaxFree);
            inputFields.Add(FieldTypes.MIN, picBox_rowMinFree);
            inputFields.Add(FieldTypes.TRIS, picBox_rowTrisFree);
            inputFields.Add(FieldTypes.SKALA, picBox_rowSkalaFree);
            inputFields.Add(FieldTypes.FULL, picBox_rowFullFree);
            inputFields.Add(FieldTypes.POKER, picBox_rowPokerFree);
            inputFields.Add(FieldTypes.YAMB, picBox_rowYambFree);

            return inputFields;
        }

        private Dictionary<FieldTypes, PictureBox> GetAnnouncementInputFileds()
        {
            Dictionary<FieldTypes, PictureBox> inputFields = new Dictionary<FieldTypes, PictureBox>();
            inputFields.Add(FieldTypes.ONE, picBox_row1Announce);
            inputFields.Add(FieldTypes.TWO, picBox_row2Announce);
            inputFields.Add(FieldTypes.THREE, picBox_row3Announce);
            inputFields.Add(FieldTypes.FOUR, picBox_row4Announce);
            inputFields.Add(FieldTypes.FIVE, picBox_row5Announce);
            inputFields.Add(FieldTypes.SIX, picBox_row6Announce);
            inputFields.Add(FieldTypes.MAX, picBox_rowMaxAnnounce);
            inputFields.Add(FieldTypes.MIN, picBox_rowMinAnnounce);
            inputFields.Add(FieldTypes.TRIS, picBox_rowTrisAnnounce);
            inputFields.Add(FieldTypes.SKALA, picBox_rowSkalaAnnounce);
            inputFields.Add(FieldTypes.FULL, picBox_rowFullAnnounce);
            inputFields.Add(FieldTypes.POKER, picBox_rowPokerAnnounce);
            inputFields.Add(FieldTypes.YAMB, picBox_rowYambAnnounce);

            return inputFields;
        }
        #endregion

        #region point fileds functions
        private Dictionary<LayerTypes, PictureBox> GetDownPointFieldsByLayer()
        {
            Dictionary<LayerTypes, PictureBox>  pointFieldsByLayer = new Dictionary<LayerTypes, PictureBox>();
            pointFieldsByLayer.Add(LayerTypes.FIRST, picBox_FirstDownPoints);
            pointFieldsByLayer.Add(LayerTypes.MIDDLE, picBox_MiddleDownPoints);
            pointFieldsByLayer.Add(LayerTypes.LAST, picBox_LastDownPoints);

            return pointFieldsByLayer;
        }

        private Dictionary<LayerTypes, PictureBox> GetUpPointFieldsByLayer()
        {
            Dictionary<LayerTypes, PictureBox> pointFieldsByLayer = new Dictionary<LayerTypes, PictureBox>();
            pointFieldsByLayer.Add(LayerTypes.FIRST, picBox_FirstUpPoints);
            pointFieldsByLayer.Add(LayerTypes.MIDDLE, picBox_MiddleUpPoints);
            pointFieldsByLayer.Add(LayerTypes.LAST, picBox_LastUpPoints);

            return pointFieldsByLayer;
        }

        private Dictionary<LayerTypes, PictureBox> GetFreePointFieldsByLayer()
        {
            Dictionary<LayerTypes, PictureBox> pointFieldsByLayer = new Dictionary<LayerTypes, PictureBox>();
            pointFieldsByLayer.Add(LayerTypes.FIRST, picBox_FirstFreePoints);
            pointFieldsByLayer.Add(LayerTypes.MIDDLE, picBox_MiddleFreePoints);
            pointFieldsByLayer.Add(LayerTypes.LAST, picBox_LastFreePoints);

            return pointFieldsByLayer;
        }

        private Dictionary<LayerTypes, PictureBox> GetAnnouncedPointFieldsByLayer()
        {
            Dictionary<LayerTypes, PictureBox> pointFieldsByLayer = new Dictionary<LayerTypes, PictureBox>();
            pointFieldsByLayer.Add(LayerTypes.FIRST, picBox_FirstAnnouncePoints);
            pointFieldsByLayer.Add(LayerTypes.MIDDLE, picBox_MiddleAnnouncePoints);
            pointFieldsByLayer.Add(LayerTypes.LAST, picBox_LastAnnouncePoints);

            return pointFieldsByLayer;
        }
        #endregion

        private void InitializePointOutput()
        {
            Dictionary<ColumnTypes, Dictionary<LayerTypes, PictureBox>>.KeyCollection columns = pointFields.Keys;

            foreach (ColumnTypes column in columns)
            {
                foreach (KeyValuePair<LayerTypes, PictureBox> pointField in pointFields[column])
                {
                    DisplayValue(0, pointField.Value);
                }
            }
            foreach (KeyValuePair<LayerTypes, PictureBox> totalLayerPointField in totalPointFieldsByLayer)
            {
                DisplayValue(0, totalLayerPointField.Value);
            }

            DisplayValue(0, picBox_TotalPoints);
        }

        private Image[] GetDiceImages()
        {
            return new Image[] {
                Properties.Resources.dice0,
                Properties.Resources.dice1,
                Properties.Resources.dice2,
                Properties.Resources.dice3,
                Properties.Resources.dice4,
                Properties.Resources.dice5,
                Properties.Resources.dice6
            };
        }
        
        #endregion

        #region DICE THROW

        private void btn_Throw_Click(object sender, EventArgs e)
        {
            btn_Throw.Enabled = false;
            game.RollDices(this);
            btn_Throw.Enabled = true;
        }

        #endregion

        #region INTERFACE IMPLEMENTATION

        public void DisplayMsg(Color txtColor, string msg)
        {
            lbl_msg.ForeColor = txtColor;
            lbl_msg.Text = msg;
        }

        public void DisplayGivenDices(int[] dices, bool[] selectedDices)
        {
            Image[] diceImages = GetDiceImages();
            int counter = 0;
            foreach (bool selected in selectedDices)
            {
                if (selected)
                {
                    PictureBox diceField = GetPicBox("picBox_dice" + (counter + 1).ToString());
                    diceField.Image = diceImages[dices[counter]];
                }
                counter++;
            }
        }

        public int DecreasDisplayDiceThrow()
        {
            int throwsLeft = int.Parse(lbl_throw.Text);
            if (throwsLeft > 0)
            {
                throwsLeft--;
                lbl_throw.Text = " " + throwsLeft.ToString();
            }
            return throwsLeft;

        }

        public void AnnouncementDisplayActions(bool announced)
        {
            if (!announced)
            {
                announcedField.BackColor = Color.White;
            }
            else
            {
                announcedField.BackColor = Color.PapayaWhip;
            }
        }

        public void EndOfTurnActions()
        {
            InitializeDiceOutput();
            UnselectDices();
            ResetDiceThrowDisplay();
        }

        public void UpdateDisplay()
        {
            int value = YambTable.GetInstance().GetFieldValue(inputFiledsColumn, GetFieldsLayer(inputFieldType), inputFieldType);
            DisplayValue(value, allInputFields[inputFiledsColumn][inputFieldType]);

            value = YambTable.GetInstance().GetLayerPointsByColumn(inputFiledsColumn, GetFieldsLayer(inputFieldType));
            DisplayValue(value, pointFields[inputFiledsColumn][GetFieldsLayer(inputFieldType)]);

            value = YambTable.GetInstance().GetTotalLayerPoints(GetFieldsLayer(inputFieldType));
            DisplayValue(value, totalPointFieldsByLayer[GetFieldsLayer(inputFieldType)]);

            if (inputFieldType == FieldTypes.ONE)
            {
                value = YambTable.GetInstance().GetLayerPointsByColumn(inputFiledsColumn, LayerTypes.MIDDLE);
                DisplayValue(value, pointFields[inputFiledsColumn][LayerTypes.MIDDLE]);

                value = YambTable.GetInstance().GetTotalLayerPoints(LayerTypes.MIDDLE);
                DisplayValue(value, totalPointFieldsByLayer[LayerTypes.MIDDLE]);
            }

            value = YambTable.GetInstance().GetTotalPoints();
            DisplayValue(value, picBox_TotalPoints);

            lbl_msg.ForeColor = Color.ForestGreen;
            lbl_msg.Text = "Throw the dice! Throw the dice! Throw the dice!";
        }

        public void GameFinished()
        {
            bool newHighscoreInput = Highscore.GetInstance()
                                         .AddHighscore(game.Player, YambTable.GetInstance().GetTotalPoints(), 0);
            if (newHighscoreInput)
            {
                frmHighscoreDisplay displayHS = new frmHighscoreDisplay(Highscore.GetInstance().insertedRank);
                displayHS.ShowDialog();
                try
                {
                    Serializer serializer = new Serializer();
                    serializer.SerializeObject("db.dat", Highscore.GetInstance());
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "The application has no access to db.dat file or the file is corrupted!\nReinstall the program to a location where they are no administrator rights necessary.\neg.: C:\\");
                    this.Close();
                }
            }

            frmEndGame endGameForm = new frmEndGame();
            bool newGame = endGameForm.ShowPoints(YambTable.GetInstance().GetTotalPoints());

            if (newGame)
            {
                StartNewGame();
            }
            else
            {
                this.Close();
            }
        }

        public void DisplayDiceSelection(int picBoxIndex, Color color)
        {
            PictureBox picBox = GetPicBox("picBox_dice" + picBoxIndex.ToString());
            picBox.BackColor = color;
        }

        public void SimulateRollingDice(bool[] selectedDices)
        {
            List<Task> tasks = new List<Task>();
            int counter = 0;
            foreach (bool selected in selectedDices)
            {
                if (selected)
                {
                    try
                    {
                        PictureBox diceField = GetPicBox("picBox_dice" + (counter + 1).ToString());
                        tasks.Add(Task.Factory.StartNew(() => DisplaySimulation(diceField, GetDiceImages(), GetNewRandom())));
                    }
                    catch (Exception)
                    {
 
                    }
                }
                counter++;
            }

            if (tasks.Count != 0)
            {
                Task.WaitAll(tasks.ToArray());
            }
        }

        #region help functions

        private void DisplaySimulation(PictureBox displayOutput, Image[] diceImages, Random rand)
        {
            //simuliranje vrtnje kockice, slučajan broj puta
            for (int rolls = 0; rolls < rand.Next(MINIMUM_DICE_ROLLS, MAXIMUM_DICE_ROLLS + 1); rolls++)
            {
                try
                {
                    displayOutput.Image = diceImages[rand.Next(1, Constants.NUMBER_OF_DICE_SIDES + 1)];
                    Thread.Sleep(DICE_ROLL_TIME);
                }
                catch (Exception)
                { }
            }
        }

        private PictureBox GetPicBox(string picBoxName)
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox && control.Name == picBoxName)
                {
                    return (PictureBox)control;
                }
            }
            throw new Exception();
        }

        private Random GetNewRandom()
        {
            return new Random();
        }

        #endregion

        #endregion

        #region DISPLAY VALUE FUNCTIONS

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

        private void DisplayValue(int value, PictureBox picBox)
        {
            point = GetPoint(value);
            valueImage = GetBitmap(value);
            valueToImageWriter = Graphics.FromImage(valueImage);
            valueToImageWriter.DrawString(value.ToString(), font, brush, point);
            picBox.Image = valueImage;
            valueToImageWriter.Dispose();
        }

        private Point GetPoint(int value)
        {
            int numberOfDigits = value.ToString().Length;
            if (numberOfDigits == 1)
            {
                return new Point(6, 3);
            }
            else if (numberOfDigits == 2)
            {
                return new Point(4, 3);
            }
            else
            {
                return new Point(0, 3);
            }
        }

        private Bitmap GetBitmap(int value)
        {
            int numberOfDigits = value.ToString().Length;
            if (numberOfDigits >= 3)
            {
                return new Bitmap(IMAGE_WIDTH + 8, IMAGE_HEIGHT);
            }
            else
            {
                return new Bitmap(IMAGE_WIDTH, IMAGE_HEIGHT);
            }
        }

        private void UnselectDices()
        {
            Control.ControlCollection controls = Controls;
            foreach (Control control in controls)
            {
                if (control is PictureBox)
                {
                    control.BackColor = Color.Transparent;
                }
            }
        }

        private void ResetDiceThrowDisplay()
        {
            lbl_throw.Text = " 3";
        }

        #endregion

        #region CLICK ON DICE EVENTS

        private void picBox_dice1_Click(object sender, EventArgs e)
        {
            game.SelectDice(this, 1);
        }

        private void picBox_dice2_Click(object sender, EventArgs e)
        {
            game.SelectDice(this, 2);
        }

        private void picBox_dice3_Click(object sender, EventArgs e)
        {
            game.SelectDice(this, 3);
        }

        private void picBox_dice4_Click(object sender, EventArgs e)
        {
            game.SelectDice(this, 4);
        }

        private void picBox_dice5_Click(object sender, EventArgs e)
        {
            game.SelectDice(this, 5);
        }

        #endregion

        #region CLICK ON FIELD EVENTS

        PictureBox TEMPannouncedField;
        ColumnTypes TEMPinputFiledsColumn;
        FieldTypes TEMPinputFieldType;

        private void SaveToTEMP()
        {
            TEMPannouncedField = announcedField;
            TEMPinputFiledsColumn = inputFiledsColumn;
            TEMPinputFieldType = inputFieldType;
        }
        private void ReturnFromTEMP()
        {
            announcedField = TEMPannouncedField;
            inputFiledsColumn = TEMPinputFiledsColumn;
            inputFieldType = TEMPinputFieldType;
        }

        #region DOWN COLUMN
        private void picBox_row1Down_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.ONE;

            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.ONE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row2Down_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.TWO;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.TWO);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row3Down_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.THREE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.THREE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row4Down_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.FOUR;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.FOUR);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row5Down_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.FIVE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.FIVE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row6Down_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.SIX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.FIRST, FieldTypes.SIX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMaxDown_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.MAX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.MIDDLE, FieldTypes.MAX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMinDown_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.MIN;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.MIDDLE, FieldTypes.MIN);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowTrisDown_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.TRIS;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.LAST, FieldTypes.TRIS);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowSkalaDown_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.SKALA;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.LAST, FieldTypes.SKALA);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowFullDown_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.FULL;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.LAST, FieldTypes.FULL);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowPokerDown_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.POKER;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.LAST, FieldTypes.POKER);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowYambDown_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.DOWN;
            inputFieldType = FieldTypes.YAMB;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.DOWN, LayerTypes.LAST, FieldTypes.YAMB);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }
        #endregion

        #region UP COLUMN

        private void picBox_row1Up_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.ONE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.FIRST, FieldTypes.ONE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row2Up_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.TWO;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.FIRST, FieldTypes.TWO);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row3Up_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.THREE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.FIRST, FieldTypes.THREE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row4Up_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.FOUR;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.FIRST, FieldTypes.FOUR);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row5Up_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.FIVE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.FIRST, FieldTypes.FIVE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row6Up_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.SIX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.FIRST, FieldTypes.SIX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMaxUp_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.MAX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.MIDDLE, FieldTypes.MAX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMinUp_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.MIN;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.MIDDLE, FieldTypes.MIN);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowTrisUp_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.TRIS;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.LAST, FieldTypes.TRIS);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowSkalaUp_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.SKALA;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.LAST, FieldTypes.SKALA);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowFullUp_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.FULL;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.LAST, FieldTypes.FULL);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowPokerUp_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.POKER;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.LAST, FieldTypes.POKER);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowYambUp_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.UP;
            inputFieldType = FieldTypes.YAMB;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.UP, LayerTypes.LAST, FieldTypes.YAMB);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        #endregion

        #region FREE COLUMN

        private void picBox_row1Free_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.ONE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.ONE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row2Free_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.TWO;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.TWO);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row3Free_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.THREE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.THREE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row4Free_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.FOUR;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.FOUR);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row5Free_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.FIVE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.FIVE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row6Free_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.SIX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.FIRST, FieldTypes.SIX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMaxFree_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.MAX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.MIDDLE, FieldTypes.MAX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMinFree_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.MIN;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.MIDDLE, FieldTypes.MIN);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowTrisFree_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.TRIS;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.TRIS);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowSkalaFree_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.SKALA;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.SKALA);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowFullFree_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.FULL;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.FULL);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowPokerFree_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.POKER;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.POKER);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowYambFree_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            inputFiledsColumn = ColumnTypes.FREE;
            inputFieldType = FieldTypes.YAMB;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.FREE, LayerTypes.LAST, FieldTypes.YAMB);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        #endregion

        #region ANNOUNCE COLUMN

        private void picBox_row1Announce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_row1Announce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.ONE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.FIRST, FieldTypes.ONE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row2Announce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_row2Announce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.TWO;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.FIRST, FieldTypes.TWO);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row3Announce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_row3Announce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.THREE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.FIRST, FieldTypes.THREE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row4Announce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_row4Announce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.FOUR;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.FIRST, FieldTypes.FOUR);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row5Announce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_row5Announce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.FIVE;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.FIRST, FieldTypes.FIVE);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_row6Announce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_row6Announce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.SIX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.FIRST, FieldTypes.SIX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMaxAnnounce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_rowMaxAnnounce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.MAX;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.MIDDLE, FieldTypes.MAX);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowMinAnnounce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_rowMinAnnounce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.MIN;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.MIDDLE, FieldTypes.MIN);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowTrisAnnounce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_rowTrisAnnounce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.TRIS;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.TRIS);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowSkalaAnnounce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_rowSkalaAnnounce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.SKALA;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.SKALA);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowFullAnnounce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_rowFullAnnounce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.FULL;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.FULL);
            if (!entered)
            {
                ReturnFromTEMP();
            }

        }

        private void picBox_rowPokerAnnounce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_rowPokerAnnounce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.POKER;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.POKER);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        private void picBox_rowYambAnnounce_Click(object sender, EventArgs e)
        {
            SaveToTEMP();
            announcedField = picBox_rowYambAnnounce;
            inputFiledsColumn = ColumnTypes.ANNOUNCEMENT;
            inputFieldType = FieldTypes.YAMB;
            bool entered = game.EnterDiceValue(this, new frmConfirm(), ColumnTypes.ANNOUNCEMENT, LayerTypes.LAST, FieldTypes.YAMB);
            if (!entered)
            {
                ReturnFromTEMP();
            }
        }

        #endregion

        #endregion

        private void StartNewGame()
        {

            game = new Game(game.Player);
            YambTable.ResetInstance();

            Control.ControlCollection fields = gb_Table.Controls;
            foreach (PictureBox field in fields)
            {
                if (field.Enabled)
                {
                    field.Image = null;
                }
            }

            InitializeDiceOutput();
            InitializePointOutput();

            lbl_msg.ForeColor = Color.ForestGreen;
            lbl_msg.Text = "Throw the dice! Throw the dice! Throw the dice!";

            ResetDiceThrowDisplay();

            picBox_dice1.BackColor = Color.Transparent;
            picBox_dice2.BackColor = Color.Transparent;
            picBox_dice3.BackColor = Color.Transparent;
            picBox_dice4.BackColor = Color.Transparent;
            picBox_dice5.BackColor = Color.Transparent;

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void frmYamb_Shown(object sender, EventArgs e)
        {
            try
            {

                Serializer serializer = new Serializer();
                Highscore highscore = serializer.DeSerializeObject("db.dat");
                Highscore.GetInstance().Highscores = highscore.Highscores;
                Highscore.GetInstance().HighscoreTables = highscore.HighscoreTables;
            }
            catch (Exception)
            {
                MessageBox.Show("The application has no access to db.dat file or the file is corrupted!\nReinstall the program to a location where they are no administrator rights necessary.\neg.: C:\\");
                this.Close();
            }

            frmWelcomeScreen welcome = new frmWelcomeScreen();
            DialogResult res = welcome.ShowDialog();
            if (res == DialogResult.OK)
            {
                game = new Game(welcome.Player);
                lbl_player.Text = welcome.Player;
            }
            else
            {
                this.Close();
            }
        }

        private void highscoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHighscoreDisplay displayHS = new frmHighscoreDisplay();
            displayHS.ShowDialog();
        }

        private void newPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWelcomeScreen welcome = new frmWelcomeScreen();
            DialogResult res = welcome.ShowDialog();
            if (res == DialogResult.OK)
            {
                game = new Game(welcome.Player);
                lbl_player.Text = welcome.Player;
                StartNewGame();
            }
        }

    }
}
