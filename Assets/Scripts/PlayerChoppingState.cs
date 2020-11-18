using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, menuName = "Scriptable Objects/PlayerChoppingState")]
public class PlayerChoppingState : PlayerState
{
    #region Data
    [SerializeField] private LayerMask treeLayer;
    [SerializeField] private float choppingRange = 1f;

    private Animator _animator;
    private float _percentsOfSpeed;
    private Collider[] _trees;
    #endregion

    #region Interface
    public override void Init(Player player)
    {
        base.Init(player);
        _animator = player.GetAnimator();
        _trees = new Collider[0];
        _player.StartCoroutine(ChopTrees());
    }
    public override void Run()
    {
        var direction = _player.GetJoystickInputValue();
        _percentsOfSpeed = direction.magnitude;
        if (_percentsOfSpeed >= 0.3f)
        {
            _animator.SetFloat("Speed", _percentsOfSpeed);
            IsFinished = true;
            return;
        }
    }
    #endregion

    #region Methods
    private IEnumerator ChopTrees()
    {
        _trees = Physics.OverlapSphere(_player.transform.position, choppingRange, treeLayer);
        while (IsFinished == false && _trees.Length > 0)
        {
            RotateTo(_trees[0].transform); 
            _animator.SetTrigger("Chopping");
            Debug.Log("chopping");
            yield return new WaitForSeconds(0.8f);

            if (IsFinished)
                break;

            foreach(var tree in _trees)
            {
                tree.GetComponent<Tree>().ApplyDamage(GetChoppingDamage());
            }
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length - 1);
            _trees = Physics.OverlapSphere(_player.transform.position, choppingRange, treeLayer);
        }
        _animator.SetTrigger("Moving");
        //Debug.Log("Moving");
    }
    private void RotateTo(Transform target)
    {
        var direction = target.position - _player.transform.position;
        var angle = Vector3.Angle(_player.transform.forward, direction);
        _player.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private float GetChoppingDamage()
    {
        var tool = _player.GetTool();
        var damage = tool.GetBaseDamage() * _player.GetLevel();
        return damage;
    }
    #endregion

}
