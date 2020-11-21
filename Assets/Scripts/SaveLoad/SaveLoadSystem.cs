using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public static class SaveLoadSystem 
{
    public static void SavePlayer(Player player)
    {
        var serializer = new XmlSerializer(typeof(PlayerSaveData));
        string path = Application.persistentDataPath + "/player.xml";
        var stream = new FileStream(path, FileMode.Create);
        var data = new PlayerSaveData(player);
        serializer.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerSaveData LoadPlayer()
    {
        var serializer = new XmlSerializer(typeof(PlayerSaveData));
        string path = Application.persistentDataPath + "/player.xml";
        if (File.Exists(path))
        {
            var stream = new FileStream(path, FileMode.Open);
            var data = serializer.Deserialize(stream) as PlayerSaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not fount in" + path);
            return null;
        }

    }
}
