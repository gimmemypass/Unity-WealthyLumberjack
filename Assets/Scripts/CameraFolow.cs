using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    #region Data
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _startPos;

    #endregion

    #region Methods
    private void Start()
    {
        transform.position = _startPos ;
    }
    private void LateUpdate()
    {
        var pos = transform.position;
        pos.z = _target.position.z + _offset.z;

        var smothedPos = Vector3.Lerp(transform.position, pos, _smoothSpeed); 
        transform.position = smothedPos;

    }
    #endregion

}
