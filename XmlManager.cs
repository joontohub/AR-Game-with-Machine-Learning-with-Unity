using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlManager
{
    public static void XmlSave<T>(T classForSave, string path)
    {
        System.Xml.Serialization.XmlSerializer sr = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using (System.IO.TextWriter tw = new System.IO.StreamWriter(path))
        {
            sr.Serialize(tw, classForSave);
            tw.Close();
        }
    }
    public static T XmlLoad<T>(string path)
    {
        System.Xml.Serialization.XmlSerializer sr = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using (System.IO.FileStream fs = new System.IO.FileStream(path,System.IO.FileMode.Open))
        {
            T t = (T)sr.Deserialize(fs);
            fs.Close();

            return t;
        }

    }
}
