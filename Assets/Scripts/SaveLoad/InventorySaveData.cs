using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class InventorySaveData  
{
    public List<string> keys; 
    public List<int> values; 
    public InventorySaveData(Inventory inventory)
    {
        keys = inventory.GetKeys().Select(k => k.name).ToList();
        values = inventory.GetValues();
    }
    public InventorySaveData() { }
}
