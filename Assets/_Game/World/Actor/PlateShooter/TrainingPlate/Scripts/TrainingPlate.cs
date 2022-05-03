using UnityEngine;

namespace LOK1game.World
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class TrainingPlate : Actor, IDamagable
    {
        [SerializeField] private float _startTorqueForce = 15f;

        [Space]
        [SerializeField] private Rigidbody _destroyFx;
        [SerializeField] private float _lifeTime = 6f;

        private Rigidbody _rigidbody;

#if UNITY_EDITOR

        private BoxCollider _collider;

#endif

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

#if UNITY_EDITOR

            _collider = GetComponent<BoxCollider>();

#endif

            Destroy(gameObject, _lifeTime);
        }

        public void TakeDamage(Damage damage)
        {
            DestroyPlate();
        }

        public void Shoot(Vector3 direction, float force)
        {
            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
            _rigidbody.AddTorque(transform.up * _startTorqueForce, ForceMode.VelocityChange);
        }

        private void DestroyPlate()
        {
            var fx = Instantiate(_destroyFx, transform.position, transform.rotation);

            fx.velocity = _rigidbody.velocity;

            Destroy(fx.gameObject, 2f);
            Destroy(gameObject);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if(_rigidbody == null) { return; }

            var pos = transform.position + _rigidbody.velocity;

            Gizmos.DrawCube(pos, _collider.size);
        }

#endif
    }
}