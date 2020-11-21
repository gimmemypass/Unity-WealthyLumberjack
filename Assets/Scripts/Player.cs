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

    // state pattern
    [Header("States")]
    [SerializeField] private PlayerState _movingState;
    [SerializeField] private PlayerState _choppingState;
    [SerializeField] private PlayerState _superJumpState;

    [Header("Actual State")]
    [SerializeField] private PlayerState _currentState;

    private CharacterController _controller;

    private Tool _tool;
    private int _level;
    private ulong _money = 0;
    private Inventory _inventory;

    private delegate void _notificatorUI();
    private event _notificatorUI NotifyUI;
    #endregion

    #region Interface GettingField

    public CharacterController GetController() => _controller;
    public Tool GetTool() => _tool;
    public int GetLevel() => _level;
    public Inventory GetInventory() => _inventory;

    public ulong GetMoney() => _money;
    #endregion

    #region Interface SaveLoad
    public void SavePlayer()
    {
        SaveLoadSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerSaveData data  = SaveLoadSystem.LoadPlayer();
        if(data != null)
        {
            _level = data.level;
            _money = data.money;
            _inventory = new Inventory(data.inventory);
            Vector3 pos;
            pos.x = data.position[0];
            pos.y = data.position[1];
            pos.z = data.position[2];
            transform.position = pos ;
        }
        else
        {
            _level = 1;
            _money = 0;
            _inventory = new Inventory();
            SavePlayer();
        }
        
    }
    #endregion
    #region Interface
    public void LevelUp()
    {
        _level++;
    }
    public Vector3 GetJoystickInputValue()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        return direction;
    }
    public void AddMoney(ulong money)
    {
        if(money >= 0)
        {
            _money += money;
            NotifyUI?.Invoke();
        }
        else
        {
            throw new System.Exception("money < 0");
        }
    }
    public bool TryDecreaseMoney(ulong money)
    {
        if(money > 0)
        {
            if(_money >= money)
            {
                _money -= money;
                NotifyUI?.Invoke();
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
    public void SetSuperJumpState()
    {
        _currentState.Finish();
        SetState(_superJumpState);
    }
    public void TakeTool(Tool tool, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        if(_tool != null)
        {
            //Destroy(_tool.gameObject);
            _tool.gameObject.SetActive(false);
        }
        tool.transform.parent = _rightHand;

        tool.transform.localPosition = pos;
        tool.transform.localRotation = Quaternion.Euler(rot);
        tool.transform.localScale = scale;

        _tool = tool; 
    }

    #endregion

    #region Methods
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        LoadPlayer();

        NotifyUI += _playerUIManager.UpdateUI;
        _inventory.NotifyUI += _playerUIManager.UpdateUI;

        SetState(_movingState); 
        NotifyUI?.Invoke();
    }
    void Update()
    {
        _currentState.Run();
        if (_currentState.IsFinished)
        {
            SetNextState(); 
        }
    }

    private void OnApplicationQuit()
    {
        SavePlayer();
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
        if(_percentsOfSpeed >= PlayerMovementState.START_MOVE_VALUE)
        {
            SetState(_movingState);
        }
        else
        {
            SetState(_choppingState);
        }
    }

    #endregion
}
