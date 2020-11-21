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
    public void UpdateUI()
    {
        _moneyText.text = Utils.PriceToString(_player.GetMoney());
        var inventory =_player.GetInventory() ;
        if(inventory != null)
            foreach(var item in _resourceItems)
            {
                item.Count.text = inventory.GetResource(item.Data).ToString();
            }
    }
    #endregion

}
