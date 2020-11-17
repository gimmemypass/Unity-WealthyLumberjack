using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Data
    [Header("Initial Parameters")]
    [SerializeField] private Joystick _joystick;

    [SerializeField] private PlayerState _movingState;
    [SerializeField] private PlayerState _choppingState;

    [Header("Actual State")]
    [SerializeField] private PlayerState _currentState;

    private PlayerState _nextState;
    #endregion

    #region Interface
    public Vector3 GetJoystickInputValue()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        return direction;
    }
    #endregion

    #region Methods
    private void Start()
    {
        SetState(_movingState, _choppingState); 
    }
    void Update()
    {
        _currentState.Run();
        if (_currentState.IsFinished)
        {
            SetNextState(); 
        }
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Animator>().SetTrigger("Chopping");
    }

    private void SetState(PlayerState currentState, PlayerState nextState)
    {
        _currentState = Instantiate(currentState);
        _currentState.Init(this);
        _nextState = nextState;
    }
    private void SetNextState()
    {
        var temp = _currentState;
        _currentState = Instantiate(_nextState);
        _currentState.Init(this);
        _nextState = temp;
    }
    #endregion
}
