using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Yamb.Model
{
    public static class XMLHandler
    {
        public static void LoadXMLHighscore()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Highscore.xml");
            XmlNodeList highscores = xmlDoc.GetElementsByTagName("Highscore");

            Highscore.ResetInstance();
            Highscore highscoreInstance = Highscore.GetInstance();

            foreach (XmlNode highscore in highscores)
            {
                string player = highscore["Player"].InnerText;
                int result = int.Parse((highscore["Result"].InnerText));
                int rank = int.Parse((highscore["Rank"].InnerText));

                highscoreInstance.AddHighscore(player, result, rank);
            }
        }

        public static void SaveHighscoreToXML()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = (XmlElement)doc.AppendChild(doc.CreateElement("Highscores"));

            Highscore highscoreInstance = Highscore.GetInstance();

            foreach (PlayerResult highscore in highscoreInstance.Highscores)
            {
                XmlElement item = (XmlElement)root.AppendChild(doc.CreateElement("Highscore"));
                XmlElement player = (XmlElement)item.AppendChild(doc.CreateElement("Player"));
                player.InnerText = highscore.Player;
                XmlElement result = (XmlElement)item.AppendChild(doc.CreateElement("Result"));
                result.InnerText = highscore.Result.ToString();
                XmlElement rank = (XmlElement)item.AppendChild(doc.CreateElement("Rank"));
                result.InnerText = highscore.Rank.ToString();
            }

            doc.Save("Highscore.xml");
        }
    }
}
