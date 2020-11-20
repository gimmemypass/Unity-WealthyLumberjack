using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    #region Data
    private Dictionary<ResourceData, int> _storage = new Dictionary<ResourceData, int>();
    
    public delegate void _notificatorUI();
    public event _notificatorUI NotifyUI;

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
        NotifyUI?.Invoke();
    }
    public void ClearStorage()
    {
        //foreach(var key in _storage.Keys)
        List<ResourceData> keys = new List<ResourceData>(_storage.Keys);
        foreach(var key in keys)
        {
            _storage[key] = 0;
        }
        NotifyUI?.Invoke();
    }
    public int GetResource(ResourceData type)
    {
        int value;
        if(_storage.TryGetValue(type, out value))
        {
            return value;
        }
        else
        {
            return 0;
        }
    }
    public int GetResourceWithZeroing(ResourceData type)
    {
        int value;
        if(_storage.TryGetValue(type, out value))
        {
            _storage[type] = 0;
            NotifyUI?.Invoke();
            return value;
        }
        return 0;
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
