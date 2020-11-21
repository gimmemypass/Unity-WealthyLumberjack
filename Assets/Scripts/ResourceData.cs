using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string Name { get => _name; }
    public float Health { get => _health; }
    public Sprite Icon { get => _icon;  }
    public ulong Price { get => price; }
    #endregion
}

