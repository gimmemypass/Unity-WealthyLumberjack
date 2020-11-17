using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, menuName = "Scriptable Objects/PlayerChoppingState")]
public class PlayerChoppingState : PlayerState
{
    #region Data
    [SerializeField] private float _turnSmoothTime = 0.1f;
    [SerializeField] private LayerMask treeLayer;
    [SerializeField] private float choppingRange = 1f;

    private float _turnSmoothSpeed;
    private Animator _animator;
    private int _speedAnimationHash;
    private float _percentsOfSpeed;
    private Collider[] _trees;
    #endregion

    #region Interface
    public override void Init(Player player)
    {
        base.Init(player);
        _animator = player.GetComponent<Animator>();
        _speedAnimationHash = Animator.StringToHash("Speed");
        _trees = new Collider[0];
        _player.StartCoroutine(ChopTrees());
    }
    public override void Run()
    {
        var direction = _player.GetJoystickInputValue();
        _percentsOfSpeed = direction.magnitude;
        if (_percentsOfSpeed >= 0.3f)
        {
            _animator.SetFloat(_speedAnimationHash, _percentsOfSpeed);
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
            _animator.SetTrigger("Chopping");
            Debug.Log("chopping");
            yield return new WaitForSeconds(1);
            foreach(var tree in _trees)
            {
                tree.gameObject.SetActive(false);
            }
            _trees = Physics.OverlapSphere(_player.transform.position, choppingRange, treeLayer);
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

}
