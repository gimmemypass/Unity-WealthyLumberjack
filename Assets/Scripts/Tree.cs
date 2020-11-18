using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    #region Data
    [SerializeField] private float _health;

    private Animator _animator;

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
        _animator = GetComponent<Animator>();
    }
    private IEnumerator Die()
    {
        //todo
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
    #endregion


}
