using UnityEngine;

namespace LOK1game
{
    public class ArmsOffset : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        [Space]
        [HideInInspector] public Vector3 Offset;

        [SerializeField] private Vector3 _maxOffset;

        private Vector3 _tempOffset;

        private void LateUpdate()
        {
            _tempOffset = Vector3.Lerp(_tempOffset, Vector3.zero, Time.deltaTime * _speed);

            transform.localPosition = Vector3.Lerp(transform.localPosition, _tempOffset, Time.deltaTime * _speed) + Offset;

            var pos = transform.localPosition;
            transform.localPosition = new Vector3(Clamp(pos.x, _maxOffset.x), Clamp(pos.y, _maxOffset.y), Clamp(pos.z, _maxOffset.z));
        }

        public void AddTemp(Vector3 tempOffset)
        {
            _tempOffset += tempOffset;
        }

        private float Clamp(float value, float max)
        {
            return Mathf.Clamp(value, -max, max);
        }
    }
}