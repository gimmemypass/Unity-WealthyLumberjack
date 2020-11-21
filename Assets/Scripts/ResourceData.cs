using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(order = 51, menuName = "Scriptable Objects/ResourceData")]
public class ResourceData : ScriptableObject
{
    #region Data
    [SerializeField] private string _name;
    [SerializeField] private float _health;
    [SerializeField] private Sprite _icon;
    [SerializeField] private ulong price;
    #endregion

    #region Interface
    public float Health { get => _health; set => _health = value; }
    public string Name { get => _name; set => _name = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public ulong Price { get => price; set => price = value; }
    #endregion
}

