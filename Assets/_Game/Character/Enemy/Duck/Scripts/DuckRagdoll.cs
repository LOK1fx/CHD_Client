using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Duck), typeof(Rigidbody))]
    public class DuckRagdoll : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 250f;
        [SerializeField] private float _velocityBoost = 10f;

        private Duck _duck;
        private Rigidbody _rb;

        private bool _activated;

        private void Start()
        {
            _duck = GetComponent<Duck>();
            _rb = GetComponent<Rigidbody>();

            _duck.OnDie += Activate;
        }

        private void Update()
        {
            if(!_activated) { return; }

            var lookRotation = Quaternion.LookRotation(Vector3.up * 5f);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
        }

        private void Activate()
        {
            if(TryGetComponent<Collider>(out var collider))
            {
                collider.isTrigger = true;
            }

            _rb.velocity = _duck.DuckMovement.Velocity * _velocityBoost;
            _rb.isKinematic = false;
            _rb.useGravity = true;
            _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            _activated = true;
        }

        private void OnDestroy()
        {
            _duck.OnDie -= Activate;
        }
    }
}