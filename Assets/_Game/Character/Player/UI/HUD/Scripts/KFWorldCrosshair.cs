using UnityEngine;

namespace LOK1game
{
    public class KFWorldCrosshair : MonoBehaviour
    {
        [SerializeField] private float _distance = 10f;

        private Transform _activeCameraParent;
        private Camera _activeCamera;

        private void LateUpdate()
        {
            if (_activeCamera == null)
            {
                _activeCamera = Camera.main;
                _activeCameraParent = _activeCamera.transform.parent.parent;
            }

            if (_activeCamera != null)
            {
                var pos = _activeCameraParent.position + _activeCameraParent.forward * _distance;

                transform.position = _activeCamera.WorldToScreenPoint(pos);
            }
        }
    }
}