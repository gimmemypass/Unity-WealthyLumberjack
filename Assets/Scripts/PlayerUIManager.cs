using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerUIManager : MonoBehaviour
{
    #region Data
    [SerializeField] private Text _moneyText;

    [SerializeField] private List<ResourceListItem> _resourceItems;

    [SerializeField] Player _player;
    #endregion

    #region Methods
    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        _moneyText.text = _player.GetMoney().ToString();
        var inventory =_player.GetInventory() ;
        if(inventory != null)
            //foreach(KeyValuePair<ResourceData, int> kv in inventory.GetAllResources())
            //{
            //    _resourceItems.Count.text = kv.Value.ToString();
            //}
            foreach(var item in _resourceItems)
            {
                item.Count.text = inventory.GetResource(item.Data).ToString();
            }
    }
    #endregion

}
