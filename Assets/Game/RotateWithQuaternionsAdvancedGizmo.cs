using UnityEngine;
using NaughtyAttributes;

namespace Game
{
    public enum RotationType
    {
        LookRotation,
        Angle,
        Euler,
        Slerp,
        FromToRotation,
        Identity
    }

    public class RotateWithQuaternionsAdvancedGizmo : MonoBehaviour
    {
        [SerializeField] private RotationType _rotationType = RotationType.LookRotation;

        [SerializeField] private Vector3 _rotationAxis = Vector3.up;

        [ShowIf("_rotationType", RotationType.Slerp)] [SerializeField]
        private float _rotationSpeed = 1f;

        [SerializeField] private float _rotationAngle = 0f;

        [SerializeField] private bool _showWireSphereGizmo = true;
        [SerializeField] private Color _wireSphereGizmoColor = Color.green;
        [SerializeField] private float _wireSphereGizmoSize = 1f;

        [SerializeField] private bool _showRotationGizmo = true;
        [SerializeField] private Color _rotationGizmoColor = Color.red;
        [SerializeField] private float _rotationGizmoSize = 1f;

        void Update()
        {
            switch (_rotationType)
            {
                case RotationType.LookRotation:
                    // Creates a rotation with the specified forward and upwards directions.
                    transform.rotation = Quaternion.LookRotation(_rotationAxis);
                    break;
                case RotationType.Angle:
                    // Creates a rotation which rotates angle degrees around axis.
                    transform.rotation = Quaternion.AngleAxis(_rotationAngle, _rotationAxis);
                    break;
                case RotationType.Euler:
                    // Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis.
                    transform.rotation = Quaternion.Euler(_rotationAxis);
                    break;
                case RotationType.Slerp:
                    // Spherically interpolates between quaternions a and b by ratio t. The parameter t is clamped to the range [0, 1].
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_rotationAxis),
                        Time.deltaTime * _rotationSpeed);
                    break;
                case RotationType.FromToRotation:
                    // Creates a rotation which rotates from fromDirection to toDirection.
                    transform.rotation = Quaternion.FromToRotation(transform.position, _rotationAxis);
                    break;
                case RotationType.Identity:
                    // The identity rotation (Read Only).
                    transform.rotation = Quaternion.identity;
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            if (_showWireSphereGizmo)
            {
                Gizmos.color = _wireSphereGizmoColor;
                Gizmos.DrawWireSphere(transform.position, _wireSphereGizmoSize);
            }

            if (_showRotationGizmo)
            {
                Gizmos.color = _rotationGizmoColor;
                Gizmos.DrawRay(transform.position, transform.forward * _rotationGizmoSize);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_showWireSphereGizmo)
            {
                Gizmos.color = _wireSphereGizmoColor;
                Gizmos.DrawWireSphere(transform.position, _wireSphereGizmoSize);
            }

            if (_showRotationGizmo)
            {
                Gizmos.color = _rotationGizmoColor;
                Gizmos.DrawRay(transform.position, transform.forward * _rotationGizmoSize);
            }
        }
    }
}