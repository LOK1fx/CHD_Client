using UnityEngine;

namespace LOK1game.Weapon
{
    [RequireComponent(typeof(Gun), typeof(Animator))]
    public class GunAnimation : MonoBehaviour
    {
        private const string TRIGGER_SHOOT = "Shoot";
        private const string TRIGGER_EQUIP = "Equip";

        private Animator _animator;
        private Gun _gun;

        private void Awake()
        {
            _gun = GetComponent<Gun>();
            _animator = GetComponent<Animator>();

            _gun.OnWeaponAttack += OnShoot;
            _gun.OnWeaponEquip += OnEquip;
        }

        private void OnShoot()
        {
            _animator.SetTrigger(TRIGGER_SHOOT);
        }

        private void OnEquip()
        {
            _animator.SetTrigger(TRIGGER_EQUIP);
        }


        private void OnDestroy()
        {
            _gun.OnWeaponAttack -= OnShoot;
            _gun.OnWeaponEquip -= OnEquip;
        }
    }
}