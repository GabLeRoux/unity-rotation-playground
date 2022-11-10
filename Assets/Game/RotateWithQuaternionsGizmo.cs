using UnityEngine;

using NaughtyAttributes;

namespace Game
{
    public class RotateWithQuaternionsGizmo : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationAxis = Vector3.up;
        [SerializeField] private float _rotationSpeed = 1f;
        [SerializeField] private float _rotationAngle = 0f;
        
        [SerializeField] private bool _showGizmo = true;
        [SerializeField] private Color _gizmoColor = Color.green;
        [SerializeField] private float _gizmoSize = 1f;
        
        [SerializeField] private bool _showRotationAxis = true;
        [SerializeField] private Color _rotationAxisColor = Color.red;
        [SerializeField] private float _rotationAxisSize = 1f;
        
        [SerializeField] private bool _showRotationAngle = true;
        [SerializeField] private Color _rotationAngleColor = Color.blue;
        [SerializeField] private float _rotationAngleSize = 1f;

        private void Update()
        {
            _rotationAngle += _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.AngleAxis(_rotationAngle, _rotationAxis);
        }
        
        private void OnDrawGizmos()
        {
            if (!_showGizmo)
            {
                return;
            }
            
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _gizmoSize);
            
            if (_showRotationAxis)
            {
                Gizmos.color = _rotationAxisColor;
                Gizmos.DrawLine(transform.position, transform.position + _rotationAxis * _rotationAxisSize);
            }
            
            if (_showRotationAngle)
            {
                Gizmos.color = _rotationAngleColor;
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * _rotationAngleSize);
            }
        }
        
        private void OnValidate()
        {
            _rotationAxis = _rotationAxis.normalized;
        }
        
        [Button]
        private void ResetRotationAngle()
        {
            _rotationAngle = 0f;
        }
        
        [Button]
        private void ResetRotationSpeed()
        {
            _rotationSpeed = 1f;
        }
        
        [Button]
        private void ResetRotationAxis()
        {
            _rotationAxis = Vector3.up;
        }
        
        [Button]
        private void ResetGizmoSize()
        {
            _gizmoSize = 1f;
        }

        [Button]
        private void ResetAll()
        {
            ResetRotationAngle();
            ResetRotationSpeed();
            ResetRotationAxis();
            ResetGizmoSize();
        }
    }
}