using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    #region Data
    [SerializeField] private GameObject _resource;
    [SerializeField] private int _countResources = 1;

    private ResourceData _data;
    private Animator _animator;
    private float _health;

    #endregion

    #region Interface
    public void ApplyDamage(float damage)
    {
        if(damage > 0)
        {
            _animator.SetTrigger("ApplyDamage");
            _health -= damage; 
        }
        if(IsAlive() == false)
        {
            StartCoroutine(Die());
        }
    }

    public bool IsAlive() => _health > 0f;
    #endregion

    #region Methods
    private void Start()
    {
        _data = _resource.GetComponent<Resource>().GetData();
        _animator = GetComponent<Animator>();
        _health = _data.Health; 
    }
    private IEnumerator Die()
    {
        for(int i = 0; i < _countResources; i++)
        {
            PoolManager.GetObject(_resource.name, transform.position, _resource.transform.rotation);
        }
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        TreesManager.SetTreeDisabled(gameObject);
    }
    #endregion


}
