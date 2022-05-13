using UnityEngine;

namespace LOK1game.Characters.Generic
{
    public class GenericCharacterHead : MonoBehaviour
    {
        public Transform Target;

        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private Vector3 _targerPositionOffset;
        [SerializeField] private Vector3 _upwardDirection = Vector3.up;

        private Vector3 _targetPosition;

        private void LateUpdate()
        {
            if (Target != null)
            {
                LerpTargetToPosition(Target.position);
            }
            else
            {
                LerpTargetToPosition(transform.forward);
            }

            transform.LookAt(_targetPosition + _targerPositionOffset, _upwardDirection);
        }

        private void LerpTargetToPosition(Vector3 position)
        {
            _targetPosition = Vector3.Lerp(_targetPosition, position, Time.deltaTime * _rotationSpeed);
        }
    }
}