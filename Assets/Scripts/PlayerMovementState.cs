using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, menuName = "Scriptable Objects/PlayerMovementState")]
public class PlayerMovementState : PlayerState
{
    #region Data
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    
    private CharacterController _controller;
    private Animator _animator;
    private float _turnSmoothSpeed;
    private int _speedAnimationHash;
    private float _percentsOfSpeed;

    #endregion

    #region Interface
    public override void Init(Player player)
    {
        base.Init(player);
        _controller = player.GetComponent<CharacterController>();
        _animator = player.GetComponent<Animator>();
        _speedAnimationHash = Animator.StringToHash("Speed");
        
    }
    public override void Run()
    {
        var direction = _player.GetJoystickInputValue();
        _percentsOfSpeed = direction.magnitude;
        if(_percentsOfSpeed >= 0.3f)
        {
            MovePlayer(direction);
        }
        else
        {
            _animator.SetFloat(_speedAnimationHash, 0);
            IsFinished = true;
        }
    }

    #endregion

    #region Methods
    private void MovePlayer(Vector3 direction)
    {
        _animator.SetFloat(_speedAnimationHash, _percentsOfSpeed);
        //Debug.Log($"direction magnitude is {_percentsOfSpeed}");
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, targetAngle, ref _turnSmoothSpeed, _turnSmoothTime);
        _player.transform.rotation = Quaternion.Euler(0, angle, 0);
        _controller.Move(direction * (_speed * _percentsOfSpeed) * Time.deltaTime);

    }
    #endregion
}
