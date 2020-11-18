using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour
{
    #region Data
    [SerializeField] private ToolData _toolData;
    [SerializeField] private Vector3 _defaultPosition = new Vector3(0.094f, -0.498f, -0.082f);
    [SerializeField] private Vector3 _defaultRotation = new Vector3(-2.248f, -178.264f, -7.179f);
    [SerializeField] private Vector3 _defaultScale = new Vector3(2f,2f,2f);

    #endregion

    #region Interface
    public string GetName() => _toolData.Name;
    public string GetDescription() => _toolData.Description;
    public float GetRange() => _toolData.Range;
    public float GetBaseDamage() => _toolData.BaseDamage;
    public Sprite GetIcon() => _toolData.Icon;
    public ToolData GetTool() => _toolData;

    public void SetTool(ToolData tool)
    {
        _toolData = tool;
    }
    #endregion

    #region Methods
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject
                .GetComponent<Player>()
                .TakeTool(this , _defaultPosition, _defaultRotation, _defaultScale);
        }
    }
    #endregion



}
