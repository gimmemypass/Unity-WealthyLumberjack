using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class ToolSaver : MonoBehaviour
{
    private void Start()
    {
        var data = Load();
        if(data != null)
        {
            gameObject.transform.position = data.position;
        }
    }
    public void Save()
    {
        var serializer = new XmlSerializer(typeof(ToolSaveData));
        string path = Application.persistentDataPath + $"/{gameObject.name}.xml";
        var stream = new FileStream(path, FileMode.Create);
        var data = new ToolSaveData(gameObject.GetComponent<Tool>());
        serializer.Serialize(stream, data);
        stream.Close();
    }
    public ToolSaveData Load()
    {
        var serializer = new XmlSerializer(typeof(ToolSaveData));
        string path = Application.persistentDataPath + $"/{gameObject.name}.xml";
        if (File.Exists(path))
        {
            var stream = new FileStream(path, FileMode.Open);
            var data = serializer.Deserialize(stream) as ToolSaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not fount in" + path);
            return null;
        }

    }
    private void OnDestroy()
    {
        Save();
    }
}
