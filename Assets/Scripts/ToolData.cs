using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ToolData", order = 51)]
public class ToolData : ScriptableObject
{
    #region Data
    [SerializeField] private string _name;
    [SerializeField] private float _range;
    [SerializeField] private float _baseDamage;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    #endregion

    #region Interface
    public string Name { get => _name;}
    public float Range { get => _range;  }
    public float BaseDamage { get => _baseDamage;  }
    public string Description { get => _description;  }
    public Sprite Icon { get => _icon; }
    #endregion

}
