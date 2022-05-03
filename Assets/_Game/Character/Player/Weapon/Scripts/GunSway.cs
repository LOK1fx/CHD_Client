using UnityEngine;

namespace com.LOK1game
{
    public class GunSway : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _returnSpeed = 7f;

        [Space]
        [SerializeField] private float _xMultiplier = 1f;
        [SerializeField] private float _yMultiplier = 1f;
        [SerializeField] private float _zMultiplier = 1f;

        private Quaternion _startRotation;
        private Vector2 _inputDelta;

        private void Start()
        {
            _startRotation = transform.localRotation;
        }

        private void Update()
        {
            _inputDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            UpdateSway();
        }

        private void UpdateSway()
        {
            var targetRotation = _startRotation * GetInputAdjustment();

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _returnSpeed);
        }

        private Quaternion GetInputAdjustment()
        {
            var inputAdjustmentX = Quaternion.AngleAxis(-_speed * _inputDelta.x * _xMultiplier, Vector3.up);
            var inputAdjustmentY = Quaternion.AngleAxis(_speed * _inputDelta.y * _yMultiplier, Vector3.right);
            var inputAdjustmentZ = Quaternion.AngleAxis(_speed * _inputDelta.x * _zMultiplier, Vector3.forward);

            return inputAdjustmentX * inputAdjustmentY * inputAdjustmentZ;
        }
    }
}