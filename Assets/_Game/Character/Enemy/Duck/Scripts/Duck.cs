using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    [RequireComponent(typeof(Health), typeof(DuckMovement))]
    public class Duck : Actor, IDamagable
    {
        public UnityAction OnDie;

        public DuckMovement DuckMovement { get; private set; }

        private Health _health;

        private void Awake()
        {
            DuckMovement = GetComponent<DuckMovement>();
            _health = GetComponent<Health>();
        }

        public void TakeDamage(Damage damage)
        {
            _health.ReduceHealth(damage.Value);

            if(_health.Hp <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            OnDie?.Invoke();

            DuckMovement.enabled = false;
            enabled = false;

            Destroy(gameObject, 8f);
        }
    }
}