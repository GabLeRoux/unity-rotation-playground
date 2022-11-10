using UnityEngine;
using NaughtyAttributes;

namespace Game
{
    public class DemonstrateGimbalLockGizmo : MonoBehaviour
    {

        [SerializeField] private float _roll;
        [SerializeField] private float _pitch;
        [SerializeField] private float _yaw;

        [SerializeField] private float _rollSpeed;
        [SerializeField] private float _pitchSpeed;
        [SerializeField] private float _yawSpeed;

        private void Update()
        {
            _roll += _rollSpeed * Time.deltaTime;
            _pitch += _pitchSpeed * Time.deltaTime;
            _yaw += _yawSpeed * Time.deltaTime;

            transform.eulerAngles = new Vector3(_roll, _pitch, _yaw);
        }

        private void OnDrawGizmos()
        {
            var t = transform;
            var position = t.position;
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(position, t.right);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, t.up);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(position, t.forward);
        }

        private void OnDrawGizmosSelected()
        {
            var t = transform;
            var position = t.position;
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(position, t.right);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, t.up);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(position, t.forward);
        }
        
        [Button]
        private void Reset()
        {
            _roll = 0;
            _pitch = 0;
            _yaw = 0;
            
            _rollSpeed = 0;
            _pitchSpeed = 0;
            _yawSpeed = 0;
        }

        [Button]
        private void ExampleLock()
        {
            _roll = 90;
            _pitch = 0;
            _yaw = 0;
            
            _rollSpeed = 0;
            _pitchSpeed = 10;
            _yawSpeed = 10;
        }

    }
}