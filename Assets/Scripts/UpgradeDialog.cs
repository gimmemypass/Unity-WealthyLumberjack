using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDialog : MonoBehaviour
{
    #region Data
    [SerializeField] private GameObject _dialogUI;
    [SerializeField] private Text _levelField; 
    [SerializeField] private Text _damageField; 
    [SerializeField] private Text _baseDamageField; 
    [SerializeField] private Text _toolNameField; 
    [SerializeField] private Text _priceField;
    [SerializeField] private Button _priceButton;

    private Player _player;
    #endregion

    #region Interface
    public void ShowDialog(Player player)
    {
        _player = player;
        var level = _player.GetLevel();
        var tool = _player.GetTool().GetToolData();
        var price = Workbench.PriceOfLevel(level);

        _levelField.text = level.ToString();
        _damageField.text = (level * tool.BaseDamage).ToString();
        _baseDamageField.text = tool.BaseDamage.ToString();
        _toolNameField.text = tool.Name;
        _priceField.text = Utils.PriceToString(price);

        _priceButton.interactable = HasPlayerEnoughMoney();
        _dialogUI.SetActive(true); 
    }

    public void HideDialog()
    {
        _dialogUI.SetActive(false);
    }
    public void LevelUp()
    {
        var price = Workbench.PriceOfLevel(_player.GetLevel());
        _player.TryDecreaseMoney(price);
        _player.LevelUp();
        UpdateFields(_player.GetLevel()); 
    }

    #endregion

    #region Methods



    private void UpdateFields(int level)
    {
        _levelField.text = level.ToString();
        _priceField.text = Utils.PriceToString(Workbench.PriceOfLevel(level));
        _priceButton.interactable = HasPlayerEnoughMoney();
        var tool = _player.GetTool().GetToolData();
        _damageField.text = (level * tool.BaseDamage).ToString();
    }

    private bool HasPlayerEnoughMoney()
    {
        var price = Workbench.PriceOfLevel(_player.GetLevel());
        if(_player.GetMoney() < price)
        {
            return false;
        }
        return true;
    }
    #endregion
}
