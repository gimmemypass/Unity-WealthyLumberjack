using UnityEngine;

public abstract class PlayerState : ScriptableObject{

    #region Data
    protected Player _player;
    public bool IsFinished { get; protected set; }
    #endregion

    #region Interface
    public virtual void Init(Player player) {
        this._player = player;
        IsFinished = false;
    }
    public abstract void Run();
    #endregion

}
