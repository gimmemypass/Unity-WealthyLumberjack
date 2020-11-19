using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    #region Data
    private Dictionary<ResourceData, int> _storage = new Dictionary<ResourceData, int>();
    #endregion

    #region Interface
    public void AddResource(ResourceData type, int count)
    {
        if (_storage.TryGetValue(type, out _))
        {
            _storage[type] += count;
        }
        else
        {
            _storage[type] = count;
        }
    }
    public void ClearStorage()
    {
        foreach(var key in _storage.Keys)
        {
            _storage[key] = 0;
        }
    }
    public int GetResource(ResourceData type)
    {
        return _storage[type];
    }
    public int GetResourceWithZeroing(ResourceData type)
    {
        var ret = _storage[type];
        _storage[type] = 0;
        return ret;
    }

    public IEnumerable GetAllResources()
    {
        foreach(var pair in _storage)
        {
            yield return pair; 
        }

    }
    public IEnumerable GetAllResourcesWithZeroing()
    {
        foreach(var pair in _storage)
        {
            yield return pair;
        }
        ClearStorage();

    }
    #endregion

    #region Methods
    #endregion

}
