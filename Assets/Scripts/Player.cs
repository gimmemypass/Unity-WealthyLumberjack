using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Data
    [Header("Initial Parameters")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _rightHand; //for axe
    [SerializeField] private PlayerUIManager _playerUIManager;

    [Header("States")]
    [SerializeField] private PlayerState _movingState;
    [SerializeField] private PlayerState _choppingState;
    [SerializeField] private PlayerState _superJumpState;

    [Header("Actual State")]
    [SerializeField] private PlayerState _currentState;

    private CharacterController _controller;
    private Animator _animator;

    private Tool _tool;
    private int _level;
    private int _money;
    private Inventory _inventory;
    //todo  инвентарь 
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
    public Inventory GetInventory() => _inventory;

    public void LevelUp()
    {
        _level++;
    }
    public int GetMoney() => _money; 
    public void AddMoney(int money)
    {
        if(money > 0)
        {
            _money += money;
        }
        else
        {
            throw new System.Exception("money < 0");
        }
    }
    public bool TryDecreaseMoney(int money)
    {
        if(money > 0)
        {
            if(_money > money)
            {
                _money -= money;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            throw new System.Exception("money < 0");
        }
    }
    #endregion

    #region Methods
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _level = 1;
        _money = 2;
        _inventory = new Inventory();

        SetState(_movingState); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentState.Finish();
            SetState(_superJumpState); 
        }
        _currentState.Run();
        if (_currentState.IsFinished)
        {
            SetNextState(); 
        }
    }

    private void SetState(PlayerState currentState)
    {
        _currentState = Instantiate(currentState);
        _currentState.Init(this);
    }
    private void SetNextState()
    {
        var direction = GetJoystickInputValue();
        var _percentsOfSpeed = direction.magnitude;
        if(_percentsOfSpeed >= PlayerMovementState.STARTVALUE)
        {
            SetState(_movingState);
        }
        else
        {
            SetState(_choppingState);
        }
    }

    public void TakeTool(Tool tool, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        if(_tool != null)
        {
            Destroy(_tool.gameObject);
        }
        tool.transform.parent = _rightHand;
        tool.transform.localPosition = pos;
        tool.transform.localRotation = Quaternion.Euler(rot);
        tool.transform.localScale = scale;

        _tool = tool; 
    }
    #endregion
}
