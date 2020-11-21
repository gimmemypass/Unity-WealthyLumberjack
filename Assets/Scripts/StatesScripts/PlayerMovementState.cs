using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, menuName = "Scriptable Objects/PlayerMovementState")]
public class PlayerMovementState : PlayerState
{
    #region Data
    public static float START_MOVE_VALUE = 0.3f;
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    
    private float _turnSmoothSpeed;
    private float _percentsOfSpeed;

    #endregion

    #region Interface
    public override void Init(Player player)
    {
        base.Init(player);
    }
    public override void Run()
    {
        var direction = _player.GetJoystickInputValue();
        _percentsOfSpeed = direction.magnitude;
        if(_percentsOfSpeed >= START_MOVE_VALUE)
        {
            MovePlayer(direction);
        }
        else
        {
            Finish();
        }
    }
    public override void Finish()
    {
        base.Finish();
        _animator.SetFloat("Speed", 0);
    }

    #endregion

    #region Methods
    private void MovePlayer(Vector3 direction)
    {
        _animator.SetFloat("Speed", _percentsOfSpeed);
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, targetAngle, ref _turnSmoothSpeed, _turnSmoothTime);
        _player.transform.rotation = Quaternion.Euler(0, angle, 0);
        _player.GetController().Move(direction * (_speed * _percentsOfSpeed) * Time.deltaTime);
    }
    #endregion
}
