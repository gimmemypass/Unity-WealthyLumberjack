using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerUIManager : MonoBehaviour
{
    #region Data
    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _treeCountText;

    private Player _player;
    #endregion

    #region Methods
    private void Start()
    {
        _player = GetComponent<Player>();
        UpdateUI();
    }
    public void UpdateUI()
    {
        _moneyText.text = _player.GetMoney().ToString();
        int count = 0;
        var inventory =_player.GetInventory() ;
        if(inventory != null)
            foreach(KeyValuePair<ResourceData, int> kv in inventory.GetAllResources())
            {
                count += kv.Value;
            }
        _treeCountText.text = count.ToString(); 
    }
    #endregion

}
