using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(CharacterSpawnPoint))]
    public class CharacterSpawnPointGizmos : MonoBehaviour
    {
        [SerializeField] private Color _capsuleColor = Color.green;
        [SerializeField] private Mesh _capsuleMesh;
        [SerializeField] private Vector3 _capsuleScale;
        [SerializeField] private Vector3 _capsulePositionOffset;

        private CharacterSpawnPoint _spawn;

        private void Awake()
        {
            _spawn = GetComponent<CharacterSpawnPoint>();
        }

        private void OnDrawGizmos()
        {
            if(_capsuleMesh != null)
            {
                Gizmos.color = _capsuleColor;
                Gizmos.DrawWireMesh(_capsuleMesh, transform.position + _capsulePositionOffset, Quaternion.identity, _capsuleScale);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + Vector3.up, new Vector3(1, 2, 1));
        }
    }
}