using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    #region Data
    public int level;
    public ulong money;
    public InventorySaveData inventory;
    public float[] position;
    #endregion
    public PlayerSaveData(Player player)
    {
        level = player.GetLevel();
        money = player.GetMoney();
        inventory = new InventorySaveData(player.GetInventory());
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
    public PlayerSaveData() { }
    //#region Interface
    //public int Level { get => level; }
    //public ulong Money { get => money; }
    //public InventorySaveData Inventory { get => inventory; }
    //public float[] Position { get => position;  }
    //public Tool Tool { get => tool;  }
    //#endregion
}
