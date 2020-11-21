using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolSaveData 
{
    public Vector3 position;
    public bool isActive;
    public ToolSaveData(Tool tool)
    {
        position = tool.gameObject.transform.position;
        isActive = tool.gameObject.activeSelf;
    }
    public ToolSaveData() { }

}
