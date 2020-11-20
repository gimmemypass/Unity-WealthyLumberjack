using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    #region Data
    [SerializeField] private ResourceData _data;
    #endregion

    #region Interface
    public ResourceData GetData() => _data;
    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            player.GetInventory().AddResource(_data, 1);
            //todo: add pool
            Destroy(gameObject);
        }
    }
    #endregion
}
