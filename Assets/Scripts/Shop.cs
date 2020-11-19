using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            BuyAllResources(player);
        }
       
    }
    private void BuyAllResources(Player player)
    {
        var inventory = player.GetInventory();
        try
        {
            foreach(KeyValuePair<ResourceData, int> kv in inventory.GetAllResourcesWithZeroing())
            {
                var money = kv.Key.Price * kv.Value;
                player.AddMoney(money);
            }

        }
        catch(System.Exception ex)
        {
            Debug.LogWarning(ex);
        }
        player.GetComponent<PlayerUIManager>().UpdateUI();
    }
    #endregion
}
