using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yamb.Model
{
    public class Serializer
    {
        public void SerializeObject(string fileName, Highscore highscore)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, highscore);
            stream.Close();
        }

        public Highscore DeSerializeObject(string fileName)
        {
            Highscore highscore;
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            highscore = (Highscore)bFormatter.Deserialize(stream);
            stream.Close();
            return highscore;
        }
    }
}
