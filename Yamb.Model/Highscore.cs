using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yamb.Model
{
    [Serializable]
    public class PlayerResult : ISerializable
    {
        public string Player { get; set; }
        public int Result { get; set; }
        public int Rank { get; set; }

        public PlayerResult(string inPlayer, int inResult, int inRank)
        {
            Player = inPlayer;
            Result = inResult;
            Rank = inRank;
        }

        public PlayerResult(SerializationInfo info, StreamingContext ctxt)
        {
           this.Player = (string)info.GetValue("Player", typeof(string));
           this.Result = (int)info.GetValue("Result", typeof(int));
           this.Rank = (int)info.GetValue("Rank", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Player", this.Player);
            info.AddValue("Result", this.Result);
            info.AddValue("Rank", this.Rank);
        }
    }

    [Serializable]
    public class HighscoreTable : ISerializable
    {
        public HighscoreColumn DownColumn = new HighscoreColumn();
        public HighscoreColumn UpColumn = new HighscoreColumn();
        public HighscoreColumn FreeColumn = new HighscoreColumn();
        public HighscoreColumn AnnouncementColumn = new HighscoreColumn();

        public int Rank;
        public string Player;

        public HighscoreTable()
        {
        }

        public HighscoreTable(SerializationInfo info, StreamingContext ctxt)
        {
            this.DownColumn = (HighscoreColumn)info.GetValue("DownColumn", typeof(HighscoreColumn));
            this.UpColumn = (HighscoreColumn)info.GetValue("UpColumn", typeof(HighscoreColumn));
            this.FreeColumn = (HighscoreColumn)info.GetValue("FreeColumn", typeof(HighscoreColumn));
            this.AnnouncementColumn = (HighscoreColumn)info.GetValue("AnnouncementColumn", typeof(HighscoreColumn));
            this.Rank = (int)info.GetValue("Rank", typeof(int));
            this.Player = (string)info.GetValue("Player", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("DownColumn", this.DownColumn);
            info.AddValue("UpColumn", this.UpColumn);
            info.AddValue("FreeColumn", this.FreeColumn);
            info.AddValue("AnnouncementColumn", this.AnnouncementColumn);
            info.AddValue("Rank", this.Rank);
            info.AddValue("Player", this.Player);
        }

        public void InputColumn(ColumnTypes type, Column column)
        {
            switch (type)
            {
                case ColumnTypes.DOWN:
                    DownColumn.AddLayers(column.Layers);
                    
                    break;
                case ColumnTypes.UP:
                    UpColumn.AddLayers(column.Layers);
                    break;
                case ColumnTypes.FREE:
                    FreeColumn.AddLayers(column.Layers);
                    break;
                case ColumnTypes.ANNOUNCEMENT:
                    AnnouncementColumn.AddLayers(column.Layers);
                    break;
                default:
                    throw new YambException("");
            }
        }

        public int GetLayerPointsByColumn(ColumnTypes column, LayerTypes layer)
        {
            switch (column)
            {
                case ColumnTypes.DOWN:
                    return DownColumn.GetLayerPoints(layer);
                case ColumnTypes.UP:
                    return UpColumn.GetLayerPoints(layer);
                case ColumnTypes.FREE:
                    return FreeColumn.GetLayerPoints(layer);
                case ColumnTypes.ANNOUNCEMENT:
                    return AnnouncementColumn.GetLayerPoints(layer);
                default:
                    throw new YambException("");
            }
        }

        public int GetTotalLayerPoints(LayerTypes layerType)
        {
            switch (layerType)
            {
                case LayerTypes.FIRST:
                    return DownColumn.FLayer.LayerPoints + UpColumn.FLayer.LayerPoints + FreeColumn.FLayer.LayerPoints +
                           AnnouncementColumn.FLayer.LayerPoints;
                case LayerTypes.MIDDLE:
                    return DownColumn.MLayer.LayerPoints + UpColumn.MLayer.LayerPoints + FreeColumn.MLayer.LayerPoints +
                           AnnouncementColumn.MLayer.LayerPoints;
                case LayerTypes.LAST:
                    return DownColumn.LLayer.LayerPoints + UpColumn.LLayer.LayerPoints + FreeColumn.LLayer.LayerPoints +
                           AnnouncementColumn.LLayer.LayerPoints; 
                default:
                    throw new YambException("");
            }
        }

        public int GetTotalPoints()
        {
            return GetTotalLayerPoints(LayerTypes.FIRST) + GetTotalLayerPoints(LayerTypes.MIDDLE) +
                   GetTotalLayerPoints(LayerTypes.LAST);
        }

        public int GetFieldValue(ColumnTypes column, LayerTypes layer, FieldTypes field)
        {
            switch (column)
            {
                case ColumnTypes.DOWN:
                    return DownColumn.GetFieldValue(layer, field);
                case ColumnTypes.UP:
                    return UpColumn.GetFieldValue(layer, field);
                case ColumnTypes.FREE:
                    return FreeColumn.GetFieldValue(layer, field);
                case ColumnTypes.ANNOUNCEMENT:
                    return AnnouncementColumn.GetFieldValue(layer, field);
                default:
                    throw new YambException("");
            }
        }

    }

    [Serializable]
    public class HighscoreColumn : ISerializable
    {
        public HighscoreLayer FLayer = new HighscoreLayer();
        public HighscoreLayer MLayer = new HighscoreLayer();
        public HighscoreLayer LLayer = new HighscoreLayer();

        public HighscoreColumn()
        {
        }

        public HighscoreColumn(SerializationInfo info, StreamingContext ctxt)
        {
            this.FLayer = (HighscoreLayer)info.GetValue("FLayer", typeof(HighscoreLayer));
            this.MLayer = (HighscoreLayer)info.GetValue("MLayer", typeof(HighscoreLayer));
            this.LLayer = (HighscoreLayer)info.GetValue("LLayer", typeof(HighscoreLayer));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("FLayer", this.FLayer);
            info.AddValue("MLayer", this.MLayer);
            info.AddValue("LLayer", this.LLayer);
        }

        public void AddLayers(Dictionary<LayerTypes, Layer> Layers)
        {
            foreach (var fields in Layers[LayerTypes.FIRST].Fields.Values)
            {
                FLayer.InputValue(fields.Value);
                FLayer.LayerPoints = Layers[LayerTypes.FIRST].GetLayerPoints();
            }
            foreach (var fields in Layers[LayerTypes.MIDDLE].Fields.Values)
            {
                MLayer.InputValue(fields.Value);
                MLayer.LayerPoints = Layers[LayerTypes.MIDDLE].GetLayerPoints();
            }
            foreach (var fields in Layers[LayerTypes.LAST].Fields.Values)
            {
                LLayer.InputValue(fields.Value);
                LLayer.LayerPoints = Layers[LayerTypes.LAST].GetLayerPoints();
            }
        }

        public int GetLayerPoints(LayerTypes layer)
        {
            switch (layer)
            {
                 case LayerTypes.FIRST:
                    return FLayer.LayerPoints;
                 case LayerTypes.MIDDLE:
                    return MLayer.LayerPoints;
                 case LayerTypes.LAST:
                    return LLayer.LayerPoints;
                default:
                    throw new YambException("");
            }
        }

        public int GetFieldValue(LayerTypes layer, FieldTypes field)
        {
            switch (layer)
            {
                case LayerTypes.FIRST:
                    return FLayer.GetValue(((int) field) - 1);
                case LayerTypes.MIDDLE:
                    return MLayer.GetValue(((int) field) - 6);
                case LayerTypes.LAST:
                    return LLayer.GetValue(((int) field) - 9);
                default:
                    throw new YambException("");
            }
        }
    }

    [Serializable]
    public class HighscoreLayer : ISerializable
    {
        public List<HighscoreField> Fields { get; private set; }
        public int LayerPoints;

        public HighscoreLayer()
        {
            Fields = new List<HighscoreField>();
        }

        public HighscoreLayer(SerializationInfo info, StreamingContext ctxt)
        {
            this.Fields = (List<HighscoreField>)info.GetValue("Fields", typeof(List<HighscoreField>));
            this.LayerPoints = (int)info.GetValue("LayerPoints", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Fields", this.Fields);
            info.AddValue("LayerPoints", this.LayerPoints);
        }

        public void InputValue(int inValue)
        {
            Fields.Add(new HighscoreField(inValue));
        }

        public int GetValue(int index)
        {
            return Fields[index].Value;
        }

    }

    [Serializable]
    public class HighscoreField : ISerializable
    {
        public int Value;

        public HighscoreField(int inValue)
        {
            Value = inValue;
        }

        public HighscoreField(SerializationInfo info, StreamingContext ctxt)
        {
            this.Value = (int)info.GetValue("Value", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Value", this.Value);
        }
    }

    [Serializable]
    public class Highscore : ISerializable
    {
        private const int NUMBER_OF_RANKS = 5;
        private static Highscore _instance = null;
        public int insertedRank { get; private set; }
        public List<PlayerResult> Highscores { get; set; }
        public List<HighscoreTable> HighscoreTables { get; set; } 

        private Highscore()
        {
            Highscores = new List<PlayerResult>();
            HighscoreTables = new List<HighscoreTable>();
        }

        public Highscore(SerializationInfo info, StreamingContext ctxt)
        {
            this.Highscores = (List<PlayerResult>)info.GetValue("Highscores", typeof(List<PlayerResult>));
            this.HighscoreTables = (List<HighscoreTable>)info.GetValue("HighscoreTables", typeof(List<HighscoreTable>));
        }

        public static Highscore GetInstance()
        {
            if (_instance == null)
                _instance = new Highscore();

            return _instance;
        }

        public static void ResetInstance()
        {
             _instance = null;
        }

        public bool AddHighscore(string player, int result, int rank)
        {
            if (Highscores.Count < NUMBER_OF_RANKS)
            {
                Highscores.Add(new PlayerResult(player, result, rank));
                return true;
            }
            else
            {
                bool last = true;
                PlayerResult lastRank = null;
                int rankCounter = NUMBER_OF_RANKS + 1;
                foreach (PlayerResult highscore in Highscores.OrderByDescending(x => x.Rank))
                {
                    if (last)
                    {
                        lastRank = highscore;
                        last = false;
                    }
                    if (highscore.Result < result)
                    {
                        rankCounter--;
                    }
                    else
                    {
                        break;
                    }
                }
                if (lastRank.Result < result)
                {
                    insertedRank = rankCounter;
                    RefreshRankigns();
                    Highscores.Remove(lastRank);
                    Highscores.Add(new PlayerResult(player, result, rankCounter));
                    NewHighscoreTable(player);
                    return true;
                }
            }
            return false;
        }

        private void RefreshRankigns()
        {
            int counter = NUMBER_OF_RANKS + 1 - insertedRank;
            int ranking = NUMBER_OF_RANKS;
            while (counter > 0)
            {
                PlayerResult score = GetHighscore(ranking);
                score.Rank++;
                HighscoreTable table = GetHighscoreTable(ranking);
                table.Rank++;
                ranking--;
                counter--;
            }
        }

        public void NewHighscoreTable(string player)
        {
            HighscoreTable table = new HighscoreTable();
            table.InputColumn(ColumnTypes.DOWN, YambTable.GetInstance().Columns[ColumnTypes.DOWN]);
            table.InputColumn(ColumnTypes.UP, YambTable.GetInstance().Columns[ColumnTypes.UP]);
            table.InputColumn(ColumnTypes.FREE, YambTable.GetInstance().Columns[ColumnTypes.FREE]);
            table.InputColumn(ColumnTypes.ANNOUNCEMENT, YambTable.GetInstance().Columns[ColumnTypes.ANNOUNCEMENT]);
            table.Rank = insertedRank;
            table.Player = player;

            HighscoreTable last = GetHighscoreTable(NUMBER_OF_RANKS + 1);
            HighscoreTables.Remove(last);
            HighscoreTables.Add(table);
        }

        public PlayerResult GetHighscore(int rank)
        {
            foreach (PlayerResult highscore in Highscores)
            {
                if (rank == highscore.Rank)
                {
                    return highscore;
                }
            }

            throw new Exception();
        }

        public HighscoreTable GetHighscoreTable(int rank)
        {
            foreach (HighscoreTable highscoreTable in HighscoreTables)
            {
                if (rank == highscoreTable.Rank)
                {
                    return highscoreTable;
                }
            }

            throw new Exception();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Highscores", this.Highscores);
            info.AddValue("HighscoreTables", this.HighscoreTables);
        }

    }
}
