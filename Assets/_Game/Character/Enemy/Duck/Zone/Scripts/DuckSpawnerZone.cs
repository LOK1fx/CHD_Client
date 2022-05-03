using UnityEngine;

namespace LOK1game
{
    public class DuckSpawnerZone : MonoBehaviour
    {
        [Range(0, 10)]
        [SerializeField] private int _duckCount = 2;
        [SerializeField] private Duck[] _duckPrefabs;

        private DuckMovementZone _zone;

        private void Awake()
        {
            _zone = GetComponent<DuckMovementZone>();

            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < _duckCount; i++)
            {
                var index = Random.Range(0, _duckPrefabs.Length);
                var duck = Instantiate(_duckPrefabs[index], Vector3.zero, Quaternion.identity);

                duck.DuckMovement.SetMovementZone(_zone);
            }
        }
    }
}