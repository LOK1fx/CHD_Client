using UnityEngine;
using LOK1game.Player;

namespace LOK1game.Characters.Generic
{
    [RequireComponent(typeof(Collider))]
    public class GenericCharacterHeadZone : MonoBehaviour
    {
        [SerializeField] private GenericCharacterHead _head;

        private void OnTriggerEnter(Collider other)
        {
            if(_head == null) { return; }

            if(other.TryGetComponent<PlayerCamera>(out var camera))
            {
                _head.Target = camera.GetCameraTransform();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_head == null) { return; }

            if (other.TryGetComponent<PlayerCamera>(out var camera))
            {
                _head.Target = null;
            }
        }
    }
}