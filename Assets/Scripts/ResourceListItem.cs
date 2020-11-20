using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResourceListItem : MonoBehaviour
{
    #region Data
    [SerializeField] private ResourceData _data;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _count;

    #endregion

    #region Interface
    public ResourceData Data { get => _data; }
    public Image Icon { get => _icon; }
    public Text Count { get => _count; }
    #endregion

    #region Methods
    private void Start()
    {
        _icon.sprite = _data.Icon;
    }
    #endregion
}
