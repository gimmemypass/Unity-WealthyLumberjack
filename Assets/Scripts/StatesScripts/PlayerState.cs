using UnityEngine;

public abstract class PlayerState : ScriptableObject{

    #region Data
    protected Player _player;
    protected Animator _animator;
    public bool IsFinished { get; protected set; }
    #endregion

    #region Interface
    public virtual void Init(Player player) {
        _player = player;
        _animator = player.GetAnimator();
        IsFinished = false;
    }
    public virtual void Run() { }
    public virtual void Finish()
    {
        IsFinished = true;
    }
    #endregion

}
