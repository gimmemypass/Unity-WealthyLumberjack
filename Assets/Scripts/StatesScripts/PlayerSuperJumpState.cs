using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, menuName = "Scriptable Objects/PlayerSuperJumpState")]
public class PlayerSuperJumpState : PlayerState
{
    #region Data
    [SerializeField] private LayerMask _treeLayer;
    [SerializeField] private float _range = 2f;
    [SerializeField] private float _baseDamage;
    #endregion

    #region Interface
    public override void Init(Player player)
    {
        base.Init(player);
        _player.StartCoroutine(SuperJump());
        _animator.SetFloat("Speed", 0);
    }
    #endregion

    #region Methods
    private IEnumerator SuperJump()
    {
        _animator.SetTrigger("SuperJump");
        _player.GetTool().gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        if (IsFinished)
            yield break;

        var _trees = Physics.OverlapSphere(_player.transform.position, _range, _treeLayer);
        Debug.Log(_trees.Length);
        foreach(var tree in _trees)
        {
            tree.GetComponent<Tree>().ApplyDamage(GetDamage());
        }
        yield return new WaitForSeconds(1.5f);
        _player.GetTool().gameObject.SetActive(true);
        Finish();
    }

    private float GetDamage()
    {
        return _player.GetLevel() * _baseDamage;
    }
    #endregion
}
