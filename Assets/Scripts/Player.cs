using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Data
    [Header("Initial Parameters")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _rightHand; //for axe

    [Header("States")]
    [SerializeField] private PlayerState _movingState;
    [SerializeField] private PlayerState _choppingState;

    [Header("Actual State")]
    [SerializeField] private PlayerState _currentState;

    private PlayerState _nextState;
    private CharacterController _controller;
    private Animator _animator;
    private Tool _tool;
    private int _level;
    #endregion

    #region Interface
    public Vector3 GetJoystickInputValue()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        return direction;
    }
    public CharacterController GetController() => _controller;
    public Animator GetAnimator() => _animator;
    public Tool GetTool() => _tool;
    public int GetLevel() => _level;

    #endregion

    #region Methods
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _level = 1;
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

    public void TakeTool(Tool tool, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        if(_tool != null)
        {
            Destroy(_tool);
        }
        tool.transform.parent = _rightHand;
        tool.transform.localPosition = pos;
        tool.transform.localRotation = Quaternion.Euler(rot);
        tool.transform.localScale = scale;

        _tool = tool; 
    }

    #endregion
}
