using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    #region Data
    [SerializeField] private const int _priceCoef = 2;
    [SerializeField] private UpgradeDialog _dialogUI;
    #endregion

    #region Interface
    public static int PriceOfLevel(int level)
    {
        return level * _priceCoef; 
    }
    #endregion

    #region Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _dialogUI.ShowDialog(other.gameObject.GetComponent<Player>());
        } 
    }

    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.CompareTag("Player"))
        {
            _dialogUI.HideDialog();
        }
    }
    #endregion

}
