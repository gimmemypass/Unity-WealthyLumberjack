using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Linq;

public class TreesManager : MonoBehaviour
{
    #region Data
    public static List<GameObject> trees ;
    public static List<bool> state ; //active : true / false
    #endregion

    #region Interface

    
    public static void SetTreeDisabled(GameObject tree)
    {
        tree.SetActive(false);
        var id = trees.IndexOf(tree);
        state[id] = false;
    }
    public static void SaveTrees()
    {
        var serializer = new XmlSerializer(typeof(List<bool>));
        string path = Application.persistentDataPath + "/trees.xml";
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, state);
        stream.Close();
    }
    public static List<bool> LoadTrees()
    {
        var serializer = new XmlSerializer(typeof(List<bool>));
        string path = Application.persistentDataPath + "/trees.xml";
        if (File.Exists(path))
        {
            var stream = new FileStream(path, FileMode.Open);
            var data = serializer.Deserialize(stream) as List<bool>;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not fount in" + path);
            return null;
        }

    }
    #endregion
    #region Methods
    private void Start()
    {
        //getting all the trees at the scene!
        trees = new List<GameObject>();
        state = LoadTrees();
        foreach(Transform child in transform)
        {
            foreach(Transform tree in child)
            {
                trees.Add(tree.gameObject);
            }
        }
        if(state == null)
        {
            state = Enumerable.Repeat(true, trees.Count).ToList();
        }
        else
        {
            for(int i = 0; i< trees.Count; i++)
            {
                trees[i].gameObject.SetActive(state[i]);
            }
        }

    }
    private void OnApplicationQuit()
    {
        SaveTrees();
    }
    #endregion
}
