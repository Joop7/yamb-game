using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yamb.Model;

namespace Yamb.App
{
    public partial class frmBestHighscore : Form
    {
        const int IMAGE_WIDTH = 25;
        const int IMAGE_HEIGHT = 25;

        private int DisplayRank;

        Dictionary<ColumnTypes, Dictionary<FieldTypes, PictureBox>> allInputFields;
        Dictionary<ColumnTypes, Dictionary<LayerTypes, PictureBox>> pointFields;
        Dictionary<LayerTypes, PictureBox> totalPointFieldsByLayer;

        Graphics valueToImageWriter;
        Bitmap valueImage;
        Font font = new Font("Calibri", 9, FontStyle.Bold);
        Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0));
        Point point;

        public frmBestHighscore(int rank)
        {
            InitializeComponent();

            DisplayRank = rank;

            allInputFields = new Dictionary<ColumnTypes, Dictionary<FieldTypes, PictureBox>>();
            pointFields = new Dictionary<ColumnTypes, Dictionary<LayerTypes, PictureBox>>();
            totalPointFieldsByLayer = new Dictionary<LayerTypes, PictureBox>();

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
            Dictionary<LayerTypes, PictureBox> pointFieldsByLayer = new Dictionary<LayerTypes, PictureBox>();
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

        private void frmBestHighscore_Load(object sender, EventArgs e)
        {
            int value = 0;
            foreach (ColumnTypes column in Enum.GetValues(typeof(ColumnTypes)))
            {
                foreach (LayerTypes layer in Enum.GetValues(typeof(LayerTypes)))
                {
                    foreach (FieldTypes field in Enum.GetValues(typeof(FieldTypes)))
                    {
                        if ((layer == LayerTypes.FIRST && field == FieldTypes.MAX)
                            || (layer == LayerTypes.MIDDLE && field == FieldTypes.TRIS))
                        {
                            break;
                        }
                        if ((layer == LayerTypes.MIDDLE && (int)field < 7)
                            || (layer == LayerTypes.LAST && (int)field < 9))
                        {
                            continue;
                        }
                        value = Highscore.GetInstance().GetHighscoreTable(DisplayRank).GetFieldValue(column, layer, field);
                        DisplayValue(value, allInputFields[column][field]);
                    }
                    value = Highscore.GetInstance().GetHighscoreTable(DisplayRank).GetLayerPointsByColumn(column, layer);
                    DisplayValue(value, pointFields[column][layer]);

                    value = Highscore.GetInstance().GetHighscoreTable(DisplayRank).GetTotalLayerPoints(layer);
                    DisplayValue(value, totalPointFieldsByLayer[layer]);
                }
            }
            value = Highscore.GetInstance().GetHighscoreTable(DisplayRank).GetTotalPoints();
            DisplayValue(value, picBox_TotalPoints);
            lbl_player.Text = Highscore.GetInstance().GetHighscoreTable(DisplayRank).Player;
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
    }
}
