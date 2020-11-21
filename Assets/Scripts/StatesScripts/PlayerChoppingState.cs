using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, menuName = "Scriptable Objects/PlayerChoppingState")]
public class PlayerChoppingState : PlayerState
{
    #region Data
    [SerializeField] private LayerMask _treeLayer;


    private Tool _tool;
    private float _percentsOfSpeed;
    private Collider[] _trees;
    #endregion

    #region Interface
    public override void Init(Player player)
    {
        base.Init(player);
        _tool = player.GetTool();
        _trees = new Collider[0];
        _player.StartCoroutine(ChopTrees());

    }
    public override void Run()
    {
        var direction = _player.GetJoystickInputValue();
        _percentsOfSpeed = direction.magnitude;
        if (_percentsOfSpeed >= PlayerMovementState.STARTVALUE)
        {
            Finish();
            return;
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
    public override void Finish()
    {
        base.Finish();
        _animator.SetFloat("Speed", _percentsOfSpeed);
    }
    #endregion

    #region Methods
    private IEnumerator ChopTrees()
    {
        _trees = TreesArround();
        while (IsFinished == false && _trees.Length > 0)
        {
            RotateTo(_trees[0].transform); 
            _animator.SetTrigger("Chopping");
            Debug.Log("chopping");
            yield return new WaitForSeconds(0.8f);

            if (IsFinished || _animator.GetCurrentAnimatorStateInfo(0).IsName("Chopping") == false)
            {
                yield break;
            }

            foreach(var tree in _trees)
            {
                tree.GetComponent<Tree>().ApplyDamage(GetChoppingDamage());
            }
            yield return new WaitForSeconds(1);
            _trees = TreesArround();
        }
    }
    private void RotateTo(Transform target)
    {
        var direction = target.position - _player.transform.position;
        Debug.DrawLine(target.position, _player.transform.position, Color.red, 2);
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        _player.transform.rotation = Quaternion.Euler(0, targetAngle, 0);

    }

    private float GetChoppingDamage()
    {
        var tool = _player.GetTool();
        var damage = tool.GetBaseDamage() * _player.GetLevel();
        return damage;
    }
    private Collider[] TreesArround() => Physics.OverlapSphere(_player.transform.position, _tool.GetRange(), _treeLayer);
    #endregion

}
